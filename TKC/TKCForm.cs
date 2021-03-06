﻿using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TKC
{
    /// <summary>
    /// Windows form class that handles main user interaction
    /// </summary>
    public partial class MainUIWindow : Form
    {

        public MainUIWindow()
        {
            InitializeComponent();
        }
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        JSONReaderSingleton reader;

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            //getting a logfiles directory path
            string directoryPath;
            if (ConfigurationManager.AppSettings["FirstRun"].Equals("true"))
            {
                //finds path to Users folder ("C:\Users\<user>)" which is then used to find default path of ED journals
                directoryPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    directoryPath = Directory.GetParent(directoryPath).ToString() + @"\Saved Games\Frontier Developments\Elite Dangerous";
                }
                ChangeConfig("JournalsDirPath", directoryPath);
                ChangeConfig("FirstRun", "false");

            }
            else
            {
                directoryPath = ConfigurationManager.AppSettings["JournalsDirPath"];
            }

            bool.TryParse(ConfigurationManager.AppSettings["ScreenShotsPermission"], out bool ScreenShotsPermission);
            try
            {
                reader = JSONReaderSingleton.GetInstance(directoryPath, out string directoryPathOutput, ScreenShotsPermission);
                //saves Journals directory path to config if its different
                if (!directoryPathOutput.Equals(ConfigurationManager.AppSettings["JournalsDirPath"]))
                {
                    ChangeConfig("JournalsDirPath", directoryPath);
                }
            }
            catch (ArgumentException ex)
            {
                log.Info(ex.Message, ex);
                throw;
            }

            KillCounter.Text = "Scanning logs please wait";

            StartAsyncMethods();

        }

        /// <summary>
        /// Method that starts asynchrounous tasks
        /// </summary>
        private async void StartAsyncMethods()
        {

            await Task.Run(() => StartReadingLogs());
            Thread printingThread = new Thread(PrintKillsInIntervals)
            {
                Name = "Printer",
                IsBackground = true
            };

            Thread realTimeReaderThread = new Thread(StartReadingInRealTime)
            {
                Name = "RealTimeR",
                IsBackground = true
            };

            realTimeReaderThread.Start();
            printingThread.Start();

        }

        /// <summary>
        /// Starts JSON reader reading method
        /// </summary>
        private void StartReadingLogs()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            try
            {
                reader.ReadDirectory();
            }
            catch (Exception ex)
            {
                log.Fatal("Unknown error", ex);
                throw;
            }
        }

        /// <summary>
        /// Starts JSON reader reading method
        /// </summary>
        private void StartReadingInRealTime()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            
            try
            {
                reader.ReadLastJsonWhilePlaying();
            }
            catch (Exception ex)
            {
                log.Fatal("Unknown error", ex);
                throw;
            }
        }

        /// <summary>
        /// Print kills in intervals
        /// </summary>
        private void PrintKillsInIntervals()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            while (true)
            {
                TimeSpan ts = stopWatch.Elapsed;
                //prints kills if number of thargoid kills changed or after 10 sec
                if (reader.Counter.CheckKillChange() == true || ts.Seconds >= 10)
                {
                    KillCounterSetText(reader.Counter.PrintAllKills());
                    stopWatch.Restart();
                }
                //after 10 sec prints kills even if they are 0
                else if(reader.Counter.CheckIfKillsZero() && ts.Seconds >= 10)
                {
                    KillCounterSetText(reader.Counter.PrintAllKills());
                    stopWatch.Restart();
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }

        /// <summary>
        /// This delegate enables asynchronous calls for setting
        /// </summary>
        /// <param name="text">Text for delegate</param>
        delegate void StringArgReturningVoidDelegate(string text);

        /// <summary>
        /// Method for thread safe calls of Windows forms KillCounter label
        /// </summary>
        /// <param name="text">Thargoid kills in string</param>
        public void KillCounterSetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.KillCounter.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(KillCounterSetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.KillCounter.Text = text;
            }
        }

        /// <summary>
        /// Changes values in app.config app setting section
        /// </summary>
        /// <param name="key">Key of setting</param>
        /// <param name="value">Value of setting</param>
        private void ChangeConfig(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// Opens settings menu
        /// </summary>
        private void SettingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Settings us = new Settings();
            us.ShowDialog();
        }
    }
}
