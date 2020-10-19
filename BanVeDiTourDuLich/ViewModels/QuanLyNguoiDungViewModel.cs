using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.ViewModels
{
    public class QuanLyNguoiDungViewModel
    {
        public QuanLyNguoiDungViewModel()
        {
            ThongTinCacNguoiDung = new List<NguoiDungViewModel>();
        }
        public List<NguoiDungViewModel> ThongTinCacNguoiDung { get; set; }
    }
}