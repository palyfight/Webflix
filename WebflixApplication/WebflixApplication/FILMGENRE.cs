//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebflixApplication
{
    using System;
    using System.Collections.Generic;
    
    public partial class FILMGENRE
    {
        public decimal IDFILMGENRE { get; set; }
        public Nullable<decimal> IDFILM { get; set; }
        public Nullable<decimal> IDGENRE { get; set; }
    
        public virtual FILM FILM { get; set; }
        public virtual GENRE GENRE { get; set; }
    }
}
