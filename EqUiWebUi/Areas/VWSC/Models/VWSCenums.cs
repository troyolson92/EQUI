using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.VWSC.Models
{
    public class VWSCenums
    {
        //helper from int to array of int
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

        //for c_bosch view
        public enum Poll_rate
        {
            SetupAsEvent = -1,
            ReadOnConnect = 0,
            //predefined times in miliseconds
            EverySecond = 1000,
            Every2Seconds = 1000 * 2,
            Every10Seconds = 1000 * 10,
            EveryMinute = 1000 * 60,
            Every5Minutes = 1000 * 60 * 5
        }

        public enum Insert_update
        {
            Insert = 0,
            Update = 1
        }

        //for c_error
        public enum LogicOperator
        {
            AND = 0,
            OR = 1,
            XOR = 2,
            IGNORE = 3
        }

        public enum c_error_flags
        {
            Insert_into_rt_alarm = 0x2,
            Use_for_breakdown_analysis = 0x4,
            Marks_the_end_breakdown = 0x8
        }

        public enum c_error_isError
        {
            WarningOrError = -1,
            Warning = 0,
            Error = 1
        }

        //global use
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
            Enablebit16 = 16,

            Enablebit17 = 17,
            Enablebit18 = 18,
            Enablebit19 = 19,
            Enablebit20 = 20,

            Enablebit21 = 21,
            Enablebit22 = 22,
            Enablebit23 = 23,
            Enablebit24 = 23,

            Enablebit25 = 25,
            Enablebit26 = 26,
            Enablebit27 = 27,
            Enablebit28 = 28,

            Enablebit29 = 29,
            Enablebit30 = 30,
            Enablebit31 = 31
         //   Enablebit32 = 32
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
            Enablebit16 = 0x8000,

            Enablebit17 = 0x10000,
            Enablebit18 = 0x20000,
            Enablebit19 = 0x40000,
            Enablebit20 = 0x80000,

            Enablebit21 = 0x100000,
            Enablebit22 = 0x200000,
            Enablebit23 = 0x400000,
            Enablebit24 = 0x800000,

            Enablebit25 = 0x1000000,
            Enablebit26 = 0x2000000,
            Enablebit27 = 0x4000000,
            Enablebit28 = 0x8000000,

            Enablebit29 = 0x10000000,
            Enablebit30 = 0x20000000,
            Enablebit31 = 0x40000000
          //  Enablebit32 = 0x80000000
        }

        public enum VWSCState
        {
            STATE_CONNECTED = 9999 //not in manual ! 
        }

        public enum VWSCSessionType
        {
            Master = 1,
            Slave = 2
        }

    }
}