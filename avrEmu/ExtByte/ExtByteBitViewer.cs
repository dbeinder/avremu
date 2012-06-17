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
    public partial class ExtByteBitViewer : UserControl
    {
        private ExtByte watchedByte;

        public ExtByte WatchedByte
        {
            get { return this.watchedByte; }
            set
            {
                if (value == null)
                    return;

                this.watchedByte = value;
                flpContent.Controls.Clear();

                for (int i = 0; i < 8; i++)
                    flpContent.Controls.Add(new ExtByteBit(value, i));
            }
        }

        public ExtByteBitViewer()
        {
            InitializeComponent();
        }
    }
}
