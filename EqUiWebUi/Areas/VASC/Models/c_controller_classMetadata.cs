using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.VASC.Models;

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
                return VASCenums.IntMaskToIntArray(this.evStateChange, Enum.GetNames(typeof(SQL_Action)).Length);
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
                return VASCenums.IntMaskToIntArray(this.evOperatingModeChange, Enum.GetNames(typeof(SQL_Action)).Length);
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
                return VASCenums.IntMaskToIntArray(this.evConnectionChange, Enum.GetNames(typeof(SQL_Action)).Length);
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
                return VASCenums.IntMaskToIntArray(this.evExecutionStatus, Enum.GetNames(typeof(SQL_Action)).Length);
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
                return VASCenums.IntMaskToIntArray(this.evExecutionStatusTRob1, Enum.GetNames(typeof(SQL_Action)).Length);
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
                return VASCenums.IntMaskToIntArray(this.evBackupCompleted, Enum.GetNames(typeof(SQL_Action)).Length);
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
                return VASCenums.IntMaskToIntArray(this.evDataResolveChange, Enum.GetNames(typeof(SQL_Action)).Length);
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
                return VASCenums.IntMaskToIntArray(this.evExecutionCycleChange, Enum.GetNames(typeof(SQL_Action)).Length);
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
                return VASCenums.IntMaskToIntArray(this.evTaskEnabledChange, Enum.GetNames(typeof(SQL_Action)).Length);
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
                return VASCenums.IntMaskToIntArray(this.evMasterChange, Enum.GetNames(typeof(SQL_Action)).Length);
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
                return VASCenums.IntMaskToIntArray(this.evMotionPointerTRob1Change, Enum.GetNames(typeof(SQL_Action)).Length);
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
                return VASCenums.IntMaskToIntArray(this.evProgramPointerTRob1Change, Enum.GetNames(typeof(SQL_Action)).Length);
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
                return VASCenums.IntMaskToIntArray(this.evMotionPointerTRob1ManualChange, Enum.GetNames(typeof(SQL_Action)).Length);
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
                return VASCenums.IntMaskToIntArray(this.evProgramPointerTRob1ManualChange, Enum.GetNames(typeof(SQL_Action)).Length);
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
                return VASCenums.IntMaskToIntArray(this.cVariableMask, Enum.GetNames(typeof(Enable_bit_MASK)).Length);
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
                return VASCenums.IntMaskToIntArray(this.cVariableSearchMask, Enum.GetNames(typeof(Enable_bit_MASK)).Length);
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
                return VASCenums.IntMaskToIntArray(this.cDeviceInfoMask, Enum.GetNames(typeof(Enable_bit_MASK)).Length);
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
                return VASCenums.IntMaskToIntArray(this.cCSVLogMask, Enum.GetNames(typeof(Enable_bit_MASK)).Length);
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
                return VASCenums.IntMaskToIntArray(this.cJobMask, Enum.GetNames(typeof(Enable_bit_MASK)).Length);
            }
            set
            {
                this.cJobMask = value.Sum();
            }
        }

        public int[] _cErrorMask
        {
            get
            {
                return VASCenums.IntMaskToIntArray(this.cErrorMask, Enum.GetNames(typeof(Enable_bit_MASK)).Length);
            }
            set
            {
                this.cErrorMask = value.Sum();
            }
        }

        public int[] _cPJVEventMask
        {
            get
            {
                return VASCenums.IntMaskToIntArray(this.cPJVEventMask, Enum.GetNames(typeof(Enable_bit_MASK)).Length);
            }
            set
            {
                this.cPJVEventMask = value.Sum();
            }
        }

    }
}