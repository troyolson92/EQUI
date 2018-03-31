using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.VASC.Models
{
    public class c_controller_classMetadata
    {
    }

    //c_controller_class metadata
    [MetadataType(typeof(c_controller_class))]
    public partial class c_controller_class
    {
        //this links our enum to the entitymodel
        public List<SelectListItem> _evStateChange
        {
            get
            {
                List<SelectListItem> items = new List<SelectListItem>();
                SelectListItem listItem1 = new SelectListItem
                {
                    Text = "item1",
                    Value = "val1",
                    Selected = true
                };
                items.Add(listItem1);

                SelectListItem listItem2 = new SelectListItem
                {
                    Text = "item2",
                    Value = "val2",
                    Selected = false
                };
                items.Add(listItem2);


                return items;
                //return (SQL_Action)Enum.ToObject(typeof(SQL_Action), this.evStateChange);
            }
            set
            {
                //this.evStateChange = (int)value;
            }
        }
        /*
        public SQL_Action _evOperatingModeChange
        {
            get
            {
                return (SQL_Action)Enum.ToObject(typeof(SQL_Action), this.evOperatingModeChange);
            }
            set
            {
                this.evStateChange = (int)value;
            }
        }
        public SQL_Action _evConnectionChange
        {
            get
            {
                return (SQL_Action)Enum.ToObject(typeof(SQL_Action), this.evConnectionChange);
            }
            set
            {
                this.evStateChange = (int)value;
            }
        }
        public SQL_Action _evExecutionStatus
        {
            get
            {
                return (SQL_Action)Enum.ToObject(typeof(SQL_Action), this.evExecutionStatus);
            }
            set
            {
                this.evStateChange = (int)value;
            }
        }
        public SQL_Action _evExecutionStatusTRob1
        {
            get
            {
                return (SQL_Action)Enum.ToObject(typeof(SQL_Action), this.evExecutionStatusTRob1);
            }
            set
            {
                this.evStateChange = (int)value;
            }
        }
        public SQL_Action _evBackupCompleted
        {
            get
            {
                return (SQL_Action)Enum.ToObject(typeof(SQL_Action), this.evBackupCompleted);
            }
            set
            {
                this.evStateChange = (int)value;
            }
        }
        public SQL_Action _evDataResolveChange
        {
            get
            {
                return (SQL_Action)Enum.ToObject(typeof(SQL_Action), this.evDataResolveChange);
            }
            set
            {
                this.evStateChange = (int)value;
            }
        }
        public SQL_Action _evExecutionCycleChange
        {
            get
            {
                return (SQL_Action)Enum.ToObject(typeof(SQL_Action), this.evExecutionCycleChange);
            }
            set
            {
                this.evStateChange = (int)value;
            }
        }
        public SQL_Action _evTaskEnabledChange
        {
            get
            {
                return (SQL_Action)Enum.ToObject(typeof(SQL_Action), this.evTaskEnabledChange);
            }
            set
            {
                this.evStateChange = (int)value;
            }
        }
        public SQL_Action _evMotionPointerTRob1Change
        {
            get
            {
                return (SQL_Action)Enum.ToObject(typeof(SQL_Action), this.evMotionPointerTRob1Change);
            }
            set
            {
                this.evStateChange = (int)value;
            }
        }
        public SQL_Action _evProgramPointerTrob1Change
        {
            get
            {
                return (SQL_Action)Enum.ToObject(typeof(SQL_Action), this._evProgramPointerTrob1Change);
            }
            set
            {
                this.evStateChange = (int)value;
            }
        }
        public SQL_Action _evMotionPointerTrob1ManualChange
        {
            get
            {
                return (SQL_Action)Enum.ToObject(typeof(SQL_Action), this.evMotionPointerTRob1ManualChange);
            }
            set
            {
                this.evStateChange = (int)value;
            }
        }
        public SQL_Action _evProgramPointerTrob1ManualChange
        {
            get
            {
                return (SQL_Action)Enum.ToObject(typeof(SQL_Action), this.evProgramPointerTRob1ManualChange);
            }
            set
            {
                this.evStateChange = (int)value;
            }
        }

    */

    }
}