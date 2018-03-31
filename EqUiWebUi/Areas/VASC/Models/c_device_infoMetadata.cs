using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Areas.VASC.Models
{
    public class c_device_infoMetadata
    {
    }

    //c_device_info metadata
    [MetadataType(typeof(c_device_info))]
    public partial class c_device_info
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

        public Insert_update _Insert_update
        {
            get
            {
                return (Insert_update)Enum.ToObject(typeof(Insert_update), this.insert_update);
            }
            set
            {
                this.insert_update = (int)value;
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

    }
}