using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static EqUiWebUi.Areas.VWSC.Models.VWSCenums;

namespace EqUiWebUi.Areas.VWSC.Models
{
    public class VWSC_c_errorMetadata
    {
    }

    [MetadataType(typeof(VWSC_c_error))]
    public partial class VWSC_c_error
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

        public LogicOperator _C_operator
        {
            get
            {
                return (LogicOperator)Enum.ToObject(typeof(LogicOperator), this.C_operator);
            }
            set
            {
                this.C_operator = (int)value;
            }
        }

        public c_error_flags _Flags
        {
            get
            {
                return (c_error_flags)Enum.ToObject(typeof(c_error_flags), this.flags);
            }
            set
            {
                this.flags = (int)value;
            }
        }


    }
}