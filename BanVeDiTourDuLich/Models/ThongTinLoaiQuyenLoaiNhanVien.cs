namespace BanVeDiTourDuLich
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongTinLoaiQuyenLoaiNhanVien")]
    public partial class ThongTinLoaiQuyenLoaiNhanVien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaThonTinLoaiQuyenLoaiNhanVien { get; set; }

        [Required]
        [StringLength(10)]
        public string MaLoaiQuyen { get; set; }

        [Required]
        [StringLength(20)]
        public string MaLoaiNhanVien { get; set; }

        public virtual LoaiNhanVien LoaiNhanVien { get; set; }

        public virtual LoaiQuyen LoaiQuyen { get; set; }
    }
}
