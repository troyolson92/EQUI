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
                return (Poll_rate)Enum.ToObject(typeof(Poll_rate), this.poll_rate);
            }
            set
            {
                this.poll_rate = (int)value;
            }
        }

        public SQL_Action _SQL_Action
        {
            get
            {
                return (SQL_Action)Enum.ToObject(typeof(SQL_Action), this.sql_action);
            }
            set
            {
                this.sql_action = (int)value;
            }
        }

        public Enable_bit _Enable_bit
        {
            get
            {
                return (Enable_bit)Enum.ToObject(typeof(Enable_bit), this.enable_bit);
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
                return (Event_code)Enum.ToObject(typeof(Event_code), this.event_enum);
            }
            set
            {
                this.event_enum = (int)value;
            }
        }

    }
}