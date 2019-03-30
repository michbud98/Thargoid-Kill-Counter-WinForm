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
            this.SuspendLayout();
            // 
            // KillCounter
            // 
            this.KillCounter.AutoSize = true;
            this.KillCounter.Location = new System.Drawing.Point(11, 9);
            this.KillCounter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.KillCounter.Name = "KillCounter";
            this.KillCounter.Size = new System.Drawing.Size(109, 130);
            this.KillCounter.TabIndex = 0;
            this.KillCounter.Text = "Thargoid Combat Kills\r\n------------------------\r\nScouts: 0\r\nCyclops: 0\r\nBasillisk" +
    ": 0\r\nMedusa: 0\r\nHydra: 0\r\nUnknown: 0\r\n------------------------\r\nTotal: 0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(168, 116);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TKCForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 161);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.KillCounter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimizeBox = false;
            this.Name = "TKCForm";
            this.Text = "Thargoid Kill Counter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label KillCounter;
        private System.Windows.Forms.Button button1;
    }
}

