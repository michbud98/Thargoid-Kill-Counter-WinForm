using System;
using System.Threading;
using System.Windows.Forms;

namespace TKC
{
    public partial class TKCForm : Form
    {
        public TKCForm()
        {
            InitializeComponent();
        }
        JSONReaderSingleton reader = JSONReaderSingleton.GetInstance();
       
        private void Form1_Load(object sender, EventArgs e)
        {
            KillCounter.Text = "Scanning log files";
            try
            {
                reader.ReadDirectory();
            }catch(Exception ex)
            {
                //error logging
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Application.Exit();
            }
            
            
            Thread printingThread = new Thread(PrintKillsInIntervals)
            {
                Name = "Printer",
                IsBackground = true
            };
            printingThread.Start();

            /*Thread directoryReaderThread = new Thread(() => Startup(reader))
            {
                Name = "DirectoryReader",
                IsBackground = true
            };
            directoryReaderThread.Start();*/
        }

        /// <summary>
        /// Begins reading log files and starts printing thread and realtime reading thread when finished
        /// </summary>
        /// <param name="reader">JSON reader instance</param>
        /*private void Startup(JSONReaderSingleton reader)
        {
            reader.ReadDirectory();
            KillCounter.Text = "Scanning log files";
            Thread printingThread = new Thread(PrintKillsInIntervals)
            {
                Name = "Printer",
                IsBackground = true
            };

            Thread realTimeReaderThread = new Thread(reader.ReadLastJsonWhilePlaying)
            {
                Name = "RealTimeR",
                IsBackground = true
            };

            realTimeReaderThread.Start();
            printingThread.Start();
            
        }*/

        /// <summary>
        /// Print kills in intervals
        /// </summary>
        private void PrintKillsInIntervals()
        {
            Boolean endOfTheCycle = true;
            while (endOfTheCycle == true)
            {

                if (reader.counter.CheckKillChange() == true)
                {
                    KillCounterSetText(reader.counter.PrintAllKills());

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
    }
}
