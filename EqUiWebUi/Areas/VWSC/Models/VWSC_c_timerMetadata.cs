using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EqUiWebUi.Areas.VWSC.Models;
using static EqUiWebUi.Areas.VWSC.Models.VWSCenums;

namespace EqUiWebUi.Areas.VWSC.Models
{
    public class VWSC_c_timerMetadata
    {
    }

    [MetadataType(typeof(VWSC_c_Timer))]
    public partial class VWSC_c_Timer
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


    }
}