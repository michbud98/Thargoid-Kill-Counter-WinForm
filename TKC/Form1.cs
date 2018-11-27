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
        private void button1_Click(object sender, EventArgs e)
        {
            r1.readDirectory();
            label1.Text = r1.printKills();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Click button1 to scan your log files";
            
        }
    }
}
