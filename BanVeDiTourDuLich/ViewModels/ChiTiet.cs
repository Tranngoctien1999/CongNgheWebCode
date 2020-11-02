using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.ViewModels
{
    public class ChiTiet
    {
        public string DuongDanAnh { get; set; }
        public string TenDiaDiem { get; set; }
        public double GiaTien { get; set; }
        public Tour Tour { get; set; }
    }
}