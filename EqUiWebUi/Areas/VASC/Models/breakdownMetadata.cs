using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Areas.VASC.Models
{
    public class breakdownMetadata
    {
    }

    //L_error metadata Exetension to get a cleaned up logtext
    [MetadataType(typeof(L_error))]
    public partial class L_error
    {
        //this is a short logtext representation
        public string _Logtext
        {
            get
            {
                if (this.Title.Contains("ErrDisplay") || this.Title.Contains("External weld fault reported"))
                {
                    return this.L_description.Description.Trim();
                }
                else
                {
                    return this.Title.Trim();
                }
            }
        }

        //this is a full logtext representation
        public string _FullLogtext
        {
            get
            {
                if (this.Title.Contains("ErrDisplay") || this.Title.Contains("External weld fault reported"))
                {
                    return this.L_description.Description.Trim();
                }
                else
                {
                    return this.Title.Trim() + Environment.NewLine + this.L_description.Description.Trim();
                }
            }
        }


    }
}