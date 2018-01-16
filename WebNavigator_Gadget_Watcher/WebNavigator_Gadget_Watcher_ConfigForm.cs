using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace WebNavigator_Gadget_Watcher
{
    public partial class WebNavigator_Gadget_Watcher_ConfigForm : Form
    {
        //instance of log4Net logger
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //instance of dataobject (to store config)
        DataTable configData = new DataTable();

        public WebNavigator_Gadget_Watcher_ConfigForm()
        {
            InitializeComponent();
            //read settings form user file into form.
            tb_xmlConfig.Text = (string)WebNavigator_Gadget_Watcher.Properties.Settings.Default["xmlConfig"];
            cb_WatchConfig.Checked = (Boolean)WebNavigator_Gadget_Watcher.Properties.Settings.Default["WatchConfig"];
            tb_ExportedFile.Text = (string)WebNavigator_Gadget_Watcher.Properties.Settings.Default["ExportedFile"];

            tb_netExportLocation.Text = (string)WebNavigator_Gadget_Watcher.Properties.Settings.Default["netExportLocation"];
            tb_netExportUser.Text = (string)WebNavigator_Gadget_Watcher.Properties.Settings.Default["netExportUser"];
            tb_netExportPass.Text = (string)WebNavigator_Gadget_Watcher.Properties.Settings.Default["netExportPass"];

            //build colums for datagrid
            configData.Columns.Add("screen", System.Type.GetType("System.String"));
            configData.Columns.Add("cycle", System.Type.GetType("System.String"));
            configData.Columns.Add("height", System.Type.GetType("System.String"));
            configData.Columns.Add("width", System.Type.GetType("System.String"));
            configData.Columns.Add("ypos", System.Type.GetType("System.String"));
            configData.Columns.Add("xpos", System.Type.GetType("System.String"));
            configData.Columns.Add("lastPublised", System.Type.GetType("System.String"));
            //link to datagrid
            dgv_data.DataSource = configData;

            //if cb_WatchConfig.Checked make file system watcher to check for changes and read the file at startup.
            if (cb_WatchConfig.Checked)
            {
                log.Info("Application autostart");

                FileSystemWatcher ConifgWatcher = new FileSystemWatcher();
                try
                {
                    ConifgWatcher.Path = tb_netExportLocation.Text;
                    ConifgWatcher.Filter = "*.xml"; ;
                    ConifgWatcher.InternalBufferSize = (ConifgWatcher.InternalBufferSize * 2); //2 times default buffer size 
                    ConifgWatcher.Error += ConifgWatcher_Error;
                    ConifgWatcher.Changed += ConifgWatcher_Changed;
                    ConifgWatcher.EnableRaisingEvents = true;
                    log.Info("ConifgWatcher config done");
                }
                catch (Exception ex)
                {
                    {
                        log.Fatal("Failed to set up ConifgWatcher", ex);
                        //if something realy bad happens wait 10 seconds and restart. 
                        System.Threading.Thread.Sleep(10000);
                        Environment.Exit(999);
                    }
                }
                //read xml config. (auto startup)
                log.Info("Reading XML");
                btn_readConfig.Enabled = false;
                ReadXmlConfig();
                //Start monitoring
                log.Info("Start monitoring");
                btn_StartMonitoring.Enabled = false;
                MonitorForPublisedImages();
            }
            //show window
            this.Show();
        }

        //This triggers when the config file changes.
        private void ConifgWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            //we log the action and respawn ourselfs.
            log.Info("ConifgWatcher Change in config detected (Restarting.)");
            System.Threading.Thread.Sleep(5000); //just watch 5 seconds 

            //respawn ourself

            //SDB to be implemented
        }

        //in case of error with configWatchter
        private void ConifgWatcher_Error(object sender, ErrorEventArgs e)
        {
            log.Fatal("ConifgWatcher error detected (Shutting down)", e);
            //if something realy bad happens wait 10 seconds and restart. 
            System.Threading.Thread.Sleep(10000);
            Environment.Exit(999);
        }


        //reads the _gadet Xml config file.
        public void ReadXmlConfig()
        {
            //read xml file

            //link to datable

            //check that all Sizes are unique
        }

        //Manual request to read config file.
        private void btn_readConfig_Click(object sender, EventArgs e)
        {
            ReadXmlConfig();
        }

        //map the network drive where we export the file
        private void map_drive()
        {
            try
            {
                var credentials = new NetworkCredential(tb_netExportUser.Text, tb_netExportPass.Text);
                NetworkConnection networkConnection = new NetworkConnection(tb_netExportLocation.Text, credentials);
                log.Info("logged in on network using incode credentials");
            }
            catch (Exception ex)
            {
                log.Error("logon failed using incode credentials", ex);
            }

        }

        //Start monitoring output location for changes
        private void MonitorForPublisedImages()
        {
            FileSystemWatcher PublishWatcher = new FileSystemWatcher();
            try
            {
                PublishWatcher.Path = tb_netExportLocation.Text;
                PublishWatcher.Filter = "*.jpg"; ;
                PublishWatcher.InternalBufferSize = (PublishWatcher.InternalBufferSize * 2); //2 times default buffer size 
                PublishWatcher.Error += PublishWatcher_Error;
                PublishWatcher.Changed += PublishWatcher_Changed;
                PublishWatcher.EnableRaisingEvents = true;
                log.Info("PublishWatcher config done");
            }
            catch (Exception ex)
            {
                {
                    log.Fatal("Failed to set up PublishWatcher", ex);
                    //if something realy bad happens wait 10 seconds and restart. 
                    System.Threading.Thread.Sleep(10000);
                    Environment.Exit(999);
                }
            }

        }

        //chandle published file
        private void PublishWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
            //get file

            //get file size

            //match with table

            //get name from table

            //push to network location

        }

        //in case of error with publish wachter
        private void PublishWatcher_Error(object sender, ErrorEventArgs e)
        {
            log.Fatal("PublishWatcher error detected (Shutting down)", e);
            //if something realy bad happens wait 10 seconds and restart. 
            System.Threading.Thread.Sleep(10000);
            Environment.Exit(999);
        }

        //manualy start monitoring
        private void btn_StartMonitoring_Click(object sender, EventArgs e)
        {
            log.Info("PublishWatcher manual click");
            MonitorForPublisedImages();
        }
    }
}
