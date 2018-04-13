using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EqUiWebUi.Areas.VASC.Models;

namespace EqUiWebUi.Areas.VASC.Models
{
    public partial class c_errorMetadata
    {
    }

    //c_alarm_ignore metadata
    [MetadataType(typeof(c_error))]
    public partial class c_error
    {
        //this links our enum to the entitymodel
        public LogicOperator _Operator
        {
            get
            {
                return (LogicOperator)Enum.ToObject(typeof(LogicOperator), this.C_operator.GetValueOrDefault());
            }
            set
            {
                this.C_operator = (int)value;
            }
        }

        public Enable_bit _Enable_bit
        {
            get
            {
                return (Enable_bit)Enum.ToObject(typeof(Enable_bit), this.enable_bit.GetValueOrDefault());
            }
            set
            {
                this.enable_bit = (int)value;
            }
        }

        public ErrorCategory _ErrorCategory
        {
            get
            {
                return (ErrorCategory)Enum.ToObject(typeof(ErrorCategory), this.error_category.GetValueOrDefault());
            }
            set
            {
                this.error_category = (int)value;
            }
        }

        public int[] _ErrorFlags
        {
            get
            {
                return VASCenums.IntMaskToIntArray(this.flags, Enum.GetNames(typeof(ErrorFlags)).Length);
            }
            set
            {
                this.flags = value.Sum();
            }
        }


    }
}