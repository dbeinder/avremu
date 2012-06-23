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
        private Timer autoSimTimer = new Timer();

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

            this.autoSimTimer.Tick += new EventHandler(autoSimTimer_Tick);
            this.autoSimTimer.Enabled = false;

            this.extByteEditors.AddRange(new ExtByteEditor[] //register editors, for format changing
            {
                this.ebeWorkingRegs,
                this.ebeSram,
                this.ebeIORegs
            });
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

            foreach (KeyValuePair<char, AvrIOPort> port in this.at2313.Ports)
            {
                TabPage tbPage = new TabPage("Port " + port.Key);
                tbPage.UseVisualStyleBackColor = true;
                IOPort portDisplay = new IOPort();
                tbPage.Controls.Add(portDisplay);
                portDisplay.AvrPort = port.Value;
                portDisplay.Dock = DockStyle.Fill;
                this.tcPorts.TabPages.Add(tbPage);
            }
            this.tcPorts.SelectedIndex = 1;

            RegisterWorkingRegs();
            RegisterSram();
            RegisterIORegs();
            this.prePro = new Preprocessor(this.at2313.Constants);
        }

        private void rtbCode_TextChanged(object sender, EventArgs e)
        {
            if (simulationInProgress)
            {
                StopAutoSim();
                this.at2313.Reset();
                UpdateCodeIcons();
                simulationInProgress = false;
            }
            rtbCode_VScroll(this.rtbCode, new EventArgs());
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
            if (pbCodeIcons.Top != yOffset)
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
            StopAutoSim();
            this.at2313.Reset();
            this.simulationInProgress = false;
            UpdateCodeIcons();
        }

        private void tsCboSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.autoSimTimer.Interval = this.possibleSpeeds[(string)this.tsCboSpeed.SelectedItem];
        }

        private void tsBtnManualStep_Click(object sender, EventArgs e)
        {
            SimulationStep();
        }

        void autoSimTimer_Tick(object sender, EventArgs e)
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

            if (this.at2313.ProgramCounter == this.prePro.ProcessedLines.Count)
                this.at2313.ProgramCounter = 0;

            UpdateCodeIcons();
        }

        void memoryLink_FetchInstruction(object sender, FetchInstructionEventArgs e)
        {
            e.Instruction = new AvrInstruction(prePro.ProcessedLines[e.InstructionNr]);
        }

        private void tcPorts_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == 0)
                e.Cancel = true;
        }

        private void tsBtnAutoRun_Click(object sender, EventArgs e)
        {
            if (this.autoSimTimer.Enabled)
                StopAutoSim();
            else
                StartAutoSim();
        }

        private void StartAutoSim()
        {
            this.tsBtnAutoRun.Image = Properties.Resources.control_pause_blue;
            this.autoSimTimer.Enabled = true;
        }

        private void StopAutoSim()
        {
            this.autoSimTimer.Enabled = false;
            this.tsBtnAutoRun.Image = Properties.Resources.control_play_blue;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F10:
                    tsBtnManualStep_Click(tsBtnManualStep, new EventArgs());
                    break;
                case Keys.F5:
                    tsBtnAutoRun_Click(tsBtnAutoRun, new EventArgs());
                    break;
                case Keys.F2:
                    tsBtnReset_Click(tsBtnReset, new EventArgs());
                    break;
                case (Keys.Control|Keys.O):
                    openToolStripButton_Click(openToolStripButton, new EventArgs());
                    break;
                case (Keys.Control | Keys.S):
                    saveToolStripButton_Click(saveToolStripButton, new EventArgs());
                    break;
                case (Keys.Control | Keys.N):
                    newToolStripButton_Click(newToolStripButton, new EventArgs());
                    break;
                default:
                    break;

            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}