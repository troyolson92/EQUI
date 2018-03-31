using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Areas.VASC.Models
{
    public class c_csv_logMetadata
    {
    }

    //c_csv_log metadata
    [MetadataType(typeof(c_csv_log))]
    public partial class c_csv_log
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

        public Csv_log_Flags _Csv_log_Flags
        {
            get
            {
                return (Csv_log_Flags)Enum.ToObject(typeof(Csv_log_Flags), this.flags);
            }
            set
            {
                this.flags = (int)value;
            }
        }

    }
}