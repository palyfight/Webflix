namespace WebflixApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPE23.PERSONNESFILM")]
    public partial class PERSONNESFILM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PERSONNESFILM()
        {
            FILMs = new HashSet<FILM>();
            ROLEs = new HashSet<ROLE>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IDPERSONNEFILM { get; set; }

        [Required]
        [StringLength(30)]
        public string NOM { get; set; }

        [Required]
        [StringLength(30)]
        public string PRENOM { get; set; }

        public string BIO { get; set; }

        [Required]
        [StringLength(100)]
        public string LIEUDENAISSANCE { get; set; }

        [StringLength(200)]
        public string PHOTO { get; set; }

        public DateTime? DATEDENAISSANCE { get; set; }

        public decimal? CODE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FILM> FILMs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ROLE> ROLEs { get; set; }
    }
}
