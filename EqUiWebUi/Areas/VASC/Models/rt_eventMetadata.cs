using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Areas.VASC.Models
{
    public class rt_eventMetadata
    {
    }

    //c_csv_log metadata
    [MetadataType(typeof(rt_event))]
    public partial class rt_event
    {
        //this links our enum to the entitymodel
        public Event_code _Event_code
        {
            get
            {
                return (Event_code)Enum.ToObject(typeof(Event_code), this.event_enum);
            }
            set
            {
                this.event_enum = (int)value;
            }
        }

    }
}