using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Areas.VASC.Models
{
    public class c_variableMetadata
    {
    }

    //c_variable metadata
    [MetadataType(typeof(c_variable))]
    public partial class c_variable
    {
        //this links our enum to the entitymodel
        public Poll_rate _Poll_Rate
        {
            get
            {
                return (Poll_rate)Enum.ToObject(typeof(Poll_rate), this.poll_rate.GetValueOrDefault());
            }
            set
            {
                this.poll_rate = (int)value;
            }
        }

        public Enable_bit _Enable_bit
        {
            get
            {
                return (Enable_bit)Enum.ToObject(typeof(Enable_bit), this.enable_bit.GetValueOrDefault());
            }
            set
            {
                this.enable_bit = (int)value;
            }
        }

        public Event_code _Event_code
        {
            get
            {
                return (Event_code)Enum.ToObject(typeof(Event_code), this.event_enum.GetValueOrDefault());
            }
            set
            {
                this.event_enum = (int)value;
            }
        }

        public int[] _SQL_Action
        {
            get
            {
                return VASCenums.IntMaskToIntArray(this.sql_action, Enum.GetNames(typeof(SQL_Action)).Length);
            }
            set
            {
                this.sql_action = value.Sum();
            }
        }

    }
}