using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.ViewModels
{
    public class SearchViewModel
    {
        public List<DiaDiemGiaTien> CacDiaDiem { get; set; }
        public string TuKhoaTimKiem { get; set; }
    }
}