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
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            richTextBox1.InhibitPaint = false;
            richTextBox1.Paint += new PaintEventHandler(richTextBox1_Paint);
            richTextBox1.SelectionIndent = 10;
            

        }

        void richTextBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, 0, 0, 100, 100);
        }
    }
}