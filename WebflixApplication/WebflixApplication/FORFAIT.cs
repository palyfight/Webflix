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
    
    public partial class FORFAIT
    {
        public FORFAIT()
        {
            this.CLIENTs = new HashSet<CLIENT>();
        }
    
        public decimal IDFORFAIT { get; set; }
        public string NOM { get; set; }
        public decimal PRIX { get; set; }
        public decimal LOCATIONSMAX { get; set; }
        public decimal DUREEMAX { get; set; }
    
        public virtual ICollection<CLIENT> CLIENTs { get; set; }
    }
}
