namespace WebflixApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPE23.DATE_W")]
    public partial class DATE_W
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DATE_W()
        {
            LOCATION_W = new HashSet<LOCATION_W>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IDDATE { get; set; }

        public DateTime SIMPLE_DATE { get; set; }

        public decimal HEURE { get; set; }

        public decimal JOUR { get; set; }

        public decimal MOIS { get; set; }

        public decimal ANNEE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOCATION_W> LOCATION_W { get; set; }
    }
}
