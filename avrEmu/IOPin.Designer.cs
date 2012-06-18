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
            this.lblDesc = new System.Windows.Forms.Label();
            this.chkInput = new System.Windows.Forms.CheckBox();
            this.lblOutput = new System.Windows.Forms.Label();
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
            this.pbBitStatus.Location = new System.Drawing.Point(28, 21);
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
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(4, 5);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(69, 13);
            this.lblDesc.TabIndex = 5;
            this.lblDesc.Text = "Pin 5: Output";
            // 
            // chkInput
            // 
            this.chkInput.AutoSize = true;
            this.chkInput.Location = new System.Drawing.Point(7, 38);
            this.chkInput.Name = "chkInput";
            this.chkInput.Size = new System.Drawing.Size(62, 17);
            this.chkInput.TabIndex = 6;
            this.chkInput.Text = "Voltage";
            this.chkInput.UseVisualStyleBackColor = true;
            this.chkInput.Visible = false;
            this.chkInput.CheckedChanged += new System.EventHandler(this.chkInput_CheckedChanged);
            // 
            // lblOutput
            // 
            this.lblOutput.Location = new System.Drawing.Point(0, 40);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(75, 16);
            this.lblOutput.TabIndex = 7;
            this.lblOutput.Text = "High";
            this.lblOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOutput.Visible = false;
            // 
            // IOPin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.chkInput);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.labelIO);
            this.Controls.Add(this.pbBitStatus);
            this.Controls.Add(this.label1);
            this.Name = "IOPin";
            this.Size = new System.Drawing.Size(74, 58);
            ((System.ComponentModel.ISupportInitialize)(this.pbBitStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbBitStatus;
        private System.Windows.Forms.Label labelIO;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.CheckBox chkInput;
        private System.Windows.Forms.Label lblOutput;
    }
}
