using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

class MyRichTextBox : RichTextBox
{
    private const int WM_PAINT = 15;
    protected override void WndProc(ref System.Windows.Forms.Message m)
    {
        base.WndProc(ref m);
        if (m.Msg == WM_PAINT && !inhibitPaint)
        {
            // raise the paint event
            using (Graphics graphic = base.CreateGraphics())
                OnPaint(new PaintEventArgs(graphic,
                 base.ClientRectangle));
        }

    }

    private bool inhibitPaint = false;

    public bool InhibitPaint
    {
        set { inhibitPaint = value; }
    }

    public event PaintEventHandler Paint;

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (this.Paint != null)
            Paint(this, e);
    }


}
