namespace WebflixApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPE23.ROLE")]
    public partial class ROLE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IDROLE { get; set; }

        [Required]
        [StringLength(75)]
        public string PERSONNAGE { get; set; }

        public decimal? IDACTEUR { get; set; }

        public decimal? IDFILM { get; set; }

        public virtual FILM FILM { get; set; }

        public virtual PERSONNESFILM PERSONNESFILM { get; set; }
    }
}
