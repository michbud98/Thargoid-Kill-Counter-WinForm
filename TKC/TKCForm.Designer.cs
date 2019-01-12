namespace TKC
{
    partial class TKCForm
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TKCForm));
            this.KillCounter = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // KillCounter
            // 
            this.KillCounter.AutoSize = true;
            this.KillCounter.Location = new System.Drawing.Point(20, 31);
            this.KillCounter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.KillCounter.Name = "KillCounter";
            this.KillCounter.Size = new System.Drawing.Size(35, 13);
            this.KillCounter.TabIndex = 0;
            this.KillCounter.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(183, 120);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 26);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Location = new System.Drawing.Point(29, 193);
            this.InfoLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(25, 13);
            this.InfoLabel.TabIndex = 2;
            this.InfoLabel.Text = "Info";
            // 
            // TKCForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 229);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.KillCounter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "TKCForm";
            this.Text = "Thargoid Kill Calculator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label KillCounter;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Label InfoLabel;
    }
}

