using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;

namespace OCR_WF_
{
    class screenshot
    {
        [DllImport("User32", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string IpClassName, string IpWindowName);

        [DllImport("user32")]
        private static extern IntPtr FindWindowEx(IntPtr hWnd1, int hWnd2, string lp1, string lp2);

        [DllImport("user32.dll")]
        internal static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcblt, int nFlags);
        public  Bitmap Capture()
        {

            IntPtr b = FindWindow(null, "BlueStacks");
            //녹스앱플레이어를 쓴다면
            //IntPtr c = FindWindowEx(b, 0, "Qt5QWindowIcon", "ScreenBoardClassWindow");

            Graphics gdata = Graphics.FromHwnd(b);

            Rectangle rect = Rectangle.Round(gdata.VisibleClipBounds);

            Bitmap bmp = new Bitmap(rect.Width, rect.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                IntPtr hdc = g.GetHdc();
                PrintWindow(b, hdc, 0x2);
                g.ReleaseHdc(hdc);
            }
            return bmp;
        }
    }
}
