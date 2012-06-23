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
    public partial class IOPort : UserControl
    {
        protected AvrIOPort avrPort;

        public AvrIOPort AvrPort
        {
            get { return this.AvrPort; }

            set
            {
                if (value == null)
                    return;

                this.flpPins.Controls.Clear();
                this.avrPort = value;

                this.SuspendLayout();
                for (int i = 0; i < value.PinCount; i++)
                {
                    IOPin pin = new IOPin();
                    pin.AvrPin = value.Pins[i];
                    this.flpPins.Controls.Add(pin);
                }
                this.ResumeLayout();
            }
        }

        public IOPort()
        {
            InitializeComponent();
        }
    }
}
