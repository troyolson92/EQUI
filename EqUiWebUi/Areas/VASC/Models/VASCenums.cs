﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.VASC.Models
{
    public static class VASCenums
    {
        //this makes a selectlist for the _sql_action 
        public static List<SelectListItem> SQL_Action_SelectList
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

        public static List<SelectListItem> Enable_bit_MASK_SelectList
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

        public static List<SelectListItem> ErrorFlags_SelectList
        {
            get
            {
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (string action in Enum.GetNames(typeof(ErrorFlags)))
                {
                    if ((int)Enum.Parse(typeof(ErrorFlags), action) == 0) continue; //skip 0 value (nothing active label)
                    SelectListItem listItem = new SelectListItem
                    {
                        Text = action,
                        Value = ((int)Enum.Parse(typeof(ErrorFlags), action)).ToString(),
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

        /// <summary>
        /// helper to  from int to an array of the bits set in that int
        /// </summary>
        /// <param name="inputValue">int value to check</param>
        /// <param name="enumLength">number of bits to check</param>
        /// <returns>array with decimal value of set bits</returns>
        public static int[] IntMaskToIntArray(int? inputValue, int enumLength)
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

        /// <summary>
        /// helper from array of int to bitmask 
        /// </summary>
        /// <param name="inputArray">array containing decimal value of set bits</param>
        /// <returns>returns -1 if all 16 bits are set else returns decimal sum of the set bits</returns>
        public static int IntArrayToIntMask(int[] inputArray)
        {
            if (inputArray.Count() >= 16)
            {
                return -1; 
            }
            else
            {
                return inputArray.Sum();
            }
        }

        /// <summary>
        /// Return a representation of the values in the array
        /// </summary>
        /// <param name="inputArray">Array of integer values</param>
        /// <returns>String representation</returns>
        public static string intArrayTostring(int[] inputArray)
        {
            if (inputArray.Length == 0)
            {
                return "disabled";
            }
            if (inputArray.Length >= 16)
            {
                return "all(-1)";
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder(inputArray.Length > 1 ? "bits(" : "bit(");
            foreach (int value in inputArray)
            {
                // Assume only one bit set per integer value
                for (int mask = 0; mask < 32; mask++)
                {
                    if ((value & (1 << mask)) != 0)
                    {
                        sb.Append("" + (mask + 1));
                        sb.Append(",");
                        break;
                    }
                }
            }
            sb[sb.Length - 1] = ')';
            return sb.ToString();
        }

        /// <summary>
        /// Return a string listing the bits set
        /// </summary>
        /// <param name="intValue">Integer value</param>
        /// <returns>String representation</returns>
        public static string intTostring(int inputValue, int enumLength)
        {
            if (inputValue == 0)
            {
                return "disabled";
            }
            if (inputValue == -1)
                return "all";
            System.Text.StringBuilder sb = new System.Text.StringBuilder(((inputValue & (inputValue - 1)) != 0) ? "bits(" : "bit(");
            for (int lcv = 0; lcv < enumLength; lcv++)
            {
                if ((inputValue & (1 << lcv)) != 0)
                {
                    sb.Append("" + (lcv + 1));
                    sb.Append(",");
                }
            }
            sb[sb.Length - 1] = ')';
            return sb.ToString();
        }

    }

    //graham his bitmasks
    public enum Poll_rate
    {
        SetupAsEvent = -1,
        ReadOnConnect = 0,
        //predefined times in miliseconds
        EverySecond = 1000,
        Every10Seconds = 1000 * 10,
        EveryMinute = 1000 * 60,
        Every5Minutes = 1000 * 60 * 5
    }

    public enum SQL_Action
    {
        noAction = 0,
        Insert_a_new_record = 0x01,
        Update_record = 0x02,
        Use_the_event_for_breakdown_analysis = 0x04,
        Update_the_rt_active_info = 0x08,
        When_used_in_breakdown_analysis_obtain_the_program_counter = 0x10,
        When_used_in_breakdown_analysis_obtain_the_motion_counter = 0x20
    }

    public enum Insert_update
    {
        Insert = 0,
        Update = 1
    }

    public enum Enable_bit
    {
        Disabled = 0,

        bit1 = 1,
        bit2 = 2,
        bit3 = 3,
        bit4 = 4,

        bit5 = 5,
        bit6 = 6,
        bit7 = 7,
        bit8 = 8,

        bit9 = 9,
        bit10 = 10,
        bit11 = 11,
        bit12 = 12,

        bit13 = 13,
        bit14 = 14,
        bit15 = 15,
        bit16 = 16
    }

    public enum Enable_bit_MASK
    {
        Disabled = 0,

        bit1 = 0x01,
        bit2 = 0x02,
        bit3 = 0x04,
        bit4 = 0x08,

        bit5 = 0x10,
        bit6 = 0x20,
        bit7 = 0x40,
        bit8 = 0x80,

        bit9 = 0x100,
        bit10 = 0x200,
        bit11 = 0x400,
        bit12 = 0x800,

        bit13 = 0x1000,
        bit14 = 0x2000,
        bit15 = 0x4000,
        bit16 = 0x8000
    }

    public enum Csv_log_Flags
    {
        obtained_PCSDK = 0x1,
        obtained_FTP = 0x2
    }

    public enum LogicOperator
    {
        AND = 0,
        OR = 1,
        XOR = 2
    }

    public enum ErrorFlags
    {
        Insert_into_h_alarm = 0x01,
        Insert_into_rt_alarm = 0x02,
        Used_in_breakdown = 0x04
    }

    public enum ControllerFlags
    {
       noflags = 0x0,
       allowPjvAction= 0x01
    }

    public enum VASCState
    {
        STATE_NOTHING = 0, /* Done nothing */
        STATE_CONNECTED = 1, /* Fully connected */
        STATE_CONNECTING = 2, /* Processing of connecting */

        STATE_NO_CONN = -1, /* ABB error no connection */
        STATE_LOST_CONN = -2, /* Lost connection */
        STATE_NO_PING = -3, /* No ping */
        STATE_NO_NAME = -4, /* No Name */
        STATE_NO_IP_SYSID = -5, /* No IP/SYSID */
        STATE_NEW_CONTROLLER = -6,
        STATE_BAD_FORMAT = -7, 
        NO_CONTROLLER = -8, //not abb robot
        STATE_NO_INFO = -9,
        STATE_RUN_LEVEL = -10,
        STATE_AVAIL = -11,
        STATE_BAD_IP = -12,
        STATE_SYS_FAIL = -13,
        STATE_NO_PCSDK = -14,
        STATE_VASC_SHUTDOWN = -15 /*Vasc Shutdown*/
    }

    public enum ErrorCategory
    {
	    Common = 0,
	    Operational = 1,
	    System = 2,
	    Hardware = 3,
	    Program = 4,
	    Motion = 5,
	    Operator = 6,
	    IOCommunication = 7,
	    User = 8,
	    Safemove = 9,
	    Internal = 10,
        Process = 11,
	    Configuration = 12,
	    Paint = 13,
	    Picker = 14
    }

    public enum Event_code
    {
        noEvent = 0, // ask GM if this is oke? 
        //ABB
        EVENT_CONTROLLER_OPERATING_MODE = 1,
        EVENT_CONTROLLER_LOG = 2,
        EVENT_SHUTDOWN = 3,
        EVENT_LOG_MESSAGE = 4,
        EVENT_CSV_FILE = 5,
        EVENT_SIGNAL = 6,
        EVENT_VARIABLE_SEARCH = 7,
        EVENT_CYCLE_CHANGE = 8,
        EVENT_RAPID_DATA_CHANGE = 8,
        EVENT_PROGRAM_POSITION = 10,
        EVENT_MOTION_POSITION = 11,
        EVENT_RAPID_DATA_RESOLVE = 12,
        EVENT_BACKUP = 13,
        EVENT_MASTER = 14,
        EVENT_TASK_ENABLED = 15,
        EVENT_EXECUTION_CHANGE = 16,
        EVENT_CONNECTION_CHANGE = 17,
        EVENT_STATE_CHANGE = 18,
        EVENT_CLEAR_LOGS = 19,
        EVENT_POLL = 20,
        EVENT_DEVICE_INFO = 21,
        EVENT_TASK_EXECUTION_CHANGE = 22,
        EVENT_DEAD = 23,
        EVENT_PJV_ACTION = 24,
        //Programmable codes
        Cycle_start_code = 100,
        Body_number = 101,
        Application_error  = 102, //(0 in alarm, 1 not in alarm)
        Cycle_end_code = 103,
        Part_data_count = 104, 
        //Register code
        Register_1 = 200,
        Register_2 = 201,
        Register_3 = 202,
        Register_4 = 203,
        Register_5 = 204,
        Register_6 = 205,
        Register_7 = 206,
        Register_8 = 207,

    }

    //From PCSDK for C_variable_search
    public enum SymbolTypes
    {
        //
        // Summary:
        //     No type.
        None = 0,
        //
        // Summary:
        //     All atomic types.
        Atomic = 1,
        //
        // Summary:
        //     Records.
        Record = 2,
        //
        // Summary:
        //     Aliases.
        Alias = 4,
        //
        // Summary:
        //     Record components.
        RecordComponent = 8,
        //
        // Summary:
        //     Constants.
        Constant = 16,
        //
        // Summary:
        //     Variables.
        Variable = 32,
        //
        // Summary:
        //     Persistent data.
        Persistent = 64,
        //
        // Summary:
        //     Data, Constant | Variable | Persistent
        Data = 112,
        //
        // Summary:
        //     Parameters.
        Parameter = 128,
        //
        // Summary:
        //     Labels.
        Label = 256,
        //
        // Summary:
        //     Functions.
        Function = 1024,
        //
        // Summary:
        //     Procedures.
        Procedure = 2048,
        //
        // Summary:
        //     Traps.
        Trap = 4096,
        //
        // Summary:
        //     Routines, Function | Procedures | Trap.
        Routine = 7168,
        //
        // Summary:
        //     Modules.
        Module = 8192,
        //
        // Summary:
        //     Tasks.
        Task = 16384
    }

    public enum L_operationCode
    {
        Session_started = 1,
        Session_stopped = 2,
        Controller_connected = 3,
        Ping_status = 4,
        Error = 5
    }

}
