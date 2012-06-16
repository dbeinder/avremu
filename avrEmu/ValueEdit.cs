using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace avrEmu
{
    public partial class ValueEdit : Form
    {
        public int Value;

        public ValueEdit(int value, int max, bool select)
        {
            InitializeComponent();
            this.nudValue.Maximum = max;
            this.Value = value;

            if (select)
            {
                this.nudValue.Value = value;
                this.nudValue.Select(0, 5);
            }
            else
            {
                this.nudValue.Select(0, 5);
                SendKeys.Send(value.ToString());
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.Value = (int)this.nudValue.Value;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
