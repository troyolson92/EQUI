using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.VASC.Models
{
    public class c_jobMetadata
    {
    }

    //c_job meta data
    [MetadataType(typeof(c_job))]
    public partial class c_job
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

        public int[] _Flags
        {
            get
            {
                return VASCenums.IntMaskToIntArray(this.flags, Enum.GetNames(typeof(c_jobFlags)).Length);
            }
            set
            {
                this.flags = value.Sum();
            }
        }

        public static List<SelectListItem> c_jobFlags_MASK_SelectList
        {
            get
            {
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (string action in Enum.GetNames(typeof(c_jobFlags)))
                {
                    if ((int)Enum.Parse(typeof(c_jobFlags), action) == 0) continue; //skip 0 value (nothing active label)
                    SelectListItem listItem = new SelectListItem
                    {
                        Text = action,
                        Value = ((int)Enum.Parse(typeof(c_jobFlags), action)).ToString(),
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

    public enum c_jobFlags
    {
        noAction = 0,
        AlwaysInsertRt_job = 0x01, //always insert into the rt_job. if not set only insert into rt_job if a breakdown.
        WriteOutBreakdownDetails = 0x02, //write out detailed information on the breakdown
        WriteoutToFile = 0x04 //write the date to an ascii file
    }
}