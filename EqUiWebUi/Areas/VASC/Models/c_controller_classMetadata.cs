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
        public int[] _evStateChange
        {
            get
            {
                return IntMaskToIntArray(this.evStateChange, Enum.GetNames(typeof(SQL_Action)).Length);
            }
            set
            {
                this.evStateChange = value.Sum();
            }
        }

        public int[] _evOperatingModeChange
        {
            get
            {
                return IntMaskToIntArray(this.evOperatingModeChange, Enum.GetNames(typeof(SQL_Action)).Length);
            }
            set
            {
                this.evOperatingModeChange = value.Sum();
            }
        }

        public int[] _evConnectionChange
        {
            get
            {
                return IntMaskToIntArray(this.evConnectionChange, Enum.GetNames(typeof(SQL_Action)).Length);
            }
            set
            {
                this.evConnectionChange = value.Sum();
            }
        }

        public int[] _evExecutionStatus
        {
            get
            {
                return IntMaskToIntArray(this.evExecutionStatus, Enum.GetNames(typeof(SQL_Action)).Length);
            }
            set
            {
                this.evExecutionStatus = value.Sum();
            }
        }

        public int[] _evExecutionStatusTRob1
        {
            get
            {
                return IntMaskToIntArray(this.evExecutionStatusTRob1, Enum.GetNames(typeof(SQL_Action)).Length);
            }
            set
            {
                this.evExecutionStatusTRob1 = value.Sum();
            }
        }

        public int[] _evBackupCompleted
        {
            get
            {
                return IntMaskToIntArray(this.evBackupCompleted, Enum.GetNames(typeof(SQL_Action)).Length);
            }
            set
            {
                this.evBackupCompleted = value.Sum();
            }
        }

        public int[] _evDataResolveChange
        {
            get
            {
                return IntMaskToIntArray(this.evDataResolveChange, Enum.GetNames(typeof(SQL_Action)).Length);
            }
            set
            {
                this.evDataResolveChange = value.Sum();
            }
        }

        public int[] _evExecutionCycleChange
        {
            get
            {
                return IntMaskToIntArray(this.evExecutionCycleChange, Enum.GetNames(typeof(SQL_Action)).Length);
            }
            set
            {
                this.evExecutionCycleChange = value.Sum();
            }
        }

        public int[] _evTaskEnabledChange
        {
            get
            {
                return IntMaskToIntArray(this.evTaskEnabledChange, Enum.GetNames(typeof(SQL_Action)).Length);
            }
            set
            {
                this.evTaskEnabledChange = value.Sum();
            }
        }

        public int[] _evMasterChange
        {
            get
            {
                return IntMaskToIntArray(this.evMasterChange, Enum.GetNames(typeof(SQL_Action)).Length);
            }
            set
            {
                this.evMasterChange = value.Sum();
            }
        }

        public int[] _evMotionPointerTRob1Change
        {
            get
            {
                return IntMaskToIntArray(this.evMotionPointerTRob1Change, Enum.GetNames(typeof(SQL_Action)).Length);
            }
            set
            {
                this.evMotionPointerTRob1Change = value.Sum();
            }
        }

        public int[] _evProgramPointerTrob1Change
        {
            get
            {
                return IntMaskToIntArray(this.evProgramPointerTRob1Change, Enum.GetNames(typeof(SQL_Action)).Length);
            }
            set
            {
                this.evProgramPointerTRob1Change = value.Sum();
            }
        }

        public int[] _evMotionPointerTrob1ManualChange
        {
            get
            {
                return IntMaskToIntArray(this.evMotionPointerTRob1ManualChange, Enum.GetNames(typeof(SQL_Action)).Length);
            }
            set
            {
                this.evMotionPointerTRob1ManualChange = value.Sum();
            }
        }

        public int[] _evProgramPointerTrob1ManualChange
        {
            get
            {
                return IntMaskToIntArray(this.evProgramPointerTRob1ManualChange, Enum.GetNames(typeof(SQL_Action)).Length);
            }
            set
            {
                this.evProgramPointerTRob1ManualChange = value.Sum();
            }
        }

        public int[] _cVariableMask
        {
            get
            {
                return IntMaskToIntArray(this.cVariableMask, Enum.GetNames(typeof(Enable_bit_MASK)).Length);
            }
            set
            {
                this.cVariableMask = value.Sum();
            }
        }

        public int[] _cVariableSearchMask
        {
            get
            {
                return IntMaskToIntArray(this.cVariableSearchMask, Enum.GetNames(typeof(Enable_bit_MASK)).Length);
            }
            set
            {
                this.cVariableSearchMask = value.Sum();
            }
        }

        public int[] _cDeviceInfoMask
        {
            get
            {
                return IntMaskToIntArray(this.cDeviceInfoMask, Enum.GetNames(typeof(Enable_bit_MASK)).Length);
            }
            set
            {
                this.cDeviceInfoMask = value.Sum();
            }
        }

        public int[] _cCSVLogMask
        {
            get
            {
                return IntMaskToIntArray(this.cCSVLogMask, Enum.GetNames(typeof(Enable_bit_MASK)).Length);
            }
            set
            {
                this.cCSVLogMask = value.Sum();
            }
        }

        public int[] _cJobMask
        {
            get
            {
                return IntMaskToIntArray(this.cJobMask, Enum.GetNames(typeof(Enable_bit_MASK)).Length);
            }
            set
            {
                this.cJobMask = value.Sum();
            }
        }

        public int[] _cErrorNoLogMask
        {
            get
            {
                return IntMaskToIntArray(this.cErrorNoLogMask, Enum.GetNames(typeof(Enable_bit_MASK)).Length);
            }
            set
            {
                this.cErrorNoLogMask = value.Sum();
            }
        }

        public int[] _cAlarmIgnoreMask
        {
            get
            {
                return IntMaskToIntArray(this.cAlarmIgnoreMask, Enum.GetNames(typeof(Enable_bit_MASK)).Length);
            }
            set
            {
                this.cAlarmIgnoreMask = value.Sum();
            }
        }

        public int[] _cPJVEventMask
        {
            get
            {
                return IntMaskToIntArray(this.cPJVEventMask, Enum.GetNames(typeof(Enable_bit_MASK)).Length);
            }
            set
            {
                this.cPJVEventMask = value.Sum();
            }
        }


        //helper from int to array of int
        public int[] IntMaskToIntArray(int? inputValue, int enumLength)
        {
            //GHM 
            int value = inputValue ?? 0;
            List<int> listValue = new List<int>();
            for (int lcv = 0; lcv < enumLength; lcv++)
            {
                if ((value & (1 << lcv)) != 0)
                {
                    listValue.Add(1 << lcv);
                }
            }
            return listValue.ToArray();
        }

        //this links our enum to the entitymodel
        public List<SelectListItem> _SQL_Action
        {
            get
            {
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (string action in Enum.GetNames(typeof(SQL_Action)))
                {
                    if ((int)Enum.Parse(typeof(SQL_Action), action) == 0) continue; //skip 0 value (nothing active label)
                    SelectListItem listItem = new SelectListItem
                    {
                        Text = action,
                        Value = ((int)Enum.Parse(typeof(SQL_Action), action)).ToString(),
                    };
                    items.Add(listItem);
                }
                return items;
            }
            set
            {
                //does not need a set
            }

        }

        public List<SelectListItem> _Enable_bit_MASK
        {
            get
            {
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (string action in Enum.GetNames(typeof(Enable_bit_MASK)))
                {
                    if ((int)Enum.Parse(typeof(Enable_bit_MASK), action) == 0) continue; //skip 0 value (nothing active label)
                    SelectListItem listItem = new SelectListItem
                    {
                        Text = action,
                        Value = ((int)Enum.Parse(typeof(Enable_bit_MASK), action)).ToString(),
                    };
                    items.Add(listItem);
                }
                return items;
            }
            set
            {
                //does not need a set
            }
        }
    }
}