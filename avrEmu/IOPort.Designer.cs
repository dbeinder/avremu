﻿namespace avrEmu
{
    partial class IOPort
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
            this.flpPins = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flpPins
            // 
            this.flpPins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPins.Location = new System.Drawing.Point(0, 0);
            this.flpPins.Name = "flpPins";
            this.flpPins.Size = new System.Drawing.Size(209, 67);
            this.flpPins.TabIndex = 0;
            // 
            // IOPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.flpPins);
            this.Name = "IOPort";
            this.Size = new System.Drawing.Size(209, 67);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpPins;

    }
}
