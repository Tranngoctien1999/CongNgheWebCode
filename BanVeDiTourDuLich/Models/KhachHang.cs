using BanVeDiTourDuLich.Models;

namespace BanVeDiTourDuLich
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        [Key]
        [StringLength(50)]
        public string MaKhachHang { get; set; }

        [Required]
        [StringLength(50)]
        public string Ten { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgaySinh { get; set; }

        public DateTime ThoiGianDangKi { get; set; }

        [Required]
        [StringLength(20)]
        public string MaLoaiKhachHang { get; set; }

        public string DuongDanAnh { get; set; }

        public string DiaChi { get; set; }

        public bool GioiTinh { get; set; }

        public string Email { get; set; }

        public string SoDienThoai { get; set; }

        public virtual ICollection<NhanXet> NhanXets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual ICollection<TinNhan> TinNhans { get; set; }
        public virtual LoaiKhachHang LoaiKhachHang { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
