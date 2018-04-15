using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Areas.VASC.Models
{
    public class l_operationMetadata
    {
    }

    [MetadataType(typeof(L_operation))]
    public partial class L_operation
    {
        public L_operationCode _L_operationCode
        {
            get
            {
                return (L_operationCode)Enum.ToObject(typeof(L_operationCode), this.code.GetValueOrDefault());
            }
            set
            {
                this.code = (int)value;
            }
        }
    }
}