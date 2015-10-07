namespace WebflixApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPE23.SCENARISTE")]
    public partial class SCENARISTE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IDSCENARISTE { get; set; }

        [StringLength(75)]
        public string NOM { get; set; }

        public decimal? IDFILM { get; set; }

        public virtual FILM FILM { get; set; }
    }
}
