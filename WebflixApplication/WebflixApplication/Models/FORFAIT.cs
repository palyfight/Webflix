namespace WebflixApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPE23.FORFAIT")]
    public partial class FORFAIT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FORFAIT()
        {
            CLIENTs = new HashSet<CLIENT>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IDFORFAIT { get; set; }

        [Required]
        [StringLength(15)]
        public string NOM { get; set; }

        public decimal PRIX { get; set; }

        public decimal LOCATIONSMAX { get; set; }

        public decimal DUREEMAX { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLIENT> CLIENTs { get; set; }
    }
}
