using EQUICommunictionLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EqUiDigibordLauncher
{
    public partial class digiboardLauncher
    {
        public void RunLauncher()
        {

            Console.WriteLine("Hi I will start the screens for you");
            //
            ChromeTools chromeTools = new ChromeTools();
            //
            Console.WriteLine("Getting configuration for: " + Environment.UserDomainName + "\\" + Environment.UserName);
            //
            EQUICommunictionLib.ConnectionManager connectionManager = new ConnectionManager();
            string QRY = @"
SELECT L_Screens.Screen_num, L_Screens.id FROM GADATA.Volvo.L_Screens
LEFT JOIN GADATA.Volvo.L_users on L_users.id = L_Screens.[User_id]
WHERE L_users.username = '{0}' AND Screen_num > 0
order by screen_num 
";

            DataTable dt = connectionManager.RunQuery(string.Format(QRY, Environment.UserDomainName + "\\" + Environment.UserName));

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        Console.WriteLine("Launching to screenum: " + row.Field<int>("Screen_num") + " ScreenID: " + row.Field<int>("id"));
                        string url = string.Format("http://equi/user_management/UserScreens/RenderUserScreen?screenID={0}", row.Field<int>("id"));
                        Console.WriteLine("url: " + url);
                        chromeTools.LaunchChrome(url, row.Field<int>("Screen_num"));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("failed to launch screen");
                        Console.WriteLine(ex.Message);
                    }

                }
            }
            else
            {
                Console.WriteLine("database retunred no confiugration");
                System.Threading.Thread.Sleep(5000);
            }
            Console.WriteLine("Done");
            System.Threading.Thread.Sleep(5000);

            //Screenblock
            Console.WriteLine("Starting sleepblocker");
            PreventSleep();
            while (true)
            {
                Console.WriteLine("Leave me open I'll block the pc from faling asleep.");
                Console.ReadLine();
            }
        }

        //helpers for sleepblock
        [FlagsAttribute]
        public enum EXECUTION_STATE : uint
        {
            ES_SYSTEM_REQUIRED = 0x00000001,
            ES_DISPLAY_REQUIRED = 0x00000002,
            // Legacy flag, should not be used.
            // ES_USER_PRESENT   = 0x00000004,
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
        }

        public static class SleepUtil
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
        }

        static void PreventSleep()
        {
            if (SleepUtil.SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS
                | EXECUTION_STATE.ES_DISPLAY_REQUIRED
                | EXECUTION_STATE.ES_SYSTEM_REQUIRED
                | EXECUTION_STATE.ES_AWAYMODE_REQUIRED) == 0) //Away mode for Windows >= Vista
                SleepUtil.SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS
                    | EXECUTION_STATE.ES_DISPLAY_REQUIRED
                    | EXECUTION_STATE.ES_SYSTEM_REQUIRED); //Windows < Vista, forget away mode
        }
    }
}
