using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static EqUiWebUi.Areas.VWSC.Models.VWSCenums;

namespace EqUiWebUi.Areas.VWSC.Models
{
    public class VWSC_rt_active_infoMetadata
    {
    }

    [MetadataType(typeof(VWSC_rt_active_info))]
    public partial class VWSC_rt_active_info
    {
        public VWSCState _VWSCState
        {
            get
            {
                return (VWSCState)Enum.ToObject(typeof(VWSCState), this.vwsc_state.GetValueOrDefault());
            }
            set
            {
                this.vwsc_state = (int)value;
            }
        }
    }

}