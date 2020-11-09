using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.ViewModels
{
    public class ChiTietViewModel
    {
        public Tour Tour { get; set; }
        public List<LoaiVe> CacLoaiVe { get; set; }
    }
}