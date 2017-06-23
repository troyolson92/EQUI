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


namespace ABBCommTest
{
    public partial class Form1 : Form
    {

        private NetworkScanner scanner = new NetworkScanner();
        private Controller controller = null;
        private ABB.Robotics.Controllers.RapidDomain.Task[] tasks = null;
        private NetworkWatcher networkwatcher = null;
        myDebugger debugger = new myDebugger();

        public Form1()
        {
            debugger.Init(@"c:\temp\ABBcomm.log");
            //
            InitializeComponent();
            //
            listView1.Columns.Add("IP", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Id", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Availability", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("IsVirtual", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("SystemName", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Version", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("ControllerName", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Connected", -2, HorizontalAlignment.Left);
            listView1.FullRowSelect = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //monitor the network
            this.networkwatcher = new NetworkWatcher(scanner.Controllers);
            this.networkwatcher.Found += new EventHandler<NetworkWatcherEventArgs>(HandleFoundEvent);
            this.networkwatcher.Lost += new EventHandler<NetworkWatcherEventArgs>(HandleLostEvent);
            this.networkwatcher.EnableRaisingEvents = true;
        }

        private void addControllertoList(ControllerInfo controllerInfo)
        {
            ListViewItem item = new ListViewItem(controllerInfo.IPAddress.ToString());
            item.SubItems.Add(controllerInfo.Id);
            item.SubItems.Add(controllerInfo.Availability.ToString());
            item.SubItems.Add(controllerInfo.IsVirtual.ToString());
            item.SubItems.Add(controllerInfo.SystemName);
            item.SubItems.Add(controllerInfo.Version.ToString());
            item.SubItems.Add(controllerInfo.ControllerName);
            item.SubItems.Add("True"); //connected
            item.Tag = controllerInfo;
            if (!listView1.Items.Contains(item)) { listView1.Items.Add(item); }
            //keep track of controller count
            label1.Text = string.Format("Number of controllers: {0}", listView1.Items.Count);
        }

        //adding a found controller to the listvieuw
        private void AddControllerToListView(object sender, NetworkWatcherEventArgs e)
        {
            ControllerInfo controllerInfo = e.Controller;
            addControllertoList(controllerInfo);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void SignalControlerLostToListView(object sender, NetworkWatcherEventArgs e)
        {
            ControllerInfo controllerInfo = e.Controller;
            //set listview item for controler Gray 
        }

        //handle event from networkscanner (controler found)
        void HandleFoundEvent(object sender, NetworkWatcherEventArgs e)
        {
            this.Invoke(new EventHandler<NetworkWatcherEventArgs>(AddControllerToListView), new Object[] { this, e });
        }


        //handle event from networkscanner (controler lost)
        void HandleLostEvent(object sender, NetworkWatcherEventArgs e)
        {
            this.Invoke(new EventHandler<NetworkWatcherEventArgs>(SignalControlerLostToListView), new Object[] { this, e });
        }






        // add a controler by ip 
        private void btn_addCtrl_Click(object sender, EventArgs e)
        {
            //adding for specifc ip 
            try
            {
                System.Net.IPAddress ipAddress;
                try
                {
                    ipAddress = System.Net.IPAddress.Parse(tbox_ip.Text);
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


        private void NFSConfigureRobot(string Robot)
        {
            string tempdir =  @"c:\temp\";
            string NFSFilePathOnControler = @"/hd0a/Param/";
            string NFSFilename = "NFS.CFG";

            try
            {
                //check file does not exist on local machine
                if (File.Exists(tempdir+NFSFilename)) { File.Delete(tempdir+NFSFilename); }
                //build config file on local machine 
                File.WriteAllText(tempdir + NFSFilename, makeNFSConfig("99090R01", "13226", "219", @"/ROBOTBCK/robot_ga/ROBLAB/", @"/ROBOTBCK/robot_ga/IRC5_SHARE/"));
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
                //!*************************************************************************
                ListViewItem item = this.listView1.SelectedItems[0]; // selected controller
                ControllerInfo controllerInfo = (ControllerInfo)item.Tag; //get controller info
                //!*************************************************************************
                if (controllerInfo.Availability != Availability.Available) { return; } //stop if controller is not available
                if (controller.OperatingMode == ControllerOperatingMode.Auto) { return; } //controller must be on auto to take master 
                //
                controller = ControllerFactory.CreateFrom(controllerInfo); //get controller from factory
                controller.Logon(UserInfo.DefaultUser); //logon to controller

                cfg = controller.Configuration; //get controller configruation database
            }
            catch(Exception ex)
            {
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
                    //RESTART !!!!!!!!!!!!!!!!!
                   // controller.Restart(ControllerStartMode.Warm);
                    //release master
                    m.Release();
                }
            }
            catch (System.InvalidOperationException ex)
            {
                debugger.Exeption(ex);
                debugger.Message("error writeing to controller");
                return;
            }
        }



        private void btn_scanNetwork_Click(object sender, EventArgs e)
        {
                    scanner.Scan();
            ControllerInfoCollection controllers = scanner.Controllers;
            //populate the listview
            foreach (ControllerInfo controllerInfo in controllers)
            {
                addControllertoList(controllerInfo);
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            debugger.Message("done");
        }

        private void btn_writeNFS_Click(object sender, EventArgs e)
        {
            NFSConfigureRobot("");
        }



        //*--------------test section
        private void btn_getConf_Click(object sender, EventArgs e)
        {

            ListViewItem item = this.listView1.SelectedItems[0];

            if (item.Tag != null)
            {
                ControllerInfo controllerInfo = (ControllerInfo)item.Tag;
                if
                (controllerInfo.Availability == Availability.Available)
                {
                    if (this.controller != null)
                    {
                        this.controller.Logoff();
                        this.controller.Dispose();
                        this.controller = null;
                    }
                    this.controller = ControllerFactory.CreateFrom(controllerInfo);
                    this.controller.Logon(UserInfo.DefaultUser);

                    ConfigurationDatabase cfg = controller.Configuration;
                    Domain sioDomain = controller.Configuration.SerialIO;

                    //test read parm
                    // string[] path = { "SIO", "COM_APP","robot_ga","ServerAddress" };
                    // string data = cfg.Read(path);
                    //OK 



                    try
                    {

                        using (Mastership m = Mastership.Request(controller.Configuration))
                        {
                            //test write parm. (existing)
                            string[] pathWRite = { "SIO", "COM_APP", "robot_ga", "LocalPath" };
                            cfg.Write("Test1", pathWRite);
                        }
                    }
                    catch (System.InvalidOperationException ex)
                    {
                        MessageBox.Show("Mastership is held by another client." + ex.Message);
                        return;
                    }

                    // OK


                }
                else
                {
                    MessageBox.Show("Selected controller not available.");
                }

            }
        }

    }


}
