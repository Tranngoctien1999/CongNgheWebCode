namespace BanVeDiTourDuLich
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon()
        {
            Ves = new HashSet<Ve>();
        }

        [Key]
        [StringLength(20)]
        public string MaHoaDon { get; set; }

        public DateTime ThoiGianXuat { get; set; }

        [Required]
        [StringLength(50)]
        public string MaKhachHang { get; set; }

        [Required]
        [StringLength(50)]
        public string MaNhanVien { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ve> Ves { get; set; }
    }
}
