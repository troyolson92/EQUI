using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EqUiDigibordLauncher
{
    public static class WinApi
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    }

    public static class Helper
    {
        /// <summary>
        /// Moves non-fullscreen window to specified monitor. (number starts from 1)
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="monitor">Monitor number, statrs from 1</param>
        public static bool MoveToMonitor(IntPtr windowHandle, int monitor)
        {
            monitor = monitor - 1;
            return WinApi.SetWindowPos(windowHandle, IntPtr.Zero, Screen.AllScreens[monitor].WorkingArea.Left,
                Screen.AllScreens[monitor].WorkingArea.Top, 1000, 800, SetWindowPosFlags.SWP_NOZORDER | SetWindowPosFlags.SWP_NOREDRAW);
        }

        public static void SendKey(IntPtr windowHandle, string keys)
        {
            WinApi.SetForegroundWindow(windowHandle);
            SendKeys.SendWait(keys);
            SendKeys.Flush();
        }

    }

    public class ChromeTools
    {
        int ChromeStartDelay = 3000;
        string chromePath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
        string chromeArguments = "--new-window";

        public void LaunchChrome(string Url = "HTTP://EQUI", int MonitorNum = 1)
        {
            //start chrome with arguments
            Process.Start(chromePath, chromeArguments + " " + Url);
            //sleep
            System.Threading.Thread.Sleep(ChromeStartDelay);
            //get window
            IntPtr hWnd = IntPtr.Zero; // = (IntPtr)WinApi.FindWindow(null, "chrome");

            Process[] procsChrome = Process.GetProcessesByName("chrome");
            foreach (Process chrome in procsChrome)
            {
                // the chrome process must have a window
                if (chrome.MainWindowHandle == IntPtr.Zero)
                {
                    continue;
                }
                hWnd = chrome.MainWindowHandle;
            }
            //show window
            WinApi.ShowWindow(hWnd, ShowWindowCommands.Restore);
            //movetomonitor
            Helper.MoveToMonitor(hWnd, MonitorNum);
            //send key
            Helper.SendKey(hWnd, "{F11}");
            //sleep
            System.Threading.Thread.Sleep(ChromeStartDelay);
        }

        public void LaunchKiosk()
        {
            //launch kiosk
            Process.Start(chromePath, "--profile-directory=Default --app-id=afhcomalholahplbjhnmahkoekoijban");
            System.Threading.Thread.Sleep(ChromeStartDelay);
        }
    }
}
