using System;
using System.Collections.Generic;
using System.IO;
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
            r1.readDirectory(@"C:\Users\Michal Budík\Saved Games\Frontier Developments\Elite Dangerous");
            label1.Text = r1.printKills();
        }
    }
}
