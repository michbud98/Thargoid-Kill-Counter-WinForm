using System;
using System.Configuration;
using System.Windows.Forms;

namespace TKC
{
    /// <summary>
    /// Windows form class that handles user settings modification
    /// </summary>
    public partial class UserSettings : Form
    {
        public UserSettings()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads settings saved in app.config
        /// </summary>
        private void UserSettings_Load(object sender, EventArgs e)
        {
            JournalsDirTB.Text = ConfigurationManager.AppSettings["JournalsDirPath"];
            bool.TryParse(ConfigurationManager.AppSettings["ScreenShotsPermission"], out bool ScreenShotsPermission);
            ScreenShotsCheckBox.Checked = ScreenShotsPermission;
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
        /// Button that saves all settings and exits window
        /// </summary>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            bool change = false;
            string stringOfChanges = "";
            if (!ConfigurationManager.AppSettings["JournalsDirPath"].Equals(JournalsDirTB.Text))
            {
                ChangeConfig("JournalsDirPath", JournalsDirTB.Text);
                change = true;
                stringOfChanges += "Default directory changed \r\n";
            }
            if (!ConfigurationManager.AppSettings["ScreenShotsPermission"].Equals(ScreenShotsCheckBox.Checked.ToString()))
            {
                ChangeConfig("ScreenShotsPermission", ScreenShotsCheckBox.Checked.ToString());
                change = true;
                stringOfChanges += "Screenshots permission changed \r\n";
            }

            if (change == true)
            {
                MessageBox.Show($"{stringOfChanges}Changes will display after restart.");
            }
            this.Close();
        }

        private void JournalsDirButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                JournalsDirTB.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
