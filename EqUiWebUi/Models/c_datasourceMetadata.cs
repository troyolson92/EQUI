using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace EqUiWebUi.Models
{
    public class c_datasourceMetadata
    {
    }

    [MetadataType(typeof(c_datasource))]
    public partial class c_datasource
    {

        public EQUICommunictionLib.db_type _db_type
        {
            get
            {
                return (EQUICommunictionLib.db_type)Enum.ToObject(typeof(EQUICommunictionLib.db_type), this.Type);
            }
            set
            {
                this.Type = (int)value;
            }
        }

    }
}