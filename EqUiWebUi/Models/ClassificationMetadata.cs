using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Models
{
    public class ClassificationMetadata
    {
    }

    [MetadataType(typeof(c_Classification))]
    public partial class c_Classification
    {

        public string _Displayname
        {
            get
            {
                return this.Classification + string.Format(" ({0})", this.Discription);
            }
            set
            {
                //do nothing
            }
        }

    }
}