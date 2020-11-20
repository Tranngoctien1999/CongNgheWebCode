using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.ViewModels
{
    public class ChiTietTour
    {
        public DiaDiem DiaDiem { get; set; }
        public string DuongDanAnh { get; set; }
        public string TenDiaDiem { get; set; }
        public double GiaTien { get; set; }
        public DateTime ThoiGianDi { get; set; }
        public string MaTour { get; set; }
        public string DiemKhoiHanh { get; set; }
    }
}