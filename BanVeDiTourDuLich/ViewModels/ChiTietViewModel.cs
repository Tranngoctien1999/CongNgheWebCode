using BanVeDiTourDuLich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.ViewModels
{
    public class ChiTietViewModel
    {
        public Tour Tour { get; set; }
        public List<LoaiVeSoLuongCon> CacLoaiVe { get; set; }
        public string chitiet { get; set; }
        public StripeKey StripeKey { get; set; }
        public ChiTietViewModel()
        {
            CacLoaiVe = new List<LoaiVeSoLuongCon>();
            LichTrinh = new List<LichTrinh>();
        }
        public List<LichTrinh> LichTrinh { get; set; }

    }
}