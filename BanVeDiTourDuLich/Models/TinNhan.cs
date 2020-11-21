using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.Models
{
    public class TinNhan
    {
        [Key]
        public int MaTinNhan { get; set; }

        [StringLength(50)]
        public string MaNhanVien { get; set; }

        [StringLength(50)]
        public string MaKhachHang { get; set; }

        [Required]
        public string NoiDung { get; set; }

        public DateTime ThoiGianGui { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}