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

using System.Xml.Linq;


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
            dt_robots = lgadatacomm.RunQueryGadata(@"select * from gadata.ngac.c_controller where ip like '10.205%' and controller_name like '%99%'");
                                                   //  where CAST(SUBSTRING(controller_name,0,4) as int) between 351 and 359");
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
                if (row.Field<string>("IP") == controllerInfo.IPAddress.ToString())
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
                   // debugger.Message("Wrong IP format");
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

            string controller_name = row.Cells[dataGridView1.Columns["controller_name"].Index].Value.ToString();
            string userID = "0"; //0 = root // row.Cells[dataGridView1.Columns["controller_name"].Index].Value.ToString();
            string GroupID = "1"; //1 = root  row.Cells[dataGridView1.Columns["controller_name"].Index].Value.ToString();
            try
            {
                //check file does not exist on local machine
                if (File.Exists(tempdir+NFSFilename)) { File.Delete(tempdir+NFSFilename); }
                //build config file on local machine 
                File.WriteAllText(tempdir + NFSFilename, makeNFSConfig(controller_name, userID, GroupID, @"/ROBOTBCK/robot_ga/IRC5-NGAC/", @"/ROBOTBCK/IRC5-NGAC/IRC5-NGAC_SHARE/"));
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

        //check motionSub
        private bool checkMotionSubconfig(ControllerInfo ci, DataGridViewRow row)
        {
            this.controller = ControllerFactory.CreateFrom(ci);
            this.controller.Logon(UserInfo.DefaultUser);

            ConfigurationDatabase cfg = controller.Configuration;
            Domain sioDomain = controller.Configuration.SerialIO;
            Domain mocDomain = controller.Configuration.MotionControl;

            // read parm to see if config was done
            string[] path = { "MOC", "MOTION_SUP", "rob1", "path_col_detect_on" };
            string datapath = null;
            try { datapath = cfg.Read(path); }
            catch (Exception) { }
            //
            string[] jog = { "MOC", "MOTION_SUP", "rob1", "jog_col_detect_on" };
            string dataJog = null;
            try { dataJog = cfg.Read(jog); }
            catch (Exception) { }


            if (datapath == "TRUE" && dataJog == "TRUE")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //buttons
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
                if (!Directory.Exists(rootDir + row.Cells[dataGridView1.Columns["controller_name"].Index].Value.ToString()))
                {
                    Directory.CreateDirectory(rootDir + row.Cells[dataGridView1.Columns["controller_name"].Index].Value.ToString());
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
                         debugger.Message("OUT OF USE");
                        //NFSConfigureRobot(ci, row);
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
                addRobotByIp(row.Field<string>("IP"));

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

        //adding code to change XML file 
        //get pino config file name
        private string GetPino(Controller controller)
        {

            ConfigurationDatabase cfg = controller.Configuration;
            Domain sioDomain = controller.Configuration.SerialIO;

            // read parm to see if config was done
            string[] path = { "SIO", "INDUSTRIAL_NETWORK_USER", "NetworkUserConfig", "FileCfgName" };
            string data = null;
            try { data = cfg.Read(path); }
            catch (Exception) { }
            return data;
        }

        //get controller gateway
        private string GetGateway(Controller controller)
        {
            NetworkSettingsInfo nsi = controller.NetworkSettings;
            return nsi.Gateway.ToString();
        }

        private void btn_gateway_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                ControllerInfo ci;
                if (scanner.TryFind(new Guid(row.Cells[dataGridView1.Columns["SystemId"].Index].Value.ToString()), out ci))
                {
                   // debugger.Message("OUT OF USE");
                    PINOUPDATE(ci, row);     
                }
                else
                {
                    debugger.Message("can not find controller: " + row.Cells[0].Value.ToString());
                }
            }
            debugger.Message("done with controllers");
        }

        private void PINOUPDATE(ControllerInfo ci, DataGridViewRow row)
        {
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
                row.Cells[dataGridView1.Columns["ConnectOK"].Index].Value = "OK";
            }
            catch (Exception ex)
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
                    // get pino filename
                    string pinofilename = GetPino(controller);
                    //get controller gateway 
                    string controllergateway = GetGateway(controller);

                    //get file from controler to pc 
                    cntrlFileSystem = controller.FileSystem;
                    controller.FileSystem.RemoteDirectory = string.Format(@"/hd0a/{0}/HOME",controller.SystemName);
                    controller.FileSystem.LocalDirectory = @"c:\temp\";
                    //move file to pc
                    controller.FileSystem.GetFile(pinofilename, pinofilename, true);
                    //edit the pino file to the new gataway.
                    editPino(controllergateway, @"c:\temp\" + pinofilename);
                    //put the file back on the controller
                    controller.FileSystem.PutFile(pinofilename, pinofilename, true);
                    
                    
                    //check if controller if home for restart.
                    Signal O_Homepos = controller.IOSystem.GetSignal("O_Homepos");
                    if (O_Homepos.Value == 1)
                    {
                        //RESTART !!!!!!!!!!!!!!!!!
                        if (controller.State == ControllerState.MotorsOn)
                        {
                            controller.State = ControllerState.MotorsOff;
                        }
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

        private void editPino(string newGateway, string file)
        {
            string[] gateway = newGateway.Split('.');

            XDocument xmlDoc = XDocument.Load(file);

            var items = from item in xmlDoc.Descendants("Gate")
                        select item;

            foreach (XElement itemElement in items)
            {
                if (itemElement.Attribute("d1").Value != gateway[0].ToString() )
                {
                    debugger.Log(string.Format("File: {0} d1:{1} -> {2}", file, itemElement.Attribute("d1").Value, gateway[0].ToString()));
                    itemElement.SetAttributeValue("d1", gateway[0].ToString());
                }

                if (itemElement.Attribute("d2").Value != gateway[1].ToString())
                {
                    debugger.Log(string.Format("File: {0} d2:{1} -> {2}", file, itemElement.Attribute("d2").Value, gateway[1].ToString()));
                    itemElement.SetAttributeValue("d2", gateway[1].ToString());
                }

                if (itemElement.Attribute("d3").Value != gateway[2].ToString())
                {
                    debugger.Log(string.Format("File: {0} d3:{1} -> {2}", file, itemElement.Attribute("d3").Value, gateway[2].ToString()));
                    itemElement.SetAttributeValue("d3", gateway[2].ToString());
                }

                if (itemElement.Attribute("d4").Value != gateway[3].ToString())
                {
                    debugger.Log(string.Format("File: {0} d4:{1} -> {2}", file, itemElement.Attribute("d4").Value, gateway[3].ToString()));
                    itemElement.SetAttributeValue("d4", gateway[3].ToString());
                }
            }

            xmlDoc.Save(file);
        }


        //check of safemove config. Based on offline files from backups
        private void checkSafeMove()
        {
    


        }

        private void button1_Click(object sender, EventArgs e)
        {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    try
                    { 
                    ControllerInfo ci;
                    if (scanner.TryFind(new Guid(row.Cells[dataGridView1.Columns["SystemId"].Index].Value.ToString()), out ci))
                    {
                        this.controller = ControllerFactory.CreateFrom(ci);
                        this.controller.Logon(UserInfo.DefaultUser);

                        if (checkMotionSubconfig(ci, row))
                        {
                            row.Cells[dataGridView1.Columns["ConfIsOK"].Index].Value = "OK";
                        }
                        else
                        {
                            row.Cells[dataGridView1.Columns["ConfIsOK"].Index].Value = "NOK";
                        }

                    }
                    else
                    {
                        debugger.Message("can not find controller: " + row.Cells[0].Value.ToString());
                    }
                    }           
                    catch (Exception ex)
            {

            }


                }
            
            debugger.Message("done with controllers");
        }

    }


}
