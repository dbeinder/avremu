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
        private int pc = 7;
        private Image bulletExec, bulletError;
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            pbCodeIcons.BackColor = Color.FromArgb(rtbCode.BackColor.R - IconLineDarkness, rtbCode.BackColor.G - IconLineDarkness, rtbCode.BackColor.B - IconLineDarkness);
            bulletExec = Image.FromHbitmap(Properties.Resources.bullet_go.GetHbitmap(pbCodeIcons.BackColor));
            bulletError = Image.FromHbitmap(Properties.Resources.bullet_red.GetHbitmap(pbCodeIcons.BackColor));
        
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

        private void pbCodeIcons_Paint(object sender, PaintEventArgs e)
        {
            int offset = 6 - rtbCode.Font.Height / 2;

            e.Graphics.DrawImage(bulletExec, 0, rtbCode.Font.Height * pc);

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
    }
}