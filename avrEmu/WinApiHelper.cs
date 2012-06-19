using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace avrEmu
{
    public enum ScrollBarType : uint
    {
        SbHorz = 0,
        SbVert = 1,
        SbCtl = 2,
        SbBoth = 3
    }

    public enum Message : uint
    {
        WM_VSCROLL = 0x0115
    }

    public enum ScrollBarCommands : uint
    {
        SB_THUMBPOSITION = 4
    }

    static class WinApiHelper
    {
        [DllImport("User32.dll")]
        private extern static int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("User32.dll")]
        private extern static int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);


        public static int GetVScrollPos(IntPtr handle)
        {
            return GetScrollPos(handle, (int)ScrollBarType.SbVert);
        }

        public static void SetVScrollPos(IntPtr handle, int offset)
        {
            uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)(offset << 16);
            SendMessage(handle, (int)Message.WM_VSCROLL, new IntPtr(wParam), new IntPtr(0));
        }
    }
}
