namespace TKC
{
    partial class Form1
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 353);
            this.Controls.Add(this.KillCounter);
            this.Name = "Form1";
            this.Text = "Thargoid Kill Calculator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label KillCounter;
    }
}

