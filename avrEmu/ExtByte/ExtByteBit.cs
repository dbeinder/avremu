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
    public partial class ExtByteBit : UserControl
    {
        private ExtByte extByte;

        public ExtByteBit(ExtByte eb, int bit)
        {
            InitializeComponent();

            this.extByte = eb;
            if (eb.BitNames.Count > bit)
                lblCaption.Text = eb.BitNames[bit];
            else
                lblCaption.Text = "B" + bit.ToString();

            eb.BitEvents[bit].BitChanged += new ExtByte.BitChangedEventHandler(ExtByteBit_BitChanged);
            UpdateIcon(eb[bit]);
        }

        public ExtByteBit(ExtByte eb, string bitName)
        {
            InitializeComponent();


            if (!eb.BitNames.Contains(bitName))
                throw new Exception("Bit Name not found");

            this.extByte = eb;
            lblCaption.Text = bitName;
            eb.BitEvents[bitName].BitChanged += new ExtByte.BitChangedEventHandler(ExtByteBit_BitChanged);
            UpdateIcon(eb[bitName]);
        }

        void ExtByteBit_BitChanged(object sender, BitChangedEventArgs e)
        {
            UpdateIcon(e.NewValue);
        }

        private void UpdateIcon(bool value)
        {
            if (value)
                this.pbBitStatus.Image = Properties.Resources.bullet_green;
            else
                this.pbBitStatus.Image = Properties.Resources.bullet_red;
        }
    }
}
