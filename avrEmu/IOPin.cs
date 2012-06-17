using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace avrEmu
{
    public partial class IOPin : UserControl
    {
        public bool Input { get; private set; }
        public bool High { get; set; }
        public int Number { get; set; }
        public bool Changed { get; set; }
        private CheckBox chkInput;

        public IOPin()
        {
            InitializeComponent();
        }

        void input_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInput.Checked)
            {
                this.pbBitStatus.Image = Properties.Resources.bullet_green;
            }
            else
            {
                this.pbBitStatus.Image = Properties.Resources.bullet_red;
            }
            this.Changed = !this.Changed;
        }

        private void IOPin_Load(object sender, EventArgs e)
        {

            if (this.Input)
            {
                labelIO.Text = "Input";
                chkInput = new CheckBox();
                chkInput.CheckState = CheckState.Checked;
                chkInput.Location = new Point(5, 15);
                chkInput.Text = "Set";
                this.Controls.Add(chkInput);
                chkInput.CheckedChanged += new EventHandler(input_CheckedChanged);
            }

            else
                labelIO.Text = "Output";

            if (this.High)
                this.pbBitStatus.Image = Properties.Resources.bullet_green;

            else
                this.pbBitStatus.Image = Properties.Resources.bullet_red;
            label1.Text = Convert.ToString(Number);
        }
    }
}
