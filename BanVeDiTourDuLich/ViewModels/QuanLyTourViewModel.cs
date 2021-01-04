using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.ViewModels
{
    public class QuanLyTourViewModel
    {
        public List<ThongTinTourExpanded> danhsachtour { get; set; }
        public int SoTrang { get; set; }
        public int STT { get; set; }
    }
}