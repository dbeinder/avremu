using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace avrEmu
{
    public enum NumberFormat
    {
        Unsigned,
        Signed,
        Hexadecimal,
        Binary
    }

    public partial class ExtByteEditor : UserControl
    {

        protected NumberFormat displayFormat = NumberFormat.Hexadecimal;

        public NumberFormat DisplayFormat
        {
            get { return this.displayFormat; }
            set
            {
                this.displayFormat = value;
                UpdateFormatting();
            }
        }

        public string DescriptionText
        {
            get { return this.lviContent.Columns[0].Text; }
            set { this.lviContent.Columns[0].Text = value; }
        }


        protected List<ExtByte> watchedBytes = new List<ExtByte>();

        public ExtByteEditor()
        {
            InitializeComponent();
            lviContent_Resize(lviContent, new EventArgs());
        }

        private void lviContent_Resize(object sender, EventArgs e)
        {
            int availableWidth = lviContent.Width - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

            lviContent.Columns[0].Width = (int)(availableWidth * 0.4d) - 1;

            lviContent.Columns[1].Width = (int)(availableWidth * 0.6d) - 2;
        }

        public void RegisterByte(ExtByte extByte, string name)
        {
            this.watchedBytes.Add(extByte);
            ListViewItem item = new ListViewItem(name);
            item.Tag = extByte;
            item.SubItems.Add(GetFormated(watchedBytes.Count - 1));
            this.lviContent.Items.Add(item);
            extByte.ByteChanged += new ExtByte.ByteChangedEventHandler(extByte_ByteChanged);
        }

        void extByte_ByteChanged(object sender, ByteChangedEventArgs e)
        {
            int nr = watchedBytes.IndexOf(e.ChangedByte);
            this.lviContent.Items[nr].SubItems[1].Text = GetFormated(nr);
        }

        public bool UnregisterByte(ExtByte extByte)
        {
            int nr = watchedBytes.IndexOf(extByte);
            if (nr == -1)
                return false;

            this.lviContent.Items.RemoveAt(nr);
            this.watchedBytes.RemoveAt(nr);
            extByte.ByteChanged -= extByte_ByteChanged;
            return true;
        }

        protected string GetFormated(int nr)
        {
            ExtByte eb = this.watchedBytes[nr];

            switch (this.displayFormat)
            {
                case NumberFormat.Unsigned:
                    return eb.Value.ToString();

                case NumberFormat.Signed:
                    return ((sbyte)(eb.Value)).ToString(); //untested

                case NumberFormat.Hexadecimal:
                    return "0x" + eb.Value.ToString("x2");

                case NumberFormat.Binary:
                    return "0b" +
                       (eb[7] ? "1" : "0") +
                       (eb[6] ? "1" : "0") +
                       (eb[5] ? "1" : "0") +
                       (eb[4] ? "1" : "0") +
                       (eb[3] ? "1" : "0") +
                       (eb[2] ? "1" : "0") +
                       (eb[1] ? "1" : "0") +
                       (eb[0] ? "1" : "0");

                default:
                    return "Format not implemented";
            }
        }

        protected void UpdateFormatting()
        {
            for (int i = 0; i < watchedBytes.Count; i++)
            {
                this.lviContent.Items[i].SubItems[1].Text = GetFormated(i);
            }
        }

        public void Clear()
        {
            foreach (ExtByte eb in this.watchedBytes)
                eb.ByteChanged -= extByte_ByteChanged;

            this.watchedBytes.Clear();
            this.lviContent.Items.Clear();
        }

        private void lviContent_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            StartEdit(-1); //original value
        }

        private void StartEdit(int newValue)
        {
            ExtByte eb = this.lviContent.SelectedItems[0].Tag as ExtByte;

            ValueEdit ve = new ValueEdit(newValue == -1 ? eb.Value : newValue, 255, newValue == -1);
            ve.StartPosition = FormStartPosition.CenterParent;

            if (ve.ShowDialog() == DialogResult.OK)
                eb.Value = (byte)ve.Value;
        }

        [DllImport("user32.dll")]
        static extern int MapVirtualKey(uint uCode, uint uMapType);

        private void lviContent_KeyDown(object sender, KeyEventArgs e)
        {
            int firstNumber;

            if (int.TryParse(((char)MapVirtualKey((uint)e.KeyValue, 2)).ToString(), out firstNumber))
                StartEdit(firstNumber);
            else if (e.KeyCode == Keys.Enter)
                StartEdit(-1);
        }

        
    }
}
