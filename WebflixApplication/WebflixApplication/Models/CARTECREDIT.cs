namespace WebflixApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EQUIPE23.CARTECREDIT")]
    public partial class CARTECREDIT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CARTECREDIT()
        {
            CLIENTs = new HashSet<CLIENT>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IDCARTECREDIT { get; set; }

        [Required]
        [StringLength(30)]
        public string TYPE { get; set; }

        [Required]
        [StringLength(20)]
        public string NUMERO { get; set; }

        public DateTime DATEEXPIRATION { get; set; }

        public decimal CVV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLIENT> CLIENTs { get; set; }
    }
}
