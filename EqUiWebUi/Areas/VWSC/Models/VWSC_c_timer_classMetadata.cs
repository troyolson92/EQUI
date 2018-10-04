using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EqUiWebUi.Areas.VWSC.Models
{
    public class VWSC_c_timer_classMetadata
    {
    }
    //VWSC_c_timer_class metadata
    [MetadataType(typeof(VWSC_c_timer_class))]
    public partial class VWSC_c_timer_class
    {
        public int[] _cBoschViewMaskMask
        {
            get
            {
                return VWSCenums.IntMaskToIntArray(this.cBoschViewMask, Enum.GetNames(typeof(VWSCenums.Enable_bit_MASK)).Length);
            }
            set
            {
                this.cBoschViewMask = value.Sum();
            }
        }

        public int[] _cErrorMask
        {
            get
            {
                return VWSCenums.IntMaskToIntArray(this.cErrorMask, Enum.GetNames(typeof(VWSCenums.Enable_bit_MASK)).Length);
            }
            set
            {
                this.cErrorMask = value.Sum();
            }
        }

        public int[] _cServeityMask
        {
            get
            {
                return VWSCenums.IntMaskToIntArray(this.cSeverityMask, Enum.GetNames(typeof(VWSCenums.Enable_bit_MASK)).Length);
            }
            set
            {
                this.cSeverityMask = value.Sum();
            }
        }
       
    }
}