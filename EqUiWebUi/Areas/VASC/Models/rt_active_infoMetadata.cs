using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Areas.VASC.Models
{
    public class rt_active_infoMetadata
    {
    }

    [MetadataType(typeof(rt_active_info))]
    public partial class rt_active_info
    {
        public VASCState _VASCState
        {
            get
            {
                return (VASCState)Enum.ToObject(typeof(VASCState), this.vasc_state.GetValueOrDefault());
            }
            set
            {
                this.vasc_state = (int)value;
            }
        }
    }

}