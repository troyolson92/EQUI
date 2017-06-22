using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;
using ABB.Robotics.Controllers.EventLogDomain;
using ABB.Robotics.Controllers.ConfigurationDomain;


namespace ABBCommTest
{
    public partial class Form1 : Form
    {

        private NetworkScanner scanner = null;
        private Controller controller = null;
        private ABB.Robotics.Controllers.RapidDomain.Task[] tasks = null;
        private NetworkWatcher networkwatcher = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //scan and populate local subnet
            this.scanner = new NetworkScanner();
            this.scanner.Scan();
            ControllerInfoCollection controllers = scanner.Controllers;
            //populate the listview
            ListViewItem item = null;
            foreach (ControllerInfo controllerInfo in controllers)
            {
                item = new ListViewItem(controllerInfo.IPAddress.ToString());
                item.SubItems.Add(controllerInfo.Id);
                item.SubItems.Add(controllerInfo.Availability.ToString());
                item.SubItems.Add(controllerInfo.IsVirtual.ToString());
                item.SubItems.Add(controllerInfo.SystemName);
                item.SubItems.Add(controllerInfo.Version.ToString());
                item.SubItems.Add(controllerInfo.ControllerName);
                this.listView1.Items.Add(item);
                item.Tag = controllerInfo;
            }

            //monitor the network
            this.networkwatcher = new NetworkWatcher(scanner.Controllers);
            this.networkwatcher.Found += new EventHandler<NetworkWatcherEventArgs>(HandleFoundEvent);
           // this.networkwatcher.Lost += new EventHandler<NetworkWatcherEventArgs>(HandleLostEvent);
            this.networkwatcher.EnableRaisingEvents = true;

            //enable monitoring for each controller 
            foreach (Controller ctrl in controllers)
            {
                ctrl.OperatingModeChanged += new EventHandler<OperatingModeChangeEventArgs>(ctrl_OperatingModeChanged);
            }


        }

        //adding a found controller to the listvieuw
        private void AddControllerToListView(object sender, NetworkWatcherEventArgs e)
        {
            ControllerInfo controllerInfo = e.Controller;
            ListViewItem item = new ListViewItem(controllerInfo.IPAddress.ToString());
            item.SubItems.Add(controllerInfo.Id);
            item.SubItems.Add(controllerInfo.Availability.ToString());
            item.SubItems.Add(controllerInfo.IsVirtual.ToString());
            item.SubItems.Add(controllerInfo.SystemName);
            item.SubItems.Add(controllerInfo.Version.ToString());
            item.SubItems.Add(controllerInfo.ControllerName);
            this.listView1.Items.Add(item); 
            item.Tag = controllerInfo;
        }

        private void SignalControlerLostToListView(object sender, NetworkWatcherEventArgs e)
        {
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
            this.Invoke(new EventHandler<NetworkWatcherEventArgs>(AddControllerToListView), new Object[] { this, e });
        }


        //event handler for op mode change
        private static void ctrl_OperatingModeChanged(object sender, OperatingModeChangeEventArgs e)
        {
            Console.WriteLine("New Operating mode at: {0} new mode is: {1}", e.Time, e.NewMode);
            MessageBox.Show(string.Format("New Operating mode at: {0} new mode is: {1}", e.Time, e.NewMode));
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
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
                    if (controller.OperatingMode == ControllerOperatingMode.Auto)
                    {
                        tasks = controller.Rapid.GetTasks();
                        using (Mastership m = Mastership.Request(controller.Rapid))
                        {

                           MessageBox.Show("Mastership");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Automatic mode is required to take master.");
                    }
                }
                else
                {
                    MessageBox.Show("Selected controller not available.");
                }
            }
        }

        private void btn_startTask_Click(object sender, EventArgs e)
        {
            Ctrl_startStop(sender, e, true);
        }

        private void btn_startStop_Click(object sender, EventArgs e)
        {
            Ctrl_startStop(sender, e, false);
        }

        private void Ctrl_startStop(object sender, EventArgs e, Boolean TaskState)
        {
            try
            {
                if (controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    tasks = controller.Rapid.GetTasks();
                    using (Mastership m = Mastership.Request(controller.Rapid))
                    {
                        //Perform operation
                        if (TaskState)
                        {
                            tasks[1].Start();
                            MessageBox.Show("Task started");
                        }
                        else
                        {
                            tasks[1].Stop();
                            MessageBox.Show("Task Stopped");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Automatic mode is required to start execution from a remote client.");
                }
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client." + ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
            }

        }

        // add a controler by ip 
        private void btn_addCtrl_Click(object sender, EventArgs e)
        {
            //adding for specifc ip 
            try
            {
                this.scanner = new NetworkScanner();
                 NetworkScanner.AddRemoteController(tbox_ip.Text.ToString()); // need to handle non ip format
                this.scanner.Scan();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

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

                    //test write parm. (existing)
                    /*
                     try
                        {

                        using (Mastership m = Mastership.Request(controller.Configuration))
                             {
                                 string[] pathWRite = { "SIO", "COM_APP", "robot_ga", "LocalPath" };
                                 cfg.Write("Test1:", pathWRite);
                             } 
                        }
                     catch (System.InvalidOperationException ex)
                        {
                              MessageBox.Show("Mastership is held by another client.");
                        }
                     */
                     // OK

                     //test write parm. (new instance )
                     try
                     {
                         DomainCollection domains = cfg.Domains;
                         Domain domain = controller.Configuration.Domains[controller.Configuration.Domains.IndexOf("SIO")];
                         ABB.Robotics.Controllers.ConfigurationDomain.Type COM_APPType = domain.Types[domain.Types.IndexOf("COM_APP")];

                         using (Mastership m = Mastership.Request(controller.Configuration))
                         {

                             //COM_apptype should have al the instances of comm app type on the controller
                             var objInstance = COM_APPType["Test_Instance"]; //looks if the instance we whant to create exist

                             //create new instance of com_app
                             if (objInstance == null) objInstance = COM_APPType.Create("Test_Instance"); //if not exist create it 

                             // set an attribute
                             objInstance.SetAttribute("LocalPath", "testpath:");
                             

                             //get it back 
                             object objEntry = objInstance.GetAttribute("LocalPath");
                  
                         }
                     }
                     catch (System.InvalidOperationException ex)
                     {
                         MessageBox.Show("Mastership is held by another client.");
                     }
                    // 

                }
                else
                {
                    MessageBox.Show("Selected controller not available.");
                }
            }
        }



    }
}
