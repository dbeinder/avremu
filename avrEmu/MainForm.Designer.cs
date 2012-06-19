namespace avrEmu
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tsBtnReset = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsBtnManualStep = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tsBtnAutoRun = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsCboSpeed = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tsCboFormat = new System.Windows.Forms.ToolStripComboBox();
            this.rtbCode = new System.Windows.Forms.RichTextBox();
            this.pnlCode = new System.Windows.Forms.Panel();
            this.pbCodeIcons = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nudSramLength = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudStartSram = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.spcCodeSplitter = new System.Windows.Forms.SplitContainer();
            this.tcPorts = new System.Windows.Forms.TabControl();
            this.tbpCaption = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialogAsm = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogAsm = new System.Windows.Forms.SaveFileDialog();
            this.ebbvSreg = new avrEmu.ExtByteBitViewer();
            this.ebeIORegs = new avrEmu.ExtByteEditor();
            this.ebeWorkingRegs = new avrEmu.ExtByteEditor();
            this.ebeSram = new avrEmu.ExtByteEditor();
            this.toolStrip1.SuspendLayout();
            this.pnlCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCodeIcons)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSramLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartSram)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcCodeSplitter)).BeginInit();
            this.spcCodeSplitter.Panel1.SuspendLayout();
            this.spcCodeSplitter.Panel2.SuspendLayout();
            this.spcCodeSplitter.SuspendLayout();
            this.tcPorts.SuspendLayout();
            this.tbpCaption.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator,
            this.helpToolStripButton,
            this.tsBtnReset,
            this.toolStripSeparator3,
            this.toolStripLabel2,
            this.tsBtnManualStep,
            this.toolStripSeparator1,
            this.toolStripLabel3,
            this.tsBtnAutoRun,
            this.toolStripSeparator4,
            this.toolStripLabel1,
            this.tsCboSpeed,
            this.toolStripSeparator2,
            this.toolStripLabel4,
            this.tsCboFormat});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(672, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&New";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.helpToolStripButton.Text = "He&lp";
            this.helpToolStripButton.Click += new System.EventHandler(this.helpToolStripButton_Click);
            // 
            // tsBtnReset
            // 
            this.tsBtnReset.Image = global::avrEmu.Properties.Resources.control_repeat_blue;
            this.tsBtnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnReset.Name = "tsBtnReset";
            this.tsBtnReset.Size = new System.Drawing.Size(55, 22);
            this.tsBtnReset.Text = "Reset";
            this.tsBtnReset.Click += new System.EventHandler(this.tsBtnReset_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(45, 22);
            this.toolStripLabel2.Text = "Manual:";
            // 
            // tsBtnManualStep
            // 
            this.tsBtnManualStep.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnManualStep.Image = global::avrEmu.Properties.Resources.control_end_blue;
            this.tsBtnManualStep.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnManualStep.Name = "tsBtnManualStep";
            this.tsBtnManualStep.Size = new System.Drawing.Size(23, 22);
            this.tsBtnManualStep.Text = "toolStripButton2";
            this.tsBtnManualStep.Click += new System.EventHandler(this.tsBtnManualStep_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(59, 22);
            this.toolStripLabel3.Text = "Automatic:";
            // 
            // tsBtnAutoRun
            // 
            this.tsBtnAutoRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnAutoRun.Image = global::avrEmu.Properties.Resources.control_play_blue;
            this.tsBtnAutoRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnAutoRun.Name = "tsBtnAutoRun";
            this.tsBtnAutoRun.Size = new System.Drawing.Size(23, 22);
            this.tsBtnAutoRun.Text = "toolStripButton3";
            this.tsBtnAutoRun.Click += new System.EventHandler(this.tsBtnAutoRun_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(41, 22);
            this.toolStripLabel1.Text = "Speed:";
            // 
            // tsCboSpeed
            // 
            this.tsCboSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsCboSpeed.DropDownWidth = 75;
            this.tsCboSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tsCboSpeed.Name = "tsCboSpeed";
            this.tsCboSpeed.Size = new System.Drawing.Size(75, 25);
            this.tsCboSpeed.SelectedIndexChanged += new System.EventHandler(this.tsCboSpeed_SelectedIndexChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(85, 22);
            this.toolStripLabel4.Text = "Number Format:";
            // 
            // tsCboFormat
            // 
            this.tsCboFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsCboFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tsCboFormat.Name = "tsCboFormat";
            this.tsCboFormat.Size = new System.Drawing.Size(90, 25);
            this.tsCboFormat.SelectedIndexChanged += new System.EventHandler(this.tsCboFormat_SelectedIndexChanged);
            // 
            // rtbCode
            // 
            this.rtbCode.AcceptsTab = true;
            this.rtbCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbCode.DetectUrls = false;
            this.rtbCode.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbCode.HideSelection = false;
            this.rtbCode.Location = new System.Drawing.Point(16, 3);
            this.rtbCode.Name = "rtbCode";
            this.rtbCode.Size = new System.Drawing.Size(631, 190);
            this.rtbCode.TabIndex = 3;
            this.rtbCode.Text = resources.GetString("rtbCode.Text");
            this.rtbCode.WordWrap = false;
            this.rtbCode.VScroll += new System.EventHandler(this.rtbCode_VScroll);
            this.rtbCode.TextChanged += new System.EventHandler(this.rtbCode_TextChanged);
            this.rtbCode.Resize += new System.EventHandler(this.rtbCode_VScroll);
            // 
            // pnlCode
            // 
            this.pnlCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCode.BackColor = System.Drawing.SystemColors.Window;
            this.pnlCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCode.Controls.Add(this.rtbCode);
            this.pnlCode.Controls.Add(this.pbCodeIcons);
            this.pnlCode.Location = new System.Drawing.Point(12, 11);
            this.pnlCode.Name = "pnlCode";
            this.pnlCode.Size = new System.Drawing.Size(648, 195);
            this.pnlCode.TabIndex = 4;
            // 
            // pbCodeIcons
            // 
            this.pbCodeIcons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pbCodeIcons.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.pbCodeIcons.Location = new System.Drawing.Point(0, 0);
            this.pbCodeIcons.Name = "pbCodeIcons";
            this.pbCodeIcons.Size = new System.Drawing.Size(16, 197);
            this.pbCodeIcons.TabIndex = 2;
            this.pbCodeIcons.TabStop = false;
            this.pbCodeIcons.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCodeIcons_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.ebeWorkingRegs);
            this.groupBox1.Location = new System.Drawing.Point(12, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 165);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Working Registers";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.nudSramLength);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.nudStartSram);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.ebeSram);
            this.groupBox2.Location = new System.Drawing.Point(223, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(226, 165);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Random Access Memory";
            // 
            // nudSramLength
            // 
            this.nudSramLength.Location = new System.Drawing.Point(155, 17);
            this.nudSramLength.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nudSramLength.Name = "nudSramLength";
            this.nudSramLength.Size = new System.Drawing.Size(40, 20);
            this.nudSramLength.TabIndex = 10;
            this.nudSramLength.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudSramLength.ValueChanged += new System.EventHandler(this.nudSramLength_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Length:";
            // 
            // nudStartSram
            // 
            this.nudStartSram.Hexadecimal = true;
            this.nudStartSram.Location = new System.Drawing.Point(70, 17);
            this.nudStartSram.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.nudStartSram.Name = "nudStartSram";
            this.nudStartSram.Size = new System.Drawing.Size(40, 20);
            this.nudStartSram.TabIndex = 8;
            this.nudStartSram.Value = new decimal(new int[] {
            110,
            0,
            0,
            0});
            this.nudStartSram.ValueChanged += new System.EventHandler(this.nudStartSram_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Start Adress:";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.ebeIORegs);
            this.groupBox3.Location = new System.Drawing.Point(455, 52);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(205, 165);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Peripheral Registers";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.ebbvSreg);
            this.groupBox4.Location = new System.Drawing.Point(12, -1);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(648, 47);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Status Register";
            // 
            // spcCodeSplitter
            // 
            this.spcCodeSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcCodeSplitter.Location = new System.Drawing.Point(0, 25);
            this.spcCodeSplitter.Name = "spcCodeSplitter";
            this.spcCodeSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcCodeSplitter.Panel1
            // 
            this.spcCodeSplitter.Panel1.Controls.Add(this.pnlCode);
            this.spcCodeSplitter.Panel1MinSize = 150;
            // 
            // spcCodeSplitter.Panel2
            // 
            this.spcCodeSplitter.Panel2.Controls.Add(this.tcPorts);
            this.spcCodeSplitter.Panel2.Controls.Add(this.groupBox4);
            this.spcCodeSplitter.Panel2.Controls.Add(this.groupBox3);
            this.spcCodeSplitter.Panel2.Controls.Add(this.groupBox1);
            this.spcCodeSplitter.Panel2.Controls.Add(this.groupBox2);
            this.spcCodeSplitter.Panel2MinSize = 170;
            this.spcCodeSplitter.Size = new System.Drawing.Size(672, 537);
            this.spcCodeSplitter.SplitterDistance = 216;
            this.spcCodeSplitter.TabIndex = 10;
            // 
            // tcPorts
            // 
            this.tcPorts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tcPorts.Controls.Add(this.tbpCaption);
            this.tcPorts.Location = new System.Drawing.Point(12, 223);
            this.tcPorts.Name = "tcPorts";
            this.tcPorts.SelectedIndex = 0;
            this.tcPorts.Size = new System.Drawing.Size(648, 91);
            this.tcPorts.TabIndex = 10;
            this.tcPorts.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tcPorts_Selecting);
            // 
            // tbpCaption
            // 
            this.tbpCaption.Controls.Add(this.label3);
            this.tbpCaption.Location = new System.Drawing.Point(4, 22);
            this.tbpCaption.Name = "tbpCaption";
            this.tbpCaption.Padding = new System.Windows.Forms.Padding(3);
            this.tbpCaption.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbpCaption.Size = new System.Drawing.Size(640, 65);
            this.tbpCaption.TabIndex = 0;
            this.tbpCaption.Text = "I/O Ports";
            this.tbpCaption.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(302, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "This displays the current state of every pin of the selected port.";
            // 
            // openFileDialogAsm
            // 
            this.openFileDialogAsm.Filter = "Assembler Code|*.asm|All Files|*.*";
            // 
            // saveFileDialogAsm
            // 
            this.saveFileDialogAsm.Filter = "Assembler Code|*.asm|All Files|*.*";
            // 
            // ebbvSreg
            // 
            this.ebbvSreg.Location = new System.Drawing.Point(8, 16);
            this.ebbvSreg.Name = "ebbvSreg";
            this.ebbvSreg.Size = new System.Drawing.Size(550, 16);
            this.ebbvSreg.TabIndex = 0;
            this.ebbvSreg.WatchedByte = null;
            // 
            // ebeIORegs
            // 
            this.ebeIORegs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ebeIORegs.DescriptionText = "Name";
            this.ebeIORegs.DisplayFormat = avrEmu.NumberFormat.Hexadecimal;
            this.ebeIORegs.Location = new System.Drawing.Point(6, 19);
            this.ebeIORegs.Name = "ebeIORegs";
            this.ebeIORegs.Size = new System.Drawing.Size(193, 136);
            this.ebeIORegs.TabIndex = 6;
            // 
            // ebeWorkingRegs
            // 
            this.ebeWorkingRegs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ebeWorkingRegs.DescriptionText = "Register";
            this.ebeWorkingRegs.DisplayFormat = avrEmu.NumberFormat.Hexadecimal;
            this.ebeWorkingRegs.Location = new System.Drawing.Point(6, 19);
            this.ebeWorkingRegs.Name = "ebeWorkingRegs";
            this.ebeWorkingRegs.Size = new System.Drawing.Size(193, 136);
            this.ebeWorkingRegs.TabIndex = 6;
            // 
            // ebeSram
            // 
            this.ebeSram.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ebeSram.DescriptionText = "Adress";
            this.ebeSram.DisplayFormat = avrEmu.NumberFormat.Hexadecimal;
            this.ebeSram.Location = new System.Drawing.Point(6, 43);
            this.ebeSram.Name = "ebeSram";
            this.ebeSram.Size = new System.Drawing.Size(214, 112);
            this.ebeSram.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 562);
            this.Controls.Add(this.spcCodeSplitter);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.Text = "AVR Emulator";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlCode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCodeIcons)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSramLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartSram)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.spcCodeSplitter.Panel1.ResumeLayout(false);
            this.spcCodeSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcCodeSplitter)).EndInit();
            this.spcCodeSplitter.ResumeLayout(false);
            this.tcPorts.ResumeLayout(false);
            this.tbpCaption.ResumeLayout(false);
            this.tbpCaption.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolStripButton tsBtnManualStep;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsBtnAutoRun;
        private System.Windows.Forms.PictureBox pbCodeIcons;
        private System.Windows.Forms.RichTextBox rtbCode;
        private System.Windows.Forms.Panel pnlCode;
        private ExtByteEditor ebeWorkingRegs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nudSramLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudStartSram;
        private System.Windows.Forms.Label label1;
        private ExtByteEditor ebeSram;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private ExtByteEditor ebeIORegs;
        private System.Windows.Forms.GroupBox groupBox4;
        private ExtByteBitViewer ebbvSreg;
        private System.Windows.Forms.SplitContainer spcCodeSplitter;
        private System.Windows.Forms.OpenFileDialog openFileDialogAsm;
        private System.Windows.Forms.SaveFileDialog saveFileDialogAsm;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton tsBtnReset;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripComboBox tsCboFormat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripComboBox tsCboSpeed;
        private System.Windows.Forms.TabControl tcPorts;
        private System.Windows.Forms.TabPage tbpCaption;
        private System.Windows.Forms.Label label3;

    }
}