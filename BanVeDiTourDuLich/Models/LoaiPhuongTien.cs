namespace BanVeDiTourDuLich
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiPhuongTien")]
    public partial class LoaiPhuongTien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiPhuongTien()
        {
            PhuongTiens = new HashSet<PhuongTien>();
        }

        [Key]
        [StringLength(20)]
        public string MaLoaiPhuongTien { get; set; }

        [Required]
        [StringLength(50)]
        public string Ten { get; set; }

        public string ChiTiet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhuongTien> PhuongTiens { get; set; }
    }
}
