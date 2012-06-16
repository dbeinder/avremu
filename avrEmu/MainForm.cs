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
    public partial class MainForm : Form
    {
        private const int IconLineDarkness = 24;
        private List<int> errorLines = new List<int>() { 2, 4, 12, 50 };
        private AvrController at2313 = new AtTiny2313();
        private AvrProgramMemory memoryLink = new AvrPMFormLink();
        private List<NumberFormat> possibleFormats = new List<NumberFormat>()
        {
            NumberFormat.Hexadecimal,
            NumberFormat.Unsigned,
            NumberFormat.Signed,
            NumberFormat.Binary
        };

        private List<ExtByteEditor> extByteEditors = new List<ExtByteEditor>();
        private Image bulletExec, bulletError;

        public MainForm()
        {
            InitializeComponent();
            at2313.ProgramMemory = this.memoryLink; //use input from this form instead of fixed memory

            this.extByteEditors.AddRange(new ExtByteEditor[] //register editors, for format changing
            {
                this.ebeWorkingRegs,
                this.ebeSram
            });
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            pbCodeIcons.BackColor = Color.FromArgb(rtbCode.BackColor.R - IconLineDarkness, rtbCode.BackColor.G - IconLineDarkness, rtbCode.BackColor.B - IconLineDarkness);
            bulletExec = Image.FromHbitmap(Properties.Resources.bullet_go.GetHbitmap(pbCodeIcons.BackColor));
            bulletError = Image.FromHbitmap(Properties.Resources.bullet_red.GetHbitmap(pbCodeIcons.BackColor));

            this.TopMost = true;

            foreach (NumberFormat nf in this.possibleFormats)
                this.tsCboFormat.Items.Add(nf);
            this.tsCboFormat.SelectedIndex = 0;


            RegisterWorkingRegs();
            RegisterSram();
            RegisterIORegs();
        }

        void richTextBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, 0, 0, 100, 100);
        }

        private void rtbCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void UpdateCodeIcons()
        {

        }

        private void RegisterWorkingRegs()
        {
            for (int i = 0; i < this.at2313.WorkingRegisters.Length; i++)
            {
                this.ebeWorkingRegs.RegisterByte(this.at2313.WorkingRegisters[i], "r" + i.ToString());
            }
        }

        private void pbCodeIcons_Paint(object sender, PaintEventArgs e)
        {
            int offset = 6 - rtbCode.Font.Height / 2;

            e.Graphics.DrawImage(bulletExec, 0, rtbCode.Font.Height * this.at2313.ProgramCounter);

            foreach (int errorPos in errorLines)
                e.Graphics.DrawImage(bulletError, 0, rtbCode.Font.Height * errorPos);
        }

        private void rtbCode_VScroll(object sender, EventArgs e)
        {
            int yOffset = rtbCode.GetPositionFromCharIndex(0).Y;
            int newIconLineHeight = pnlCode.Height - yOffset;
            if (pbCodeIcons.Height < newIconLineHeight)
                pbCodeIcons.Height = newIconLineHeight;
            pbCodeIcons.Top = yOffset;
        }

        private void nudStartSram_ValueChanged(object sender, EventArgs e)
        {
            this.ebeSram.Clear();
            RegisterSram();
        }

        private void RegisterSram()
        {
            int lastAddres = (int)(nudStartSram.Value + nudSramLength.Value);
            if (lastAddres >= this.at2313.SRAM.Memory.Length)
                lastAddres = this.at2313.SRAM.Memory.Length - 1;

            for (int i = (int)nudStartSram.Value; i < lastAddres; i++)
            {
                this.ebeSram.RegisterByte(this.at2313.SRAM.Memory[i], "0x" + i.ToString("x2"));
            }
        }

        private void nudSramLength_ValueChanged(object sender, EventArgs e)
        {
            this.at2313.WorkingRegisters[2].Value += 7; ///DEBUG!!!
            ebeSram.Clear();
            RegisterSram();
        }

        private void tsCboFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            NumberFormat selectedFormat = (NumberFormat)this.tsCboFormat.SelectedItem;

            foreach (ExtByteEditor ebe in this.extByteEditors)
                ebe.DisplayFormat = selectedFormat;
        }

        private void RegisterIORegs()
        {
            foreach (KeyValuePair<string, ExtByte> ioReg in this.at2313.PeripheralRegisters)
                ebeIORegs.RegisterByte(ioReg.Value, ioReg.Key);
        }

    }
}