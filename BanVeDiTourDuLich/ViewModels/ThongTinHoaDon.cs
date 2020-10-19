using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.ViewModels
{
    public class ThongTinHoaDon
    {
        public HoaDon HoaDon  { get; set; }
        public List<Ve> CacVe { get; set; }
        public NhanVien NhanVien { get; set; }
        public KhachHang KhachHang { get; set; }
    }
}