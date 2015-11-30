namespace WebflixApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPE23.LOCATION_W")]
    public partial class LOCATION_W
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IDLOCATION { get; set; }

        public decimal IDFILM { get; set; }

        public decimal IDCLIENT { get; set; }

        public decimal IDDATE { get; set; }

        public decimal GROUPEAGE { get; set; }

        [Required]
        [StringLength(30)]
        public string PROVINCE { get; set; }

        public decimal JOUR { get; set; }

        public decimal MOIS { get; set; }

        public virtual CLIENT_W CLIENT_W { get; set; }

        public virtual DATE_W DATE_W { get; set; }

        public virtual FILM_W FILM_W { get; set; }
    }
}
