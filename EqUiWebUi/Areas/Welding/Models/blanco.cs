//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Areas.Welding.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class blanco
    {
        public string pjvspotid { get; set; }
        public Nullable<int> pictureID { get; set; }
        public Nullable<int> TDTTypeID { get; set; }
        public Nullable<int> FoutcodeID { get; set; }
        public string StartComment { get; set; }
        public int Plates { get; set; }
        public Nullable<int> BodyNbr { get; set; }
        public decimal Nomdiameter { get; set; }
        public int Id { get; set; }
        public int Type_ID { get; set; }
        public int Picture_ID { get; set; }
    
        public virtual picture picture { get; set; }
        public virtual picture picture1 { get; set; }
        public virtual type type { get; set; }
        public virtual type type1 { get; set; }
    }
}
