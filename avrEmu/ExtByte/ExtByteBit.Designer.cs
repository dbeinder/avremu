namespace avrEmu
{
    partial class ExtByteBit
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
            this.lblCaption = new System.Windows.Forms.Label();
            this.pbBitStatus = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbBitStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Location = new System.Drawing.Point(16, 2);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(27, 13);
            this.lblCaption.TabIndex = 3;
            this.lblCaption.Text = "PB3";
            // 
            // pbBitStatus
            // 
            this.pbBitStatus.Image = global::avrEmu.Properties.Resources.bullet_red;
            this.pbBitStatus.Location = new System.Drawing.Point(0, 0);
            this.pbBitStatus.Name = "pbBitStatus";
            this.pbBitStatus.Size = new System.Drawing.Size(16, 16);
            this.pbBitStatus.TabIndex = 2;
            this.pbBitStatus.TabStop = false;
            // 
            // ExtByteBit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.pbBitStatus);
            this.Name = "ExtByteBit";
            this.Size = new System.Drawing.Size(60, 16);
            ((System.ComponentModel.ISupportInitialize)(this.pbBitStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.PictureBox pbBitStatus;
    }
}
