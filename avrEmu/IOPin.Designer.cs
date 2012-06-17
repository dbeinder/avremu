namespace avrEmu
{
    partial class IOPin
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.pbBitStatus = new System.Windows.Forms.PictureBox();
            this.labelIO = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbBitStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 0;
            // 
            // pbBitStatus
            // 
            this.pbBitStatus.Image = global::avrEmu.Properties.Resources.bullet_red;
            this.pbBitStatus.Location = new System.Drawing.Point(3, 35);
            this.pbBitStatus.Name = "pbBitStatus";
            this.pbBitStatus.Size = new System.Drawing.Size(16, 16);
            this.pbBitStatus.TabIndex = 3;
            this.pbBitStatus.TabStop = false;
            // 
            // labelIO
            // 
            this.labelIO.AutoSize = true;
            this.labelIO.Location = new System.Drawing.Point(4, 4);
            this.labelIO.Name = "labelIO";
            this.labelIO.Size = new System.Drawing.Size(0, 13);
            this.labelIO.TabIndex = 4;
            // 
            // IOPin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelIO);
            this.Controls.Add(this.pbBitStatus);
            this.Controls.Add(this.label1);
            this.Name = "IOPin";
            this.Size = new System.Drawing.Size(49, 54);
            this.Load += new System.EventHandler(this.IOPin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbBitStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbBitStatus;
        private System.Windows.Forms.Label labelIO;
    }
}
