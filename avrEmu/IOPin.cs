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
        protected AvrIOPin avrPin;

        public AvrIOPin AvrPin
        {
            get { return this.avrPin; }
            set
            {
                if (this.avrPin != null)
                    this.avrPin.PinChanged -= avrPin_PinChanged;

                this.avrPin = value;
                value.PinChanged += new BitChangedEventHandler(avrPin_PinChanged);
                UpdateData();
            }
        }


        public IOPin()
        {
            InitializeComponent();
        }


        void avrPin_PinChanged(object sender, BitChangedEventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            this.lblDesc.Text = "Pin " + this.avrPin.PinNumber.ToString() + ": " +
                (this.avrPin.IsOutput ? "Output" : "Input");

            this.chkInput.Visible = !this.avrPin.IsOutput;
            this.chkInput.Checked = this.avrPin.InputValue;
            
            this.lblOutput.Visible = this.avrPin.IsOutput;
            this.lblOutput.Text = this.avrPin.OutputValue ? "High" : "Low";

            SetIcon(this.avrPin.IsOutput ? this.avrPin.OutputValue : this.avrPin.InputValue);
        }

        private void SetIcon(bool value)
        {
            if (value)
                this.pbBitStatus.Image = Properties.Resources.bullet_green;
            else
                this.pbBitStatus.Image = Properties.Resources.bullet_red;
        }

        void chkInput_CheckedChanged(object sender, EventArgs e)
        {
            this.avrPin.InputValue = chkInput.Checked;
        }

    }
}
