using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku
{
    public class Clicking
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData,
UIntPtr dwExtraInfo);
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;
        private static extern void mouse_event(
               UInt32 dwFlags, // motion and click options
               UInt32 dx, // horizontal position or change
               UInt32 dy, // vertical position or change
               UInt32 dwData, // wheel movement
               IntPtr dwExtraInfo // application-defined information
        );


        public static void SendClick()
        {

            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, new System.IntPtr());
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, new System.IntPtr());
        }




    }
}
