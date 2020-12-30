using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.ViewModels
{
    public class ThongTinChiTietViewModel
    {
        public bool isCustomer { get; set; }

        public KhachHang KhachHang { get; set; }

        public NhanVien NhanVien { get; set; }  
    }
}