using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using EQUICommunictionLib;

namespace ExcelAddInEquipmentDatabase
{
    public class ClickOnceUtil
    {
        myDebugger Debugger = new myDebugger();

        Version _UpdateVersion = null;
        public string UpdateLocation
        {
            get
            {
                return System.Deployment.Application.ApplicationDeployment.CurrentDeployment.UpdateLocation.AbsoluteUri;
            }
        }
        public string DeployLocation
        {
            get
            {
                return System.Deployment.Application.ApplicationDeployment.CurrentDeployment.DataDirectory; 
            }
        }
        public Version AvailableVersion
        {
            get
            {
                if (_UpdateVersion == null)
                {
                    _UpdateVersion = new Version("0.0.0.0");
                    if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                    {
                        using (XmlReader reader = XmlReader.Create(System.Deployment.Application.ApplicationDeployment.CurrentDeployment.UpdateLocation.AbsoluteUri))
                        {
                            //Keep reading until there are no more FieldRef elements
                            while (reader.ReadToFollowing("assemblyIdentity"))
                            {
                                //Extract the value of the Name attribute
                                string versie = reader.GetAttribute("version");
                                _UpdateVersion = new Version(versie);
                            }
                        }
                    }
                }
                return _UpdateVersion;
            }
        }
        public bool UpdateAvailable
        {
            get
            {
                return System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion < AvailableVersion;
            }
        }
        public string CurrentVersion
        {
            get
            {
                return System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
        }

        public void Update()
        {
            System.Diagnostics.Process.Start(System.Deployment.Application.ApplicationDeployment.CurrentDeployment.UpdateLocation.AbsoluteUri);
            Environment.Exit(0);
        }

        public void CheckAndUpdate()
        {
            try
            {
                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                {
                    if (UpdateAvailable)
                        Update();
                }
                else
                {

                    Debugger.Message("not network deployed");
                }
            }
            catch (Exception)
            {
            }
        }

        public void test()
        {
         
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                Debugger.Message("u:" + UpdateLocation); //locatie op
                Debugger.Message("d:" + DeployLocation); //waar het op  de pc staat
                Debugger.Message("cv:" + CurrentVersion); //versie op pc
                Debugger.Message("cAvail:" + AvailableVersion.ToString()); //beschikbare verise
            }
            else
            {
                Debugger.Message("not network deployed");
            }
            
        }

        public void CheckUpdateLocation()
        {
            myDebugger Debugger = new myDebugger();
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                if (UpdateLocation.Like("%OBJECTBEHEER%"))
                {
                    Debugger.Message(@"
Sorry voor dit ongemak. 
Omdat deze plugin binnenkort ook in GB zal gebruikt worden moet ik de 'Launch' locatie veranderen.
Om dit mogelijk te maken moeten eenmaalig de reeds geinstaleerde versies worden verwijderd en opnieuw geinstaleerd.
Gelieve deze instructie op sharepoint te volgen.
https://sharepoint.volvocars.net/sites/vcg_ga_aaosr/SitePages/EQUI_changeDeployment.aspx
Druk op Ok om naar de sharepoint te gaan.
mvg 
Sam
");
                    System.Diagnostics.Process.Start("https://sharepoint.volvocars.net/sites/vcg_ga_aaosr/SitePages/EQUI_changeDeployment.aspx");
                }
            }
            else
            {
               // Debugger.Message("not network deployed");
            }

        }

    }
}