namespace WebflixApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPE23.CLIENT")]
    public partial class CLIENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLIENT()
        {
            LOCATIONs = new HashSet<LOCATION>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IDCLIENT { get; set; }

        public decimal? IDPERSONNE { get; set; }

        public decimal? IDCARTECREDIT { get; set; }

        public decimal? IDFORFAIT { get; set; }

        public virtual CARTECREDIT CARTECREDIT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOCATION> LOCATIONs { get; set; }

        public virtual FORFAIT FORFAIT { get; set; }

        public virtual PERSONNE PERSONNE { get; set; }
    }
}
