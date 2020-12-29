using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.ViewModels
{
    public class ChiTietTour2
    {
        public DiaDiem DiaDiemDen { get; set; }
        public DiaDiem DiaDiemDi { get; set; }
        public double GiaTien { get; set; }
        public DateTime ThoiGianDi { get; set; }
        public string MaTour { get; set; }
        public int SoGio { get; set; }
    }
}