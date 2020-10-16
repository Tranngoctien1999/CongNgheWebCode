using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.ViewModels
{
    public class ThongTinVeExpanded
    {
        public Ve Ve { get; set; }
        public DiaDiem DiaDiemDi { get; set; }
        public DiaDiem DiaDiemDen { get; set; }
        public double GiaTien { get; set; }
    }
}