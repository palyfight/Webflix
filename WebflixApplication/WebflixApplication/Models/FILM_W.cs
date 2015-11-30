namespace WebflixApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPE23.FILM_W")]
    public partial class FILM_W
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FILM_W()
        {
            LOCATION_W = new HashSet<LOCATION_W>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IDFILM { get; set; }

        public decimal CODE { get; set; }

        [Required]
        [StringLength(150)]
        public string TITRE { get; set; }

        public decimal ANNEEPARUTION { get; set; }

        [StringLength(50)]
        public string ORIGINE { get; set; }

        public byte ACTION { get; set; }

        public byte ADVENTURE { get; set; }

        public byte COMEDY { get; set; }

        public byte FAMILY { get; set; }

        public byte ROMANCE { get; set; }

        public byte DRAMA { get; set; }

        public byte ANIMATION { get; set; }

        public byte FANTASY { get; set; }

        public byte BIOGRAPHY { get; set; }

        public byte THRILLER { get; set; }

        public byte SCI_FI { get; set; }

        public byte CRIME { get; set; }

        public byte SPORT { get; set; }

        public byte HORROR { get; set; }

        public byte FILM_NOIR { get; set; }

        public byte MYSTERY { get; set; }

        public byte WESTERN { get; set; }

        public byte WAR { get; set; }

        public byte MUSICAL { get; set; }

        public byte DOCUMENTARY { get; set; }

        public byte HISTORY { get; set; }

        public byte MUSIC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOCATION_W> LOCATION_W { get; set; }
    }
}
