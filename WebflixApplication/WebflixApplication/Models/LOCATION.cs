namespace WebflixApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPE23.LOCATION")]
    public partial class LOCATION
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IDLOCATION { get; set; }

        public DateTime? DATEDELOCATION { get; set; }

        public DateTime? DATERETOUR { get; set; }

        public decimal? IDCOPIE { get; set; }

        public decimal? IDCLIENT { get; set; }

        public virtual CLIENT CLIENT { get; set; }

        public virtual COPIE COPIE { get; set; }
    }
}
