namespace BanVeDiTourDuLich
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhuongTien")]
    public partial class PhuongTien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhuongTien()
        {
            ChiTietPhuongTiens = new HashSet<ChiTietPhuongTien>();
        }

        [Key]
        [StringLength(20)]
        public string MaPhuongTien { get; set; }

        [Required]
        [StringLength(50)]
        public string Ten { get; set; }

        [Required]
        [StringLength(20)]
        public string MaLoaiPhuongTien { get; set; }

        public virtual LoaiPhuongTien LoaiPhuongTien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhuongTien> ChiTietPhuongTiens { get; set; }
    }
}
