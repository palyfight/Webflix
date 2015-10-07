namespace WebflixApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPE23.PERSONNE")]
    public partial class PERSONNE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PERSONNE()
        {
            CLIENTs = new HashSet<CLIENT>();
            EMPLOYEs = new HashSet<EMPLOYE>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IDPERSONNE { get; set; }

        [Required]
        [StringLength(30)]
        public string NOM { get; set; }

        [Required]
        [StringLength(30)]
        public string PRENOM { get; set; }

        [Required]
        [StringLength(50)]
        public string COURRIEL { get; set; }

        [Required]
        [StringLength(15)]
        public string TELEPHONE { get; set; }

        public DateTime? DATEDENAISSANCE { get; set; }

        [StringLength(255)]
        public string MOTDEPASSE { get; set; }

        public decimal? IDADRESSE { get; set; }

        public virtual ADRESSE ADRESSE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLIENT> CLIENTs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMPLOYE> EMPLOYEs { get; set; }
    }
}
