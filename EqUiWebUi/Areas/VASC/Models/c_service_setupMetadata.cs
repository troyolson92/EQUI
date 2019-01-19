using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Areas.VASC.Models
{
    public class c_service_setupMetadata
    {
    }

    //c_service_setup metadata
    [MetadataType(typeof(c_service_setup))]
    public partial class c_service_setup
    {
        //this links our enum to the entitymodel
        public Enable_bit _Enable_bit
        {
            get
            {
                return (Enable_bit)Enum.ToObject(typeof(Enable_bit), this.bit_id.GetValueOrDefault());
            }
            set
            {
                this.bit_id = (int)value;
            }
        }

        public int[] _Enable_bit_MASK
        {
            get
            {
                return VASCenums.IntMaskToIntArray(this.bit_id, Enum.GetNames(typeof(Enable_bit_MASK)).Length);
            }
            set
            {
                this.bit_id = VASCenums.IntArrayToIntMask(value);
            }
        }

    }
}