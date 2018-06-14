using System;
using EQUICommunictionLib;
using System.Data;

namespace EqUiDigibordLauncher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hi I will start the kiosk for you");
            //
            ChromeTools chromeTools = new ChromeTools();
            //start kiosk
            try
            {
                chromeTools.LaunchKiosk();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while starting KIOSK");
                Console.WriteLine(ex.Message);
                System.Threading.Thread.Sleep(5000);
            }
            //
            Console.WriteLine("Getting configuration for: "+ Environment.UserDomainName+"\\"+Environment.UserName);
            //
            EQUICommunictionLib.ConnectionManager connectionManager = new ConnectionManager();
            string QRY = @"
SELECT L_Screens.Screen_num, L_Screens.id FROM GADATA.Volvo.L_Screens
LEFT JOIN GADATA.Volvo.L_users on L_users.id = L_Screens.[User_id]
WHERE L_users.username = '{0}' AND Screen_num > 1
";

            DataTable dt = connectionManager.RunQuery(string.Format(QRY, Environment.UserDomainName + "\\" + Environment.UserName));

            if (dt.Rows.Count != 0 )
            {
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        Console.WriteLine("Launching to screenum: " + row.Field<int>("Screen_num")+ " ScreenID: " + row.Field<int>("id"));
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
        }
    }
}
