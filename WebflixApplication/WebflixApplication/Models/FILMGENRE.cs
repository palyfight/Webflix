namespace WebflixApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPE23.FILMGENRE")]
    public partial class FILMGENRE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IDFILMGENRE { get; set; }

        public decimal? IDFILM { get; set; }

        public decimal? IDGENRE { get; set; }

        public virtual FILM FILM { get; set; }

        public virtual GENRE GENRE { get; set; }
    }
}
