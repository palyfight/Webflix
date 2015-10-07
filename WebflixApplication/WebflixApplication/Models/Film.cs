namespace WebflixApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPE23.FILM")]
    public partial class FILM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FILM()
        {
            ANNONCEs = new HashSet<ANNONCE>();
            COPIEs = new HashSet<COPIE>();
            FILMGENREs = new HashSet<FILMGENRE>();
            FILMPAYS = new HashSet<FILMPAY>();
            SCENARISTEs = new HashSet<SCENARISTE>();
            ROLEs = new HashSet<ROLE>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IDFILM { get; set; }

        [Required]
        [StringLength(150)]
        public string TITRE { get; set; }

        public DateTime DATEPARUTION { get; set; }

        public decimal? DUREEFILM { get; set; }

        public string SCENARIO { get; set; }

        [StringLength(200)]
        public string POSTER { get; set; }

        public decimal NBCOPIE { get; set; }

        [StringLength(25)]
        public string LANGUEORIGINALE { get; set; }

        public decimal? IDREALISATEUR { get; set; }

        public List<FILM> FILMS { get; set; }

        [Required]
        [StringLength(25)]
        public string CODE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ANNONCE> ANNONCEs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COPIE> COPIEs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FILMGENRE> FILMGENREs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FILMPAY> FILMPAYS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SCENARISTE> SCENARISTEs { get; set; }

        public virtual PERSONNESFILM PERSONNESFILM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ROLE> ROLEs { get; set; }
    }
}
