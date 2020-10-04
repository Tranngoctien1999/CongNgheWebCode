namespace BanVeDiTourDuLich
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MaGiamGia")]
    public partial class MaGiamGia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MaGiamGia()
        {
            Ves = new HashSet<Ve>();
        }

        [Key]
        [Column("MaGiamGia")]
        [StringLength(20)]
        public string MaGiamGia1 { get; set; }

        [Required]
        [StringLength(50)]
        public string Ten { get; set; }

        [Required]
        public string ChiTiet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ve> Ves { get; set; }
    }
}
