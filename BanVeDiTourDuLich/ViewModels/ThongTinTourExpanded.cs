using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.ViewModels
{
    public class ThongTinTourExpanded
    {
        public Tour Tour { get; set; }
        public DiaDiem DiaDiemDi { get; set; }
        public DiaDiem DiaDiemDen { get; set; }
        public string DuongDanAnh { get; set; }
        public string TenDiaDiem { get; set; }
        public double GiaTien { get; set; }
        public string Ten { get; set; }
    }
}