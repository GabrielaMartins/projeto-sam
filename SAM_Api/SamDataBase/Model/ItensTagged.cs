//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SamDataBase.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ItensTagged
    {
        public int id { get; set; }
        public Nullable<int> item { get; set; }
        public Nullable<int> tag { get; set; }
    
        public virtual Iten Item { get; set; }
        public virtual Tag Tag { get; set; }
    }
}