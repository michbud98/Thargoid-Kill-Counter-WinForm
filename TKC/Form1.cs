using System;
using System.Windows.Forms;

namespace TKC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        JSONReaderSingleton r1 = JSONReaderSingleton.getInstance();
       
        private void Form1_Load(object sender, EventArgs e)
        {
            r1.ReadDirectory();
            KillCounter.Text = r1.printAllKills();
        }
    }
}
