using BanVeDiTourDuLich.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanVeDiTourDuLich.Controllers
{
    public class TourController : Controller
    {
        private DataContext context = new DataContext();
        // GET: Tour
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TimKiemTour(string diemden, string ngaydi = null, double gia = 0)
        {
            SearchViewModel searchViewModel = new SearchViewModel();

            var query1 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour
                         where diaDiem.TenDiaDiem.Contains(diemden)

                         select new ChiTietTour() { DiaDiem = diaDiem,DuongDanAnh=diaDiem.DuongDanAnh,ThoiGianDi=tour.ThoigianDi,MaLoaiVe=loaive.MaLoaiVe, GiaTien = loaive.GiaTien };
            var query2 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour
                         where tour.ThoigianDi.ToString().Contains(ngaydi)
                         select new ChiTietTour() { DiaDiem = diaDiem, DuongDanAnh = diaDiem.DuongDanAnh, ThoiGianDi = tour.ThoigianDi, MaLoaiVe = loaive.MaLoaiVe, GiaTien = loaive.GiaTien };
            var query3 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour
                         where loaive.GiaTien <= gia
                         select new ChiTietTour() { DiaDiem = diaDiem, DuongDanAnh = diaDiem.DuongDanAnh, ThoiGianDi = tour.ThoigianDi, MaLoaiVe = loaive.MaLoaiVe, GiaTien = loaive.GiaTien };
            var query4 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour
                         where diaDiem.TenDiaDiem.Contains(diemden) && tour.ThoigianDi.ToString().Contains(ngaydi)
                         select new ChiTietTour() { DiaDiem = diaDiem, DuongDanAnh = diaDiem.DuongDanAnh, ThoiGianDi = tour.ThoigianDi, MaLoaiVe = loaive.MaLoaiVe, GiaTien = loaive.GiaTien };
            var query5 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour
                         where diaDiem.TenDiaDiem.Contains(diemden) && loaive.GiaTien <= gia
                         select new ChiTietTour() { DiaDiem = diaDiem, DuongDanAnh = diaDiem.DuongDanAnh, ThoiGianDi = tour.ThoigianDi, MaLoaiVe = loaive.MaLoaiVe, GiaTien = loaive.GiaTien };

            var query6 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour
                         where tour.ThoigianDi.ToString().Contains(ngaydi) && loaive.GiaTien <= gia
                         select new ChiTietTour() { DiaDiem = diaDiem, DuongDanAnh = diaDiem.DuongDanAnh, ThoiGianDi = tour.ThoigianDi, MaLoaiVe = loaive.MaLoaiVe, GiaTien = loaive.GiaTien };
            var query7 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour
                         where tour.ThoigianDi.ToString().Contains(ngaydi) && loaive.GiaTien <= gia && diaDiem.TenDiaDiem.Contains(diemden)
                         select new ChiTietTour() { DiaDiem = diaDiem, DuongDanAnh = diaDiem.DuongDanAnh, ThoiGianDi = tour.ThoigianDi, MaLoaiVe = loaive.MaLoaiVe, GiaTien = loaive.GiaTien };
            if (diemden != null && ngaydi == "" && gia == 0)
            {
                searchViewModel.CacTour = query1.ToList();
            }
            else if (diemden == "" && ngaydi != null && gia == 0)
            {
                searchViewModel.CacTour = query2.ToList();
            }
            else if (diemden == "" && ngaydi == "" && gia != 0)
            {
                searchViewModel.CacTour = query3.ToList();
            }
            else if (diemden != null && ngaydi != null && gia == 0)
            {
                searchViewModel.CacTour = query4.ToList();
            }
            else if (diemden != null && ngaydi == "" && gia != 0)
            {
                searchViewModel.CacTour = query5.ToList();
            }
            else if (diemden == "" && ngaydi != null && gia != 0)
            {
                searchViewModel.CacTour = query6.ToList();
            }
            else
            {
                searchViewModel.CacTour = query7.ToList();
            }
            return View(searchViewModel);

        }
        public ActionResult ChiTietChuyenDi(String id)
        {
            ChiTietViewModel chiTietViewModel = new ChiTietViewModel();
            var chuyenDi = context.Tours.Where(tour => tour.MaTour == id).Include(tour => tour.LoaiVes)
                .Include(tour => tour.DiaDiemDen).FirstOrDefault();
            chiTietViewModel.CacLoaiVe = chuyenDi.LoaiVes.ToList();
            chiTietViewModel.Tour = chuyenDi;
            return View(chiTietViewModel);
        }

        public static string AddDotAndCommaToInteger(double amount)
        {
            string parts = amount.ToString("N0", new NumberFormatInfo()
            {
                NumberGroupSizes = new[] { 3 },
                NumberGroupSeparator = "."
            });
            return parts;
        }

    }
}