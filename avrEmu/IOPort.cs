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
        public List<AvrIOPin> pins = new List<AvrIOPin>();
        public IOPort()
        {
            InitializeComponent();
            
            AvrIOPin pinf = new AvrIOPin();
            pinf.IsOutput = false;
            pinf.OutputValue=true;
            pinf.InputValue=true;
            pins.Add(pinf);
            AvrIOPin pinp = new AvrIOPin();
            pinp.IsOutput = true;
            pinp.OutputValue = true;
            pinp.InputValue = true;
            pins.Add(pinp);
            for (int i = 0; i < pins.Count; i++)
            {
                IOPin pin = new IOPin();
                pin.Input = pins[i].IsOutput;
                if (pins[i].IsOutput)
                    pin.High = pins[i].OutputValue;
                if (!pins[i].IsOutput)
                    pin.High = pins[i].InputValue;
                pin.Number = i;
                flowLayoutPanel1.Controls.Add(pin);
            }

        }
    }
}
