using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EqUiWebUi.Areas.VWSC.Models;
using static EqUiWebUi.Areas.VWSC.Models.VWSCenums;

namespace EqUiWebUi.Areas.VWSC.Models
{
    public class VWSC_c_bosch_viewMetadata
    {
    }

    [MetadataType(typeof(VWSC_c_bosch_view))]
    public partial class VWSC_c_bosch_view
    {

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

        public Poll_rate _Poll_rate
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

        public c_error_flags _Flag
        {
            get
            {
                return (c_error_flags)Enum.ToObject(typeof(c_error_flags), this.flag);
            }
            set
            {
                this.flag = (int)value;
            }
        }


    }
}