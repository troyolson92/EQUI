using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using EQUICommunictionLib;

using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;
using ABB.Robotics.Controllers.EventLogDomain;
using ABB.Robotics.Controllers.ConfigurationDomain;
using ABB.Robotics.Controllers.FileSystemDomain;
using ABB.Robotics.Controllers.IOSystemDomain;


namespace ABBCommTest
{
    public partial class Form1 : Form
    {

        private NetworkScanner scanner = new NetworkScanner();
        private Controller controller = null;
        private myDebugger debugger = new myDebugger();
        private GadataComm lgadatacomm = new GadataComm();
        private DataTable dt_robots;

        public Form1()
        {
            debugger.Init(@"c:\temp\ABBcomm.log");
            //
            InitializeComponent();
            //get greenfield list from GADATA
            dt_robots = lgadatacomm.RunQueryGadata(@"SELECT top 10 [Robotnaam]
                                                      ,[IP address]
                                                      ,[Subnetmask]
                                                      ,[Default Gateway]
                                                  FROM [GADATA].[dbo].[$GreenFieldRobots]
                                                  where robotnaam like '99090%'");
            //add colums for extra data
            dt_robots.Columns.Add("SystemId", System.Type.GetType("System.String"));
            dt_robots.Columns.Add("Availability", System.Type.GetType("System.String"));
            dt_robots.Columns.Add("IsVirtual", System.Type.GetType("System.String"));
            dt_robots.Columns.Add("SystemName", System.Type.GetType("System.String"));
            dt_robots.Columns.Add("Version", System.Type.GetType("System.String"));
            dt_robots.Columns.Add("ControllerName", System.Type.GetType("System.String"));
            dt_robots.Columns.Add("ConfIsOK", System.Type.GetType("System.String"));
            dt_robots.Columns.Add("autoOK", System.Type.GetType("System.String"));
            dt_robots.Columns.Add("ConnectOK", System.Type.GetType("System.String"));
            dt_robots.Columns.Add("ConfigOK", System.Type.GetType("System.String"));
            dt_robots.Columns.Add("restartOK", System.Type.GetType("System.String"));
            //link to datagrid
            dataGridView1.DataSource = dt_robots;
            //
        }

        private void LinkControllertoList(ControllerInfo controllerInfo)
        {
            foreach (DataRow row in dt_robots.Rows)
            {
                if (row.Field<string>("IP address").Trim() == controllerInfo.IPAddress.ToString().Trim())
                {
                    row["SystemId"] = controllerInfo.SystemId;
                    row["Availability"] = controllerInfo.Availability.ToString();
                    row["IsVirtual"] = controllerInfo.IsVirtual.ToString();
                    row["SystemName"] = controllerInfo.SystemName;
                    row["Version"] = controllerInfo.Version.ToString();
                    row["ControllerName"] = controllerInfo.ControllerName;
                }
            }
        }

        //expose a robot by IP to the networkscanner
        private void addRobotByIp(string Ip)
        {
            try
            {
                System.Net.IPAddress ipAddress;
                try
                {
                    ipAddress = System.Net.IPAddress.Parse(Ip);
                    NetworkScanner.AddRemoteController(ipAddress);
                }
                catch (FormatException ex)
                {
                    debugger.Exeption(ex);
                    debugger.Message("Wrong IP address format");
                }

                this.scanner.Scan();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //add controllers that are exposed to the scanner
        private void handleScanner()
        {
            scanner.Scan();
            ControllerInfoCollection controllers = scanner.Controllers;
            //populate the listview
            foreach (ControllerInfo controllerInfo in controllers)
            {
                LinkControllertoList(controllerInfo);
            }
            debugger.Message("done with scan");
        }

        //construct string to make CFG file to load paramaters.
        private string makeNFSConfig(string Robot, string UserId, string GroupId, string BasePath, string sharepath)
        {
            string nfsConfigScelaton = @"
SIO:CFG_1.0:6:1::

COM_APP:

      -Name ""robot_ga"" -Type ""NFS"" -Trp ""TCPIP1"" -ServerAddress ""10.249.2.103""\
      -Trusted 1 -LocalPath ""robot_ga:""\
      -ServerPath ""{0}{1}"" -UserID {2}\
      -GroupID {3}

";
            return string.Format(nfsConfigScelaton,BasePath,Robot,UserId,GroupId,sharepath);
            /*
             *       -Name ""share_ga"" -Type ""NFS"" -Trp ""TCPIP1"" -ServerAddress ""10.249.2.103""\
      -Trusted 1 -LocalPath ""share_ga:""\
      -ServerPath ""{4}{1}"" -UserID {2}\
      -GroupID {3}*/
        }

        //write configuration to robot
        private void NFSConfigureRobot(ControllerInfo ci, DataGridViewRow row)
        {
            string tempdir =  @"c:\temp\";
            string NFSFilePathOnControler = @"/hd0a/Param/";
            string NFSFilename = "NFS.CFG";

            string robotnaam = row.Cells[dataGridView1.Columns["Robotnaam"].Index].Value.ToString();
            string userID = "0"; //0 = root // row.Cells[dataGridView1.Columns["Robotnaam"].Index].Value.ToString();
            string GroupID = "1"; //1 = root  row.Cells[dataGridView1.Columns["Robotnaam"].Index].Value.ToString();
            try
            {
                //check file does not exist on local machine
                if (File.Exists(tempdir+NFSFilename)) { File.Delete(tempdir+NFSFilename); }
                //build config file on local machine 
                File.WriteAllText(tempdir + NFSFilename, makeNFSConfig(robotnaam, userID, GroupID, @"/ROBOTBCK/robot_ga/IRC5-NGAC/", @"/ROBOTBCK/IRC5-NGAC/IRC5-NGAC_SHARE/"));
            }
            catch (Exception ex)
            {
                debugger.Exeption(ex);
                debugger.Message("Create CNFG file error");
                return;
            }

            ConfigurationDatabase cfg;
            FileSystem cntrlFileSystem;

            try
            {
                if (ci.Availability != Availability.Available) { debugger.Message("controller busy: " + ci.Id); return; } //stop if controller is not available
                //
                controller = ControllerFactory.CreateFrom(ci); //get controller from factory
                if (controller.OperatingMode != ControllerOperatingMode.Auto) //controller must be on auto to take master 
                {
                    row.Cells[dataGridView1.Columns["AutoOK"].Index].Value = "NOK";
                    return;
                }
                else
                { 
                    row.Cells[dataGridView1.Columns["AutoOK"].Index].Value = "OK";
                }
                controller.Logon(UserInfo.DefaultUser); //logon to controller
                cfg = controller.Configuration; //get controller configruation database
                row.Cells[dataGridView1.Columns["ConnectOK"].Index].Value = "OK";
            }
            catch(Exception ex)
            {
                row.Cells[dataGridView1.Columns["ConnectOK"].Index].Value = "NOK";
                debugger.Exeption(ex);
                debugger.Message("Error connecting to controller");
                return;
            }
            try
            {
                //get controller mastership
                using (Mastership m = Mastership.Request(controller))
                {
                    cntrlFileSystem = controller.FileSystem;
                    controller.FileSystem.RemoteDirectory = NFSFilePathOnControler;
                    controller.FileSystem.LocalDirectory = tempdir;
                    //move file to controler
                    controller.FileSystem.PutFile(NFSFilename, NFSFilename, true);
                    //load file on controller
                    cfg.Load("ctrl:" + NFSFilePathOnControler + NFSFilename, LoadMode.Replace);
                    //check if controller if home for restart.
                    Signal O_Homepos = controller.IOSystem.GetSignal("O_Homepos");
                    if (O_Homepos.Value == 1 )
                    {
                        //RESTART !!!!!!!!!!!!!!!!!
                        controller.State = ControllerState.MotorsOff;
                        controller.Restart(ControllerStartMode.Warm);
                        //
                        row.Cells[dataGridView1.Columns["restartOK"].Index].Value = "OK";
                    }
                    else
                    {
                        row.Cells[dataGridView1.Columns["restartOK"].Index].Value = "NOK";
                    }

                    //release master
                    m.Release();
                    row.Cells[dataGridView1.Columns["ConfigOK"].Index].Value = "OK";
                }
            }
            catch (System.InvalidOperationException ex)
            {
                row.Cells[dataGridView1.Columns["ConfigOK"].Index].Value = "NOK";
                debugger.Exeption(ex);
                debugger.Message("error in write to controller");
                return;
            }
        }

        //check configuration of robot
        private bool checkNFSconfig(ControllerInfo ci, DataGridViewRow row)
        {
            this.controller = ControllerFactory.CreateFrom(ci);
            this.controller.Logon(UserInfo.DefaultUser);

            ConfigurationDatabase cfg = controller.Configuration;
            Domain sioDomain = controller.Configuration.SerialIO;

            // read parm to see if config was done
             string[] path = { "SIO", "COM_APP","robot_ga","ServerAddress" };
             string data = null;
             try { data = cfg.Read(path); }
             catch (Exception) { }
             if (data == "10.249.2.103")
             {
                 return true;
             }
             else
             {
                 return false;
             }
        }

        //bottons
        private void btn_scanNetwork_Click(object sender, EventArgs e)
        {
            handleScanner();
        }
        private void btn_writeNFS_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                //make sure dir exists for robot.
                string rootDir = @"\\gnlsnm0101\6308-APP-NASROBOTBCK0001\robot_ga\IRC5-NGAC\";
                if (!Directory.Exists(rootDir + row.Cells[dataGridView1.Columns["Robotnaam"].Index].Value.ToString()))
                {
                    Directory.CreateDirectory(rootDir + row.Cells[dataGridView1.Columns["Robotnaam"].Index].Value.ToString());
                }

                ControllerInfo ci;
                if (scanner.TryFind(new Guid(row.Cells[dataGridView1.Columns["SystemId"].Index].Value.ToString()), out ci))
                {
                    //check if robot needs config
                    if (checkNFSconfig(ci, row))
                    {
                       row.Cells[dataGridView1.Columns["ConfIsOK"].Index].Value = "OK";

                    }
                    else
                    {
                        row.Cells[dataGridView1.Columns["ConfIsOK"].Index].Value = "NOK->";
                        //load NFS config.
                        NFSConfigureRobot(ci, row);
                    }
                }
                else
                {
                    debugger.Message("can not find controller: " + row.Cells[0].Value.ToString());
                }
            }
            debugger.Message("done with controllers");

        }
        private void btn_addCtrl_Click(object sender, EventArgs e)
        {
            //adding for specifc ip 
            addRobotByIp(tbox_ip.Text);
        }
        private void btn_expose_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in dt_robots.Rows)
            {
                addRobotByIp(row.Field<string>("IP address").Trim());

            }
            debugger.Message("done with expose");
        }

        private void btnDirMap_Click(object sender, EventArgs e)
        {
            RobotBckShortcuts bckShort = new RobotBckShortcuts();
            bckShort.searchForRobots();
            bckShort.buildShortcutdirectory();
            debugger.Message("done with dirbuild");
        }

    }


}
