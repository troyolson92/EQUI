using System;
using System.ComponentModel.DataAnnotations;

namespace EqUiWebUi.Areas.Alert.Models
{
 //Trigger metadata
    [MetadataType(typeof(c_triggersMetaData))]
    public partial class c_triggers
    {
        //this links our enum to the entitymodel
        public SmsDatabases RunAgainstDatabase
        {
            get
            {
                return (SmsDatabases)Enum.ToObject(typeof(SmsDatabases), this.RunAgainst);
            }
            set
            {
                this.RunAgainst = (int)value;
            }
        }

    }

    public class c_triggersMetaData
    {
        //************************************************************************************************
        //************************************************************************************************
        //************************************************************************************************
        //************************************************************************************************

        //HELPTEXT is not being inherited from here! Need to invesitage :( 

        //************************************************************************************************
        //************************************************************************************************
        //************************************************************************************************
        //************************************************************************************************


        public int id { get; set; }
        [HelpText("turn the trigger on or off")]
        public bool enabled { get; set; }
        [Display(Name = "Omschrijving")]
        [HelpText("Describe the porpuse of this alert trigger")]
        public string discription { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 3)]
        [HelpText("Gets used in sms and shown as alert subgroup")]
        public string alertType { get; set; }
        [HelpText(
@"This statement is run against gadata
  and must return col<LocationTree, _timestamp, info>")]
        public string sqlStqStatement { get; set; }
        [HelpText("CPT600 sms system to be used. MUST be defined in CPT600!!!")]
        public Nullable<int> smsSystem { get; set; }
        [HelpText("Inital state of alert created by this trigger")]
        public int initial_state { get; set; }
        [HelpText("Polrate in minutes to evaluate this trigger")]
        public int Pollrate { get; set; }
        [HelpText("When alert trigger is gone set alert state to techComp")]
        public bool AutoSetStateTechComp { get; set; }
        [HelpText("Send sms on each retrigger of the alert")]
        public bool smsOnRetrigger { get; set; }
        [HelpText("Database to run statement")]
        public int RunAgainst { get; set; }
        [HelpText("General on of for SMS system")]
        public bool enableSMS { get; set; }

    }

    //enum for RunAgainst database type
    public enum SmsDatabases
    {
        GADATA = 1,
        STO = 2
    }

    //enum for alert states
    public enum alertState
    {
        WGK = 1,
        OKREQ = 2,
        COMP = 3,
        VOID = 4,
        TECHCOMP = 5
    }

    //Alert metadata
    [MetadataType(typeof(h_alertMetaData))]
    public partial class h_alert
    {
    }

    public class h_alertMetaData
    {
        public int id { get; set; }
        public int c_tirgger_id { get; set; }
        [Display(Name = "Alert trigger timestamp")]
        public System.DateTime C_timestamp { get; set; }
        [HelpText("info about this alert (from SQL statement)")]
        public string info { get; set; }
        [HelpText("Maximo like status for this alert")]
        public int state { get; set; }
        public string location { get; set; }
        public string comments { get; set; }
        [HelpText("User that puts alert in state 'OKREQ'")]
        public Nullable<int> acceptUserID { get; set; }
        public Nullable<System.DateTime> acceptTimestamp { get; set; }
        [HelpText("User that puts alert in state 'COMP or VOID'")]
        public Nullable<int> closeUserID { get; set; }
        public Nullable<System.DateTime> closeTimestamp { get; set; }
        [HelpText("User that last changed the alert (save operation)")]
        public Nullable<int> lastChangedUserID { get; set; }
        public Nullable<System.DateTime> lastChangedTimestamp { get; set; }
        [HelpText("Number of times this alert was retriggerd")]
        public int triggerCount { get; set; }
        [HelpText("Last time this alert was triggerd")]
        public System.DateTime lastTriggerd { get; set; }

    }


}

