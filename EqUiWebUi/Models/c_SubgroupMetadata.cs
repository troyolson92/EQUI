using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Models
{
    public class c_SubgroupMetadata
    {
    }

    [MetadataType(typeof(c_Subgroup))]
    public partial class c_Subgroup
    {

        public string _Displayname
        {
            get
            {
                return this.Subgroup + string.Format(" ({0})", this.Discription);
            }
            set
            {
                //do nothing
            }       
        }
    
    }
}