using System;
using System.Threading;
using System.Windows.Forms;

namespace TKC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        JSONReaderSingleton reader = JSONReaderSingleton.getInstance();
       
        private void Form1_Load(object sender, EventArgs e)
        {
            reader.ReadDirectory();
            KillCounter.Text = reader.counter.PrintAllKills();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread readingThread = new Thread(reader.ReadLastJsonFileInRealTime)
            {
                Name = "Real Time Reading",
                IsBackground = true
            };

            Thread printingThread = new Thread(printKillsInIntervals)
            {
                Name = "Printing in Intervals",
                IsBackground = true
            };
            readingThread.Start();
            printingThread.Start();
        }

        private void printKillsInIntervals()
        {
            DateTime now = DateTime.Now;
            DateTime before = now;
            Boolean endOfTheCycle = true;
            while (endOfTheCycle == true)
            {
                now = DateTime.Now;
                if (now >= before && reader.counter.CheckKillChange() == true)
                {
                    before = now.AddSeconds(10);
                    Console.WriteLine("Printing kills");
                    KillCounterSetText(reader.counter.PrintAllKills());
                }else
                {
                    Thread.Sleep(10000);
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
