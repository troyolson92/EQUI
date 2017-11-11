﻿using System;
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

        int COUNT = 0;

        public Form1()
        {
            debugger.Init(@"c:\temp\ABBcomm.log");
            //
            InitializeComponent();
            //get greenfield list from GADATA
            dt_robots = lgadatacomm.RunQueryGadata(@"select * from gadata.ngac.c_controller where assetnum like 'URA%' AND CONTROLLER_NAME LIKE '339%'"); //); //and controller_name like '%99%'");
                                                   
            //add colums for extra data
            dt_robots.Columns.Add("SystemId", System.Type.GetType("System.String"));
           // dt_robots.Columns.Add("Availability", System.Type.GetType("System.String"));
           // dt_robots.Columns.Add("IsVirtual", System.Type.GetType("System.String"));
           // dt_robots.Columns.Add("SystemName", System.Type.GetType("System.String"));
           // dt_robots.Columns.Add("Version", System.Type.GetType("System.String"));
            dt_robots.Columns.Add("ControllerName", System.Type.GetType("System.String"));
           // dt_robots.Columns.Add("ConfIsOK", System.Type.GetType("System.String"));
            dt_robots.Columns.Add("autoOK", System.Type.GetType("System.String"));
            dt_robots.Columns.Add("Version", System.Type.GetType("System.String"));
            dt_robots.Columns.Add("OKtoLoad", System.Type.GetType("System.String"));
            dt_robots.Columns.Add("ConnectOK", System.Type.GetType("System.String"));
            //  dt_robots.Columns.Add("ConfigOK", System.Type.GetType("System.String"));
            // dt_robots.Columns.Add("restartOK", System.Type.GetType("System.String"));
               dt_robots.Columns.Add("LoadOK", System.Type.GetType("System.String"));
          //  dt_robots.Columns.Add("HasTipneed", System.Type.GetType("System.String"));
           // dt_robots.Columns.Add("HasTipneedComment", System.Type.GetType("System.String"));
          //  dt_robots.Columns.Add("Found", System.Type.GetType("System.String"));
          //  dt_robots.Columns.Add("Deleted", System.Type.GetType("System.String"));
          //  dt_robots.Columns.Add("Exeption", System.Type.GetType("System.String"));

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
               //     row["Availability"] = controllerInfo.Availability.ToString();
               //     row["IsVirtual"] = controllerInfo.IsVirtual.ToString();
               //     row["SystemName"] = controllerInfo.SystemName;
               //     row["Version"] = controllerInfo.Version.ToString();
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

        //change socket config in autorun. to robot
        private void SocketConfigureRobot(ControllerInfo ci, DataGridViewRow row)
        {
            string tempdir = @"c:\temp\debug\";
            string FilePathOnControler = @"/hd0a/TEMP/";

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

                    // get modules from controller task trob1
                    ABB.Robotics.Controllers.RapidDomain.Task tRob1 = controller.Rapid.GetTask("T_ROB1");
                    Module[] mx  = tRob1.GetModules();
                    //find the one we need 
                    foreach (Module m in mx)
                    {
                        if (m.Name.StartsWith("LR",StringComparison.InvariantCulture))
                        {
                            Routine proc = m.GetRoutine("InitUser");
                            if (proc != null) //check if we have the right module
                            {
                                //find the module on the controller and get it **************************************
                                    //save it on the controller
                                    m.SaveToFile(FilePathOnControler);
                                    System.Threading.Thread.Sleep(1000);
                                    //get file from controler to pc 
                                    FileSystem cntrlFileSystem;
                                    cntrlFileSystem = controller.FileSystem;
                                    controller.FileSystem.RemoteDirectory = FilePathOnControler;
                                    controller.FileSystem.LocalDirectory = tempdir;
                                    //move file to pc
                                    controller.FileSystem.GetFile(m.Name + ".mod", m.Name + ".mod", true);
                                //process the file******************************************************************
                                    string lrModule = File.ReadAllText(tempdir + m.Name + ".mod");
                                    lrModule = lrModule.Replace("bUseSocket", "!bUseSocket");
                                    lrModule = lrModule.Replace("bBodyIDActive", "!bBodyIDActive");
                                    File.WriteAllText(tempdir + m.Name + ".mod", lrModule);
                                //put the file back*****************************************************************
                                    try
                                    {
                                        //get controller mastership
                                        using (Mastership master = Mastership.Request(controller))
                                        {

                                            //put the file back on the controller
                                            controller.FileSystem.PutFile(m.Name + ".mod", m.Name + ".mod", true);


                                            //check if controller if home for load.
                                            Signal O_Homepos = controller.IOSystem.GetSignal("O_Homepos");
                                            if (O_Homepos.Value == 1)
                                            {
                                                tRob1.LoadModuleFromFile(FilePathOnControler + m.Name + ".mod", RapidLoadMode.Replace);
                                                row.Cells[dataGridView1.Columns["LoadOK"].Index].Value = "OK";
                                            }
                                            else
                                            {
                                                row.Cells[dataGridView1.Columns["LoadOK"].Index].Value = "NOK";           
                                            }

                                            //release master
                                            master.Release();
                                        }
                                    }
                                    catch (System.InvalidOperationException ex)
                                    {
                                        debugger.Exeption(ex);
                                        debugger.Message("error in write to controller");
                                        return;
                                    }
                            }
                      
                        }

                    }
                }
                row.Cells[dataGridView1.Columns["ConnectOK"].Index].Value = "OK";   
        }     
            catch (Exception ex)
            {
                row.Cells[dataGridView1.Columns["ConnectOK"].Index].Value = "NOK";
                debugger.Exeption(ex);
                debugger.Message("Error connecting to controller");
                return;
            }
        }


        //load new version of lrobot.
        private void LoadNewLrobotRobot(ControllerInfo ci, DataGridViewRow row)
        {
            string tempdir = @"c:\temp\debug\";
            string FilePathOnControler = @"/hd0a/TEMP/";
            string modulename = "LRobot.sys";
            string refVar = "Version_LRobot";

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


                    //check current version 
                    RapidData rd = controller.Rapid.GetRapidData("T_ROB1", modulename.Split('.')[0], refVar);
                    row.Cells[dataGridView1.Columns["Version"].Index].Value = rd.Value.ToString();

                   // return; // break for testing .

                    if (rd.Value.ToString().Contains("ABB 6V99 - 2017-11-02"))
                    {

                        //put the file on the controller*****************************************************************
                        FileSystem cntrlFileSystem;
                        cntrlFileSystem = controller.FileSystem;
                        controller.FileSystem.RemoteDirectory = FilePathOnControler;
                        controller.FileSystem.LocalDirectory = tempdir;

                        try
                        {

                            //get controller mastership
                            using (Mastership master = Mastership.Request(controller))
                            {

                                //put the file  on the controller
                                controller.FileSystem.PutFile(modulename, modulename, true);


                                //check if controller if home for load.
                                Signal O_Homepos = controller.IOSystem.GetSignal("O_Homepos");
                                if (O_Homepos.Value == 1)
                                {
                                    // get modules from controller task trob1
                                    ABB.Robotics.Controllers.RapidDomain.Task tRob1 = controller.Rapid.GetTask("T_ROB1");
                                    //stop trob 1 if running.
                                    bool bWasRunning = false;
                                    if (tRob1.ExecutionStatus == TaskExecutionStatus.Running)
                                    {
                                        bWasRunning = true;
                                        tRob1.Stop(StopMode.Immediate);
                                        System.Threading.Thread.Sleep(1000);

                                    }

                                    tRob1.LoadModuleFromFile(FilePathOnControler + modulename, RapidLoadMode.Replace);
                                    row.Cells[dataGridView1.Columns["LoadOK"].Index].Value = "OK";

                                    //restart if was running
                                    if (bWasRunning)
                                    {
                                        tRob1.ResetProgramPointer();
                                        tRob1.Start();
                                    }
                                }
                                else
                                {
                                    row.Cells[dataGridView1.Columns["LoadOK"].Index].Value = "NOK";
                                }

                                //release master
                                master.Release();
                            }
                        }
                        catch (System.InvalidOperationException ex)
                        {
                            debugger.Exeption(ex);
                            debugger.Message("error in write to controller");
                            return;
                        }
                        
                    }
                    else
                    {
                        row.Cells[dataGridView1.Columns["OKtoLoad"].Index].Value = "NOK";
                    }

                    }
                row.Cells[dataGridView1.Columns["ConnectOK"].Index].Value = "OK";
            }
            catch (Exception ex)
            {
                row.Cells[dataGridView1.Columns["ConnectOK"].Index].Value = "NOK";
                debugger.Exeption(ex);
                debugger.Message("Error connecting to controller");
                return;
            }
            
        }

        //check doTipneed active
        //write configuration to robot
        private void DoTipneedcheckRobot(ControllerInfo ci, DataGridViewRow row)
        {
            string tempdir = @"c:\temp\debug\";
            string FilePathOnControler = @"/hd0a/TEMP/";

            try
            {
                if (ci.Availability != Availability.Available) { debugger.Message("controller busy: " + ci.Id); return; } //stop if controller is not available
                //
                controller = ControllerFactory.CreateFrom(ci); //get controller from factory
                    // get modules from controller task trob1
                    ABB.Robotics.Controllers.RapidDomain.Task tRob1 = controller.Rapid.GetTask("T_ROB1");
                    Module[] mx = tRob1.GetModules();
                    //find the one we need 
                    foreach (Module m in mx)
                    {
                        if (m.Name.StartsWith("LR", StringComparison.InvariantCulture))
                        {
                            Routine proc = m.GetRoutine("AutoRunUser");
                            if (proc != null) //check if we have the right module
                            {
                                //find the module on the controller and get it **************************************
                                //save it on the controller
                                m.SaveToFile(FilePathOnControler);
                                System.Threading.Thread.Sleep(1000);
                                //get file from controler to pc 
                                FileSystem cntrlFileSystem;
                                cntrlFileSystem = controller.FileSystem;
                                controller.FileSystem.RemoteDirectory = FilePathOnControler;
                                controller.FileSystem.LocalDirectory = tempdir;
                                //move file to pc
                                controller.FileSystem.GetFile(m.Name + ".mod", m.Name + ".mod", true);
                                //process the file******************************************************************
                                string lrModule = File.ReadAllText(tempdir + m.Name + ".mod");

                                if (lrModule.Contains("DoTipNeed;"))
                                {
                                    row.Cells[dataGridView1.Columns["HasTipneed"].Index].Value = "Y";
                                    string[] lines = System.IO.File.ReadAllLines(tempdir + m.Name + ".mod");

                                    foreach (string r in lines)
                                    {
                                        if (r.Contains("DoTipNeed") && r.Contains("!"))
                                        {
                                            row.Cells[dataGridView1.Columns["HasTipneedComment"].Index].Value = "Y";
                                            debugger.Log(string.Format("ERROR controller: {0} checkline= {1}",ci.ControllerName,r));
                                            return;

                                        }
                                        else
                                        {
                                            row.Cells[dataGridView1.Columns["HasTipneedComment"].Index].Value = "N";
                                        }
                                        
                                    }
                                }
                                else
                                {
                                    row.Cells[dataGridView1.Columns["HasTipneed"].Index].Value = "N";
                                }

                            }

                        }

                    }
               
                row.Cells[dataGridView1.Columns["ConnectOK"].Index].Value = "OK";
            }
            catch (Exception ex)
            {
                row.Cells[dataGridView1.Columns["ConnectOK"].Index].Value = "NOK";
                debugger.Exeption(ex);
                debugger.Message("Error connecting to controller");
                return;
            }
        }

        //try delete robotbackupprogramma folder 
        private void DoRobbieFupCheck(ControllerInfo ci, DataGridViewRow row)
        {
            string FilePathOnControler = @"/hd0a/";
            try
            {
                if (ci.Availability != Availability.Available) { debugger.Message("controller busy: " + ci.Id); return; } //stop if controller is not available
                //
                controller = ControllerFactory.CreateFrom(ci); //get controller from factory

                            FileSystem cntrlFileSystem;
                            cntrlFileSystem = controller.FileSystem;
                            controller.FileSystem.RemoteDirectory = FilePathOnControler;

              if (controller.FileSystem.DirectoryExists("ROBOTBACKUPPROGRAMMA"))
                {
                    row.Cells[dataGridView1.Columns["Found"].Index].Value = "YES";
                    try
                    {
                        controller.FileSystem.RemoveDirectory("ROBOTBACKUPPROGRAMMA", true);
                        if (controller.FileSystem.DirectoryExists("ROBOTBACKUPPROGRAMMA"))
                        {
                            row.Cells[dataGridView1.Columns["Deleted"].Index].Value = "NOK";
                        }
                        else
                        {
                            row.Cells[dataGridView1.Columns["Deleted"].Index].Value = "OK";
                        }
                        row.Cells[dataGridView1.Columns["Exeption"].Index].Value = "NO";
                    }
                    catch (Exception ex)
                    {
                        //fail 
                        row.Cells[dataGridView1.Columns["Exeption"].Index].Value = "YES";

                        COUNT++;
                        debugger.Log(ex.Message);
                        debugger.Log(string.Format("Found one: {0}", COUNT));
                    }
                }
                 else
                {
                    //not there 
                    row.Cells[dataGridView1.Columns["Found"].Index].Value = "NOdir";
                }

                row.Cells[dataGridView1.Columns["ConnectOK"].Index].Value = "OK";
            }
            catch (Exception ex)
            {
                row.Cells[dataGridView1.Columns["ConnectOK"].Index].Value = "NOK";
                debugger.Exeption(ex);
                debugger.Message("Error connecting to controller");
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

                        // SocketConfigureRobot(ci, row);
                        //DoRobbieFupCheck(ci, row);
                        //DoTipneedcheckRobot(ci, row);
                        LoadNewLrobotRobot(ci, row);
                        }
                        else
                        {
                            debugger.Message("can not find controller: " + row.Cells[0].Value.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        debugger.Exeption(ex);
                    }
                }
            
            debugger.Message("done with controllers");
        }

        private void btn_bckShortcuts_Click(object sender, EventArgs e)
        {
            RobotBckShortcuts bckShort = new RobotBckShortcuts();
            bckShort.searchForRobots();
            bckShort.buildShortcutdirectory();
            debugger.Message("done with dirbuild");
        }
    }


}
