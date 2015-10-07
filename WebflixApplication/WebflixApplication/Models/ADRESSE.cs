namespace WebflixApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPE23.ADRESSE")]
    public partial class ADRESSE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADRESSE()
        {
            PERSONNEs = new HashSet<PERSONNE>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IDADRESSE { get; set; }

        public decimal NUMCIVIQUE { get; set; }

        [Required]
        [StringLength(75)]
        public string RUE { get; set; }

        [Required]
        [StringLength(30)]
        public string VILLE { get; set; }

        [Required]
        [StringLength(30)]
        public string PROVINCE { get; set; }

        [Required]
        [StringLength(10)]
        public string CODEPOSTAL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PERSONNE> PERSONNEs { get; set; }
    }
}
