using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Areas.VASC.Models
{
    public class c_controllerMetadata
    {
    }

    [MetadataType(typeof(c_controller))]
    public partial class c_controller
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

        public ControllerFlags _ControllerFlags
        {
            get
            {
                return (ControllerFlags)Enum.ToObject(typeof(ControllerFlags), this.flags);
            }
            set
            {
                this.flags = (int)value;
            }
        }
    }
}