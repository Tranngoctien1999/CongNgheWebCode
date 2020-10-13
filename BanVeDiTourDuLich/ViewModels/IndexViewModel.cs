using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanVeDiTourDuLich.Models;

namespace BanVeDiTourDuLich.ViewModels
{
    public class IndexViewModel
    {
        public List<DiaDiemGiaTien> CacDiaDiem { get; set; }
        public List<ExpandedDiaDiemViewModel> CacDiaDiemBinhChon { get; set; }
        public List<NhanXetExpandedViewModel> CacNhanXet { get; set; }
    }
}