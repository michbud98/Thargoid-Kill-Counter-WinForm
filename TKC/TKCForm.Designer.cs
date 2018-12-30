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
            this.KillCounter = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // KillCounter
            // 
            this.KillCounter.AutoSize = true;
            this.KillCounter.Location = new System.Drawing.Point(30, 48);
            this.KillCounter.Name = "KillCounter";
            this.KillCounter.Size = new System.Drawing.Size(51, 20);
            this.KillCounter.TabIndex = 0;
            this.KillCounter.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(275, 184);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Location = new System.Drawing.Point(44, 297);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(37, 20);
            this.InfoLabel.TabIndex = 2;
            this.InfoLabel.Text = "Info";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 353);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.KillCounter);
            this.Name = "Form1";
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

