using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace avrEmu
{
    public partial class MainForm : Form
    {
        private const int IconLineDarkness = 24;
        private bool simulationInProgress = false;
        private List<int> errorLines = new List<int>() { };
        private AvrController at2313 = new AtTiny2313();
        private AvrPMFormLink memoryLink = new AvrPMFormLink();
        private Preprocessor prePro;
        private AvrIOPort iOPort = new AvrIOPort('A', 3);

        private Dictionary<string, int> possibleSpeeds = new Dictionary<string, int>()
        {
            { "¼ Hz", 4000 },
            { "½ Hz", 2000 },
            { "1 Hz", 1000 },
            { "2 Hz", 500 },
            { "4 Hz", 250 },
            { "8 Hz", 125 },
            { "16 Hz", 63 },
            { "32 Hz", 31 },
            { "64 Hz", 16 },
            { "128 Hz", 8 },
            { "256 Hz", 4 }
        };

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

            this.at2313.ProgramMemory = this.memoryLink; //use input from this form instead of fixed memory
            this.memoryLink.FetchInstruction += new AvrPMFormLink.FetchInstructionEventHandler(memoryLink_FetchInstruction);

            this.extByteEditors.AddRange(new ExtByteEditor[] //register editors, for format changing
            {
                this.ebeWorkingRegs,
                this.ebeSram,
                this.ebeIORegs
            });

            IOPort ioPort = new IOPort();
         (  (AtTiny2313) this.at2313).PortA.Pins[0].pin
            ioPort.pins.AddRange(iOPort.Pins);
            ioPort.Location = new Point(3, 3);
            flowLayoutPanel1.Controls.Add(ioPort);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            pbCodeIcons.BackColor = Color.FromArgb(rtbCode.BackColor.R - IconLineDarkness, rtbCode.BackColor.G - IconLineDarkness, rtbCode.BackColor.B - IconLineDarkness);
            bulletExec = Image.FromHbitmap(Properties.Resources.bullet_go.GetHbitmap(pbCodeIcons.BackColor));
            bulletError = Image.FromHbitmap(Properties.Resources.bullet_red.GetHbitmap(pbCodeIcons.BackColor));

            foreach (NumberFormat nf in this.possibleFormats)
                this.tsCboFormat.Items.Add(nf);
            this.tsCboFormat.SelectedIndex = 0;

            foreach (string speed in this.possibleSpeeds.Keys)
                this.tsCboSpeed.Items.Add(speed);
            this.tsCboSpeed.SelectedIndex = 3;

            this.ebbvSreg.WatchedByte = this.at2313.ALU.SREG;

            RegisterWorkingRegs();
            RegisterSram();
            RegisterIORegs();
            this.prePro = new Preprocessor(this.at2313.Constants);
        }

        private void rtbCode_TextChanged(object sender, EventArgs e)
        {
            if (simulationInProgress)
            {
                this.at2313.Reset();
                UpdateCodeIcons();
                simulationInProgress = false;
            }
        }

        private void UpdateCodeIcons()
        {
            this.pbCodeIcons.Invalidate();
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
            if (!simulationInProgress)
                return;

            int offset = 6 - rtbCode.Font.Height / 2;

            e.Graphics.DrawImage(this.bulletExec, 0,
                this.rtbCode.Font.Height *
                  this.prePro.LineMapping[this.at2313.ProgramCounter]);

            foreach (int errorPos in errorLines)
                e.Graphics.DrawImage(this.bulletError, 0, rtbCode.Font.Height * errorPos);
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
                lastAddres = this.at2313.SRAM.Memory.Length;

            for (int i = (int)nudStartSram.Value; i < lastAddres; i++)
            {
                this.ebeSram.RegisterByte(this.at2313.SRAM.Memory[i], "0x" + i.ToString("x2"));
            }
        }

        private void nudSramLength_ValueChanged(object sender, EventArgs e)
        {
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

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            (new AboutForm()).ShowDialog();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            if (rtbCode.Text == "" || MessageBox.Show("Clear all code in the box, and perform a reset?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                this.rtbCode.Text = "";
                this.at2313.Reset();
            }
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            if (openFileDialogAsm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.rtbCode.Text = File.ReadAllText(openFileDialogAsm.FileName);
                    this.at2313.Reset();
                }
                catch
                {
                    MessageBox.Show("Error while reading file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if (openFileDialogAsm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialogAsm.FileName, rtbCode.Text);
                }
                catch
                {
                    MessageBox.Show("Error while writing file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tsBtnReset_Click(object sender, EventArgs e)
        {
            this.at2313.Reset();
            this.simulationInProgress = false;
            UpdateCodeIcons();
        }

        private void tsCboSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tsBtnManualStep_Click(object sender, EventArgs e)
        {
            SimulationStep();
        }

        private void SimulationStep()
        {
            if (!simulationInProgress)
            {
                try
                {
                    this.prePro.Process(this.rtbCode.Text);
                    this.simulationInProgress = true;
                    UpdateCodeIcons();
                    return;
                }
                catch
                {
                    MessageBox.Show("Error while parsing code!");
                    return;
                }
            }

            this.at2313.ClockTick();
            UpdateCodeIcons();
        }

        void memoryLink_FetchInstruction(object sender, FetchInstructionEventArgs e)
        {
            e.Instruction = new AvrInstruction(prePro.ProcessedLines[e.InstructionNr]);
        }
    }
}