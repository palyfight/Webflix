namespace WebflixApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPE23.EMPLOYE")]
    public partial class EMPLOYE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IDEMPLOYE { get; set; }

        public decimal MATRICULE { get; set; }

        public decimal? IDPERSONNE { get; set; }

        public virtual PERSONNE PERSONNE { get; set; }
    }
}
