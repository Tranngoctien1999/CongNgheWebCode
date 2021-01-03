using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.ViewModels
{
    public class NguoiDungViewModel
    {
        public string TenNguoiDung { get; set; }
        public double SoTienMua { get; set; }
        public int SoVeMua { get; set; }
        public DateTime NgayTaoTaiKhoan { get; set; }
        public string MaNguoiDung { get; set; }
    }
}