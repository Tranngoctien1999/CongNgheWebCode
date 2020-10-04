namespace BanVeDiTourDuLich
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiNhanVien")]
    public partial class LoaiNhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiNhanVien()
        {
            NhanViens = new HashSet<NhanVien>();
            ThongTinLoaiQuyenLoaiNhanViens = new HashSet<ThongTinLoaiQuyenLoaiNhanVien>();
        }

        [Key]
        [StringLength(20)]
        public string MaLoaiNhanVien { get; set; }

        [Required]
        [StringLength(50)]
        public string TenLoaiNhanVien { get; set; }

        public string ChiTiet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhanVien> NhanViens { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongTinLoaiQuyenLoaiNhanVien> ThongTinLoaiQuyenLoaiNhanViens { get; set; }
    }
}
