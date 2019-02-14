using log4net.Appender;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace UltralogExportTool
{
    public class SystemTray
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //system tray
        private readonly string _systemDisplayName;
        private readonly NotifyIcon _systemTray;

        //to show hide console
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public SystemTray(string systemDisplayName)
        {
            _systemTray = new NotifyIcon();
            _systemDisplayName = systemDisplayName;
            InitializeSystemTray();

            if (!System.Diagnostics.Debugger.IsAttached)
            {
                Application.Run(); // when this runs the console appender stops strange...
            }

            if (Properties.Settings.Default.HideConsole)
            {
                ShowWindow(GetConsoleWindow(), SW_HIDE); // closes the console
            }
        }

        private void InitializeSystemTray()
        {
            _systemTray.Icon = new Icon(SystemIcons.Hand, 40, 40);
            _systemTray.Visible = true;
            _systemTray.BalloonTipTitle = _systemDisplayName;
            _systemTray.BalloonTipText = _systemDisplayName + " is running in the background";

            ContextMenu clickMenu = new ContextMenu();

            clickMenu.MenuItems.Add("HideConsole", (s, e) => ShowWindow(GetConsoleWindow(), SW_HIDE));
            clickMenu.MenuItems.Add("ShowConsole", (s, e) => ShowWindow(GetConsoleWindow(), SW_SHOW));
            clickMenu.MenuItems.Add("OpenLog", logfile_Click);
            clickMenu.MenuItems.Add("Exit", (s, e) => Application.Exit());

            _systemTray.ContextMenu = clickMenu;
            _systemTray.ShowBalloonTip(1000);
        }


        private void logfile_Click(object sender, EventArgs e)
        {
            string logfile = log4net.LogManager.GetRepository()
                                .GetAppenders()
                                .OfType<FileAppender>()
                                .FirstOrDefault().File;
            try
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                process.StartInfo = startInfo;
                startInfo.FileName = logfile;
                process.Start();
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show($"Was not able to open the log file. File should be on <{logfile}>", "File not found", MessageBoxButtons.OK);
                log.Error(ex);
            }

        }
    }

}
