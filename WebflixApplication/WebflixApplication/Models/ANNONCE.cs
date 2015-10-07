namespace WebflixApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPE23.ANNONCE")]
    public partial class ANNONCE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IDANNONCE { get; set; }

        [Required]
        [StringLength(250)]
        public string LIEN { get; set; }

        public decimal? IDFILM { get; set; }

        public virtual FILM FILM { get; set; }
    }
}
