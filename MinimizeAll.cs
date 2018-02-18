using System;
using System.Runtime.InteropServices;

namespace MinimizeAll
{
    public static class Minimizer
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);

        const int WM_COMMAND = 0x111;
        const int MIN_ALL = 419;
        const int MIN_ALL_UNDO = 416;
        private static readonly IntPtr lHwnd;

        static Minimizer()
        {
            lHwnd = FindWindow("Shell_TrayWnd", null);
        }

        /// <summary>
        /// Minimises all windows by finding the taskbar and sending it a message 
        /// </summary>
        public static void UndoMinimizeAll()
        {
            SendMessage(lHwnd, WM_COMMAND, (IntPtr)MIN_ALL_UNDO, IntPtr.Zero);
        }

        /// <summary>
        /// UnMinimises all windows by finding the taskbar and sending it a message 
        /// </summary>
        public static void MinimizeAll()
        {
            SendMessage(lHwnd, WM_COMMAND, (IntPtr)MIN_ALL, IntPtr.Zero);
        }
    }
}