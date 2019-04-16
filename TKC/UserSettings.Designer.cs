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
            this.JournalsDirTB = new System.Windows.Forms.TextBox();
            this.JournalsDirLabel = new System.Windows.Forms.Label();
            this.JournalsDirButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // JournalsDirTB
            // 
            this.JournalsDirTB.Location = new System.Drawing.Point(12, 40);
            this.JournalsDirTB.Name = "JournalsDirTB";
            this.JournalsDirTB.Size = new System.Drawing.Size(322, 20);
            this.JournalsDirTB.TabIndex = 0;
            // 
            // JournalsDirLabel
            // 
            this.JournalsDirLabel.AutoSize = true;
            this.JournalsDirLabel.Location = new System.Drawing.Point(13, 21);
            this.JournalsDirLabel.Name = "JournalsDirLabel";
            this.JournalsDirLabel.Size = new System.Drawing.Size(116, 13);
            this.JournalsDirLabel.TabIndex = 1;
            this.JournalsDirLabel.Text = "Journals directory path:";
            // 
            // JournalsDirButton
            // 
            this.JournalsDirButton.Location = new System.Drawing.Point(259, 66);
            this.JournalsDirButton.Name = "JournalsDirButton";
            this.JournalsDirButton.Size = new System.Drawing.Size(75, 23);
            this.JournalsDirButton.TabIndex = 2;
            this.JournalsDirButton.Text = "Browse";
            this.JournalsDirButton.UseVisualStyleBackColor = true;
            // 
            // UserSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 201);
            this.Controls.Add(this.JournalsDirButton);
            this.Controls.Add(this.JournalsDirLabel);
            this.Controls.Add(this.JournalsDirTB);
            this.Name = "UserSettings";
            this.Text = "UserSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox JournalsDirTB;
        private System.Windows.Forms.Label JournalsDirLabel;
        private System.Windows.Forms.Button JournalsDirButton;
    }
}