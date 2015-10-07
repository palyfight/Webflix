namespace WebflixApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPE23.FILMPAYS")]
    public partial class FILMPAY
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IDFILMPAYS { get; set; }

        public decimal? IDPAYS { get; set; }

        public decimal? IDFILM { get; set; }

        public virtual FILM FILM { get; set; }

        public virtual PAY PAY { get; set; }
    }
}
