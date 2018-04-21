using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Areas.VASC.Models
{
    public class rt_job_breakdownMetadata
    {
    }

    //c_service_setup metadata
    [MetadataType(typeof(rt_job_breakdown))]
    public partial class rt_job_breakdown
    {
        //this links our enum to the entitymodel
        public Event_code _ev_breakdownAck
        {
            get
            {
                return (Event_code)Enum.ToObject(typeof(Event_code), this.ev_breakdownAck.GetValueOrDefault());
            }
            set
            {
                this.ev_breakdownAck = (int)value;
            }
        }

        public Event_code _ev_breakdownStart
        {
            get
            {
                return (Event_code)Enum.ToObject(typeof(Event_code), this.ev_breakdownStart.GetValueOrDefault());
            }
            set
            {
                this.ev_breakdownStart = (int)value;
            }
        }


    }
}