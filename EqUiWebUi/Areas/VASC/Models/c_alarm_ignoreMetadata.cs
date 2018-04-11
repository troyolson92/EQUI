using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Areas.VASC.Models
{
    public class c_alarm_ignoreMetadata
    {
    }

    //c_alarm_ignore metadata
    [MetadataType(typeof(c_alarm_ignore))]
    public partial class c_alarm_ignore
    {
        //this links our enum to the entitymodel
        public LogicOperator _Operator
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

        public ErrorCategory _ErrorCategory
        {
            get
            {
                return (ErrorCategory)Enum.ToObject(typeof(ErrorCategory), this.error_category);
            }
            set
            {
                this.error_category = (int)value;
            }
        }
    }
}