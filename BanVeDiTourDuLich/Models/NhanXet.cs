using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.Models
{
    public class NhanXet
    {
        [Key]
        public int MaNhanXet { get; set; }

        [StringLength(20)]
        public string MaKhachHang { get; set; }

        [StringLength(20)] 
        public string MaTour { get; set; }

        public string NoiDung { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual Tour Tour { get; set; }
    }
}