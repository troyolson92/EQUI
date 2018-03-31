using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.VASC.Models
{
    public class VASCenums
    {
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

        Insert_a_new_record_into_the_rt_event_table = 0x01,
        Update_the_last_record_for_this_controller_event = 0x02,
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

        Enablebit1 = 1,
        Enablebit2 = 2,
        Enablebit3 = 3,
        Enablebit4 = 4,

        Enablebit5 = 5,
        Enablebit6 = 6,
        Enablebit7 = 7,
        Enablebit8 = 8,

        Enablebit9 = 9,
        Enablebit10 = 10,
        Enablebit11 = 11,
        Enablebit12 = 12,

        Enablebit13 = 13,
        Enablebit14 = 14,
        Enablebit15 = 15,
        Enablebit16 = 16
    }

    public enum Enable_bit_MASK
    {
        Disabled = 0,

        Enablebit1 = 0x01,
        Enablebit2 = 0x02,
        Enablebit3 = 0x04,
        Enablebit4 = 0x08,

        Enablebit5 = 0x10,
        Enablebit6 = 0x20,
        Enablebit7 = 0x40,
        Enablebit8 = 0x80,

        Enablebit9 = 0x100,
        Enablebit10 = 0x200,
        Enablebit11 = 0x400,
        Enablebit12 = 0x800,

        Enablebit13 = 0x1000,
        Enablebit14 = 0x2000,
        Enablebit15 = 0x4000,
        Enablebit16 = 0x8000
    }

    public enum Csv_log_Flags
    {
        obtained_PCSDK = 0x1,
        obtained_FTP = 0x2
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

}
