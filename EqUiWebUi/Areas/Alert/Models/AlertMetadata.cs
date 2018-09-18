using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using TrackerEnabledDbContext;

namespace EqUiWebUi.Areas.Alert.Models
{
    //tracker (not testet)
    //https://github.com/bilal-fazlani/tracker-enabled-dbcontext/wiki/1.-Types-of-TEDB
    public class ApplicationDbContext : TrackerContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }

    //trigger metadata
    [TrackChanges]
    [MetadataType(typeof(c_triggersMetaData))]
    public partial class c_triggers
    {
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
        [HelpText("Turn the trigger on or off")]
        public bool enabled { get; set; }
        [Display(Name = "Omschrijving")]
        [HelpText("Describe the porpuse of this alert trigger")]
        public string discription { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 3)]
        [HelpText("Gets used in sms and shown as alert subgroup")]
        public string alertType { get; set; }
        [HelpText("This statement is run against dbEQUI")]
        public string sqlStatement { get; set; }
        [HelpText("CPT600 sms system to be used. MUST be defined in CPT600!!!")]
        public Nullable<int> smsSystem { get; set; }
        [HelpText("Inital state of alert created by this trigger")]
        public int initial_state { get; set; }
        [HelpText("When alert trigger is gone set alert state to techComp")]
        public bool AutoSetStateTechComp { get; set; }
        [HelpText("Send sms on each retrigger of the alert")]
        public bool smsOnRetrigger { get; set; }
        [HelpText("Database to run statement")]
        public int RunAgainst { get; set; }
        [HelpText("General on of for SMS system")]
        public bool enableSMS { get; set; }
        [HelpText("if on this alert shows downtime")]
        public bool isDowntime { get; set; }
        [HelpText("if on is show in reports")]
        public bool isInReport { get; set; }

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

//************************************
//Helpers and enums.......
//************************************
    //enum for alert states
    public enum alertState
    {
        WGK = 1,
        OKREQ = 2,
        COMP = 3,
        VOID = 4,
        TECHCOMP = 5
    }

    //helper class to pass chart settings
    public class ChartSettings
    {
        public string chartname { get; set; }
        public string scaleLabel { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public string alarmobject { get; set; }
        public int c_trigger_id { get; set; }
        public int optDatanum { get; set; } //from 0-5 0=no opt data 1-5 op data set 
        public string optDataLabels { get; set; }
    }

    public class AlertResult
    {
        public DateTime timestamp { get; set; }
        public string info { get; set;}
        public string LocationTree { get; set; }
        public string ClassTree { get; set; }
        public string Location { get; set; }
        public string alarmobject { get; set; }
        public bool handeld { get; set; }
    }

}

