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
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour into g
                         where diaDiem.TenDiaDiem.Contains(diemden)

                         select new ChiTietTour2() { DiaDiem = diaDiem, DuongDanAnh = diaDiem.DuongDanAnh, ThoiGianDi = tour.ThoigianDi, MaTour = tour.MaTour, GiaTien = (double?)g.Min(p => p.GiaTien) ?? 0 };
            var query2 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour into g
                         where tour.ThoigianDi.ToString().Contains(ngaydi)
                         select new ChiTietTour2() { DiaDiem = diaDiem, DuongDanAnh = diaDiem.DuongDanAnh, ThoiGianDi = tour.ThoigianDi, MaTour = tour.MaTour, GiaTien = (double?)g.Min(p => p.GiaTien) ?? 0 };
            var query3 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour into g
                         where g.Max(p => p.GiaTien) <= gia
                         select new ChiTietTour2() { DiaDiem = diaDiem, DuongDanAnh = diaDiem.DuongDanAnh, ThoiGianDi = tour.ThoigianDi, MaTour = tour.MaTour, GiaTien = (double?)g.Min(p => p.GiaTien) ?? 0 };
            var query4 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour into g
                         where diaDiem.TenDiaDiem.Contains(diemden) && tour.ThoigianDi.ToString().Contains(ngaydi)
                         select new ChiTietTour2() { DiaDiem = diaDiem, DuongDanAnh = diaDiem.DuongDanAnh, ThoiGianDi = tour.ThoigianDi, MaTour = tour.MaTour, GiaTien = (double?)g.Min(p => p.GiaTien) ?? 0 };
            var query5 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour into g
                         where diaDiem.TenDiaDiem.Contains(diemden) && g.Max(p => p.GiaTien) <= gia
                         select new ChiTietTour2() { DiaDiem = diaDiem, DuongDanAnh = diaDiem.DuongDanAnh, ThoiGianDi = tour.ThoigianDi, MaTour = tour.MaTour, GiaTien = (double?)g.Min(p => p.GiaTien) ?? 0 };

            var query6 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour into g
                         where tour.ThoigianDi.ToString().Contains(ngaydi) && g.Max(p => p.GiaTien) <= gia
                         select new ChiTietTour2() { DiaDiem = diaDiem, DuongDanAnh = diaDiem.DuongDanAnh, ThoiGianDi = tour.ThoigianDi, MaTour = tour.MaTour, GiaTien = (double?)g.Min(p => p.GiaTien) ?? 0 };
            var query7 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour into g
                         where tour.ThoigianDi.ToString().Contains(ngaydi) && g.Max(p => p.GiaTien) <= gia && diaDiem.TenDiaDiem.Contains(diemden)
                         select new ChiTietTour2() { DiaDiem = diaDiem, DuongDanAnh = diaDiem.DuongDanAnh, ThoiGianDi = tour.ThoigianDi, MaTour = tour.MaTour, GiaTien = (double?)g.Min(p => p.GiaTien) ?? 0 };
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
            var query1 = from tour in context.Tours
                         join ChiTietTour2 in context.ChiTietTours on tour.MaTour equals ChiTietTour2.MaTour
                         where tour.MaTour.Contains(id)
                         select ChiTietTour2.ChiTiet;
            chiTietViewModel.chitiet = query1.SingleOrDefault();
            var query2 = from lichtrinh in context.LichTrinhs
                         where lichtrinh.MaTour.Contains(id)
                         select lichtrinh;
            chiTietViewModel.LichTrinh = query2.OrderBy(x => x.Ngay).ToList();
            return View(chiTietViewModel);
        }
        public ActionResult ChitietTourDiemDen(string id)
        {
            SearchViewModel indexView = new SearchViewModel();

            var query1 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour into g
                         where diaDiem.MaDiaDiem.Contains(id)
                         select new ViewModels.ChiTietTour2() { DiaDiem = diaDiem, DuongDanAnh = diaDiem.DuongDanAnh, ThoiGianDi = tour.ThoigianDi, TenDiaDiem = diaDiem.TenDiaDiem, MaTour = tour.MaTour, GiaTien = (double?)g.Min(p => p.GiaTien) ?? 0 };
            indexView.CacTour = query1.ToList();
            var query2 = from tour in context.Tours
                         join diaDiem in context.DiaDiems on tour.MaDiemDi equals diaDiem.MaDiaDiem


                         where tour.MaDiemDen.Contains(id)
                         select diaDiem.TenDiaDiem;

            indexView.DiemKhoiHanh = query2.ToList();
            return View(indexView);
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