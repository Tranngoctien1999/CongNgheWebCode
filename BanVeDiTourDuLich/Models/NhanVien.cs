namespace BanVeDiTourDuLich
{
    using BanVeDiTourDuLich.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            HoaDons = new HashSet<HoaDon>();
        }
        
        [Key]
        [StringLength(20)]
        public string MaNhanVien { get; set; }

        [Required]
        [StringLength(50)]
        public string Ten { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgaySinh { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayVaoLam { get; set; }

        public double Luong { get; set; }

        [Required]
        [StringLength(20)]
        public string MaLoaiNhanVien { get; set; }
        


        public string DuongDanAnh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
        public virtual LoaiNhanVien LoaiNhanVien { get; set; }
        public virtual ICollection<TinNhan> TinNhans { get; set; }

    }
}
