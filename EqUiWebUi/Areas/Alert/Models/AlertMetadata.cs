using System.ComponentModel.DataAnnotations;

namespace EqUiWebUi.Areas.Alert.Models
{
 //Trigger metadata
    [MetadataType(typeof(c_triggersMetaData))]
    public partial class c_triggers
    {
    }

    public class c_triggersMetaData
    {

        //HELPTEXT is not being inherited from here! Need to invesitage :( 
        [HelpText("Turns the trigger on or off")]
        public bool enabled { get; set; }

        [Required]
        [Display(Name = "Omschrijving")]
        [HelpText("Describe the porpuse of the alert trigger")]
        public string discription { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 3)]
        [HelpText("Gets used in sms and shown as alert subgroup")]
        public string alertType { get; set; }

        [HelpText("ReAD MANUAL!")]
        public string sqlStqStatement { get; set; }

    }


 //Alert metadata
    [MetadataType(typeof(h_alertMetaData))]
    public partial class h_alert
    {
    }

    public class h_alertMetaData
    {
        [Display(Name = "Alert trigger timestamp")]
        public System.DateTime C_timestamp { get; set; }

    }


}

