namespace avrEmu
{
    partial class ExtByteEditor
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
            this.lviContent = new System.Windows.Forms.ListView();
            this.Desc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lviContent
            // 
            this.lviContent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Desc,
            this.Value});
            this.lviContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lviContent.FullRowSelect = true;
            this.lviContent.GridLines = true;
            this.lviContent.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lviContent.HideSelection = false;
            this.lviContent.Location = new System.Drawing.Point(0, 0);
            this.lviContent.MultiSelect = false;
            this.lviContent.Name = "lviContent";
            this.lviContent.Size = new System.Drawing.Size(150, 150);
            this.lviContent.TabIndex = 0;
            this.lviContent.UseCompatibleStateImageBehavior = false;
            this.lviContent.View = System.Windows.Forms.View.Details;
            this.lviContent.Resize += new System.EventHandler(this.lviContent_Resize);
            // 
            // Desc
            // 
            this.Desc.Text = "Name";
            // 
            // Value
            // 
            this.Value.Text = "Value";
            // 
            // ExtByteEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lviContent);
            this.Name = "ExtByteEditor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lviContent;
        private System.Windows.Forms.ColumnHeader Desc;
        private System.Windows.Forms.ColumnHeader Value;
    }
}
