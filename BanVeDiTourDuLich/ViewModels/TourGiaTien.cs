using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.ViewModels
{
    public class TourGiaTien
    {
        public Tour Tour { get; set; }
        public double GiaTien { get; set; }
        public DiaDiem DiaDiem { get; set; }
    }
}