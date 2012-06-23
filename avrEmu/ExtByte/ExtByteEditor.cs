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

        protected Dictionary<ExtByte, int> watchedBytes = new Dictionary<ExtByte, int>();
        

        public ExtByteEditor()
        {
            InitializeComponent();
            lviContent_Resize(lviContent, new EventArgs());
        }
        
        public void RegisterByte(ExtByte extByte, string name)
        {
            this.watchedBytes.Add(extByte, lviContent.Items.Count);
            ListViewItem item = new ListViewItem(name);
            item.Tag = extByte;
            item.SubItems.Add(GetFormated(extByte));
            this.lviContent.Items.Add(item);
            extByte.ByteChanged += new ByteChangedEventHandler(extByte_ByteChanged);
        }
        
        public void Clear()
        {
            foreach (ExtByte eb in this.watchedBytes.Keys)
                eb.ByteChanged -= extByte_ByteChanged;

            this.watchedBytes.Clear();
            this.lviContent.Items.Clear();
        }


        #region Usercontrol Updates

        void extByte_ByteChanged(object sender, ByteChangedEventArgs e)
        {
            int nr = watchedBytes[e.ChangedByte];
            this.lviContent.Items[nr].SubItems[1].Text = GetFormated(e.ChangedByte);
        }

        protected void UpdateFormatting()
        {
            foreach (KeyValuePair<ExtByte, int> item in watchedBytes)
                this.lviContent.Items[item.Value].SubItems[1].Text = GetFormated(item.Key);
        }

        protected string GetFormated(ExtByte eb)
        {
            switch (this.displayFormat)
            {
                case NumberFormat.Unsigned:
                    return eb.Value.ToString();

                case NumberFormat.Signed:
                    return ((sbyte)(eb.Value)).ToString();

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

        private void lviContent_Resize(object sender, EventArgs e)
        {
            int availableWidth = lviContent.Width - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

            lviContent.Columns[0].Width = (int)(availableWidth * 0.4d) - 2;
            lviContent.Columns[1].Width = (int)(availableWidth * 0.6d) - 2;
        }

        #endregion

        #region Editing

        //when -1, show the old value
        private void StartEdit(int newValue)
        {
            ExtByte eb = this.lviContent.SelectedItems[0].Tag as ExtByte;
            ValueEdit ve = new ValueEdit(newValue == -1 ? eb.Value : newValue, 255, newValue == -1);

            if (ve.ShowDialog() == DialogResult.OK)
                eb.Value = (byte)ve.Value;
        }

        private void lviContent_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            StartEdit(-1);
        }

        [DllImport("user32.dll")]
        static extern int MapVirtualKey(uint uCode, uint uMapType);

        private void lviContent_KeyDown(object sender, KeyEventArgs e)
        {
            //when pressed key is a number, open the dialog and insert said number
            int firstNumber;

            if (int.TryParse(((char)MapVirtualKey((uint)e.KeyValue, 2)).ToString(), out firstNumber))
                StartEdit(firstNumber);
            else if (e.KeyCode == Keys.Enter)
                StartEdit(-1);
        }

        #endregion
    }
}
