using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.ViewModels
{
    public class IndexViewModel
    {
        public List<DiaDiemGiaTien> CacDiaDiem { get; set; }
        public List<ExpandedDiaDiemViewModel> CacDiaDiemBinhChon { get; set; }
    }
}