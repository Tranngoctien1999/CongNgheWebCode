using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.ViewModels
{
    public class SearchViewModel
    {
        public List<ChiTietTour2> CacTour { get; set; }
        public string TuKhoaTimKiem { get; set; }
    }
}