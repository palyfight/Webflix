namespace WebflixApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPE23.CLIENT_W")]
    public partial class CLIENT_W
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLIENT_W()
        {
            LOCATION_W = new HashSet<LOCATION_W>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IDCLIENT { get; set; }

        [Required]
        [StringLength(30)]
        public string NOM { get; set; }

        [Required]
        [StringLength(30)]
        public string PRENOM { get; set; }

        public decimal GROUPEAGE { get; set; }

        public DateTime DATEPLOCATION { get; set; }

        [Required]
        [StringLength(10)]
        public string CODEPOSTAL { get; set; }

        [Required]
        [StringLength(30)]
        public string VILLE { get; set; }

        [Required]
        [StringLength(30)]
        public string PROVINCE { get; set; }

        public DateTime DATENAISSANCE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOCATION_W> LOCATION_W { get; set; }
    }
}
