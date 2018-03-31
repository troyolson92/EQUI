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
        Disabled = -1,

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
}
