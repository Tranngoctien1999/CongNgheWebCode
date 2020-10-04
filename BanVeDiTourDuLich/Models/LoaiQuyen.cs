namespace BanVeDiTourDuLich
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiQuyen")]
    public partial class LoaiQuyen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiQuyen()
        {
            Quyens = new HashSet<Quyen>();
            ThongTinLoaiQuyenLoaiNhanViens = new HashSet<ThongTinLoaiQuyenLoaiNhanVien>();
        }

        [Key]
        [StringLength(10)]
        public string MaLoaiQuyen { get; set; }

        [Required]
        [StringLength(50)]
        public string TenLoaiQuyen { get; set; }

        public string ChiTiet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quyen> Quyens { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongTinLoaiQuyenLoaiNhanVien> ThongTinLoaiQuyenLoaiNhanViens { get; set; }
    }
}
