namespace TKC
{
    partial class UserSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.JournalsDirLabel = new System.Windows.Forms.Label();
            this.JournalsDirTB = new System.Windows.Forms.TextBox();
            this.JournalsDirButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // JournalsDirLabel
            // 
            this.JournalsDirLabel.AutoSize = true;
            this.JournalsDirLabel.Location = new System.Drawing.Point(13, 13);
            this.JournalsDirLabel.Name = "JournalsDirLabel";
            this.JournalsDirLabel.Size = new System.Drawing.Size(113, 13);
            this.JournalsDirLabel.TabIndex = 0;
            this.JournalsDirLabel.Text = "Journals directory path";
            // 
            // JournalsDirTB
            // 
            this.JournalsDirTB.Location = new System.Drawing.Point(13, 30);
            this.JournalsDirTB.Name = "JournalsDirTB";
            this.JournalsDirTB.Size = new System.Drawing.Size(459, 20);
            this.JournalsDirTB.TabIndex = 1;
            // 
            // JournalsDirButton
            // 
            this.JournalsDirButton.Location = new System.Drawing.Point(397, 56);
            this.JournalsDirButton.Name = "JournalsDirButton";
            this.JournalsDirButton.Size = new System.Drawing.Size(75, 23);
            this.JournalsDirButton.TabIndex = 2;
            this.JournalsDirButton.Text = "Browse";
            this.JournalsDirButton.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(397, 224);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 3;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // UserSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 259);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.JournalsDirButton);
            this.Controls.Add(this.JournalsDirTB);
            this.Controls.Add(this.JournalsDirLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "UserSettings";
            this.Text = "UserSettings";
            this.Load += new System.EventHandler(this.UserSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label JournalsDirLabel;
        private System.Windows.Forms.TextBox JournalsDirTB;
        private System.Windows.Forms.Button JournalsDirButton;
        private System.Windows.Forms.Button SaveButton;
    }
}