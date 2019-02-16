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
        JSONReaderSingleton reader = JSONReaderSingleton.getInstance();
       
        private void Form1_Load(object sender, EventArgs e)
        {
            KillCounter.Text = reader.counter.PrintAllKills();
            Thread readingThread1 = new Thread(reader.ReadDirectory)
            {
                Name = "Directory log files reading",
                IsBackground = true
            };
            Thread readingThread2 = new Thread(reader.ReadLastJsonWhilePlaying)
            {
                Name = "Read while game running",
                IsBackground = true
            };

            Thread printingThread = new Thread(PrintKillsInIntervals)
            {
                Name = "Printing in intervals",
                IsBackground = true
            };
            readingThread1.Start();
            readingThread2.Start();
            printingThread.Start();
        }
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
