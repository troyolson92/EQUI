//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Areas.VASC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class c_error
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public c_error()
        {
            this.Ignore = "";
        }
    
        public int id { get; set; }
        public int enable_bit { get; set; }
        public int error_number { get; set; }
        public int error_category { get; set; }
        public int error_number_mask { get; set; }
        public int error_category_mask { get; set; }
        public int C_operator { get; set; }
        public int flags { get; set; }
        public string Ignore { get; set; }
        public string UserComment { get; set; }
        public Nullable<int> ordinal { get; set; }
    }
}
