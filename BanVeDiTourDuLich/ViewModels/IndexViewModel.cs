﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanVeDiTourDuLich.Models;

namespace BanVeDiTourDuLich.ViewModels
{
    public class IndexViewModel
    {
        public List<TourGiaTien> CacTour { get; set; }
        public List<ExpandedDiaDiemViewModel> CacDiaDiemBinhChon { get; set; }
        public List<NhanXetExpandedViewModel> CacNhanXet { get; set; }
        public int SoTrang { get; set; }
        public int STT { get; set; }
    }
}