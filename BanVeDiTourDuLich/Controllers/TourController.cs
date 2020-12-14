using BanVeDiTourDuLich.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
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
        public ActionResult TimKiemTour(string diemden, string ngaydi = null, double gia = -1)
        {
            DateTime? ngayDiDateTime = null;
            if (!string.IsNullOrEmpty(ngaydi))
            {
                ngayDiDateTime = DateTime.ParseExact(ngaydi, "MM/dd/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture);
            }
            SearchViewModel searchViewModel = new SearchViewModel();

            searchViewModel.CacTour = new List<ChiTietTour2>();
            if (ngayDiDateTime.HasValue && gia != -1)
            {
                searchViewModel.CacTour = context.Tours.Where(tour => tour.DiaDiemDen.TenDiaDiem.ToLower().Contains(diemden.ToLower())).Select(tour => new ChiTietTour2()
                {
                    MaTour = tour.MaTour,
                    GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                    DiaDiemDen = tour.DiaDiemDen,
                    DiaDiemDi = tour.DiaDiemDi,
                    ThoiGianDi = tour.ThoigianDi,
                    SoGio = tour.SoGio
                }).Where(thongTinTour => thongTinTour.GiaTien <= gia && thongTinTour.ThoiGianDi == ngayDiDateTime.Value).ToList();
            }
            else
            {
                if(ngayDiDateTime.HasValue && gia == -1)
                searchViewModel.CacTour = context.Tours.Where(tour => tour.DiaDiemDen.TenDiaDiem.ToLower().Contains(diemden.ToLower())).Select(tour => new ChiTietTour2()
                {
                    MaTour = tour.MaTour,
                    GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                    DiaDiemDen = tour.DiaDiemDen,
                    DiaDiemDi = tour.DiaDiemDi,
                    ThoiGianDi = tour.ThoigianDi,
                    SoGio = tour.SoGio
                }).Where(thongTinTour => thongTinTour.ThoiGianDi == ngayDiDateTime.Value).ToList();
                else
                {
                    if (!ngayDiDateTime.HasValue && gia != -1)
                    {
                        searchViewModel.CacTour = context.Tours.Where(tour => tour.DiaDiemDen.TenDiaDiem.ToLower().Contains(diemden.ToLower())).Select(tour => new ChiTietTour2()
                        {
                            MaTour = tour.MaTour,
                            GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                            DiaDiemDen = tour.DiaDiemDen,
                            DiaDiemDi = tour.DiaDiemDi,
                            ThoiGianDi = tour.ThoigianDi,
                            SoGio = tour.SoGio
                        }).Where(thongTinTour => thongTinTour.GiaTien <= gia).ToList();
                    }
                    else
                    {
                        searchViewModel.CacTour = context.Tours
                            .Where(tour => tour.DiaDiemDen.TenDiaDiem.ToLower().Contains(diemden.ToLower())).Select(
                                tour => new ChiTietTour2()
                                {
                                    MaTour = tour.MaTour,
                                    GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                                    DiaDiemDen = tour.DiaDiemDen,
                                    DiaDiemDi = tour.DiaDiemDi,
                                    ThoiGianDi = tour.ThoigianDi,
                                    SoGio = tour.SoGio
                                }).ToList();
                    }
                }
            }
            return View(searchViewModel);

        }
        public ActionResult ChiTietChuyenDi(String id)
        {
            StripeKey key = new StripeKey()
            {
                PublishableKey = ConfigurationManager.AppSettings["stripePublishableKey"],
                SecretKey = ConfigurationManager.AppSettings["stripeSecretKey"]
            };
            ChiTietViewModel chiTietViewModel = new ChiTietViewModel();
            chiTietViewModel.StripeKey = key;
            var chuyenDi = context.Tours.Where(tour => tour.MaTour == id).Include(tour => tour.LoaiVes)
                .Include(tour => tour.DiaDiemDen).SingleOrDefault();
            List<LoaiVe> loaiVes = chuyenDi.LoaiVes.ToList();
            foreach (LoaiVe loaiVe in loaiVes)
            {
                LoaiVeSoLuongCon data = new LoaiVeSoLuongCon()
                {
                    LoaiVe = loaiVe,
                    SoLuong = loaiVe.SoLuong - loaiVe.Ves.Where(ve => ve.MaHoaDon != string.Empty).Count()
                };
                chiTietViewModel.CacLoaiVe.Add(data);
            }
            
            chiTietViewModel.Tour = chuyenDi;
            var query1 = from tour in context.Tours
                         join ChiTietTour2 in context.ChiTietTours on tour.MaTour equals ChiTietTour2.MaTour
                         where tour.MaTour.Contains(id)
                         select ChiTietTour2.ChiTiet;
            chiTietViewModel.chitiet = query1.FirstOrDefault();
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
                         join diaDiemDi in context.DiaDiems on tour.MaDiemDi equals diaDiemDi.MaDiaDiem
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour into g
                         where diaDiem.MaDiaDiem.Contains(id)
                         select new ViewModels.ChiTietTour2() { DiaDiemDen = diaDiem, DiaDiemDi = diaDiemDi , ThoiGianDi = tour.ThoigianDi, MaTour = tour.MaTour, GiaTien = (double?)g.Min(p => p.GiaTien) ?? 0 };
            indexView.CacTour = query1.ToList();
            var query2 = from tour in context.Tours
                         join diaDiem in context.DiaDiems on tour.MaDiemDi equals diaDiem.MaDiaDiem
                         where tour.MaDiemDen.Contains(id)
                         select diaDiem.TenDiaDiem;
            //indexView.DiemKhoiHanh = query2.ToList();
            return View(indexView);
        }
        [HttpPost]
        public async Task<ActionResult> FindTourWithStartingAndDestinationPoint(string maDiemDi , string maDiemDen)
        {
            if (!string.IsNullOrEmpty(maDiemDi) && !string.IsNullOrEmpty(maDiemDen))
            {
                var list = await context.Tours.Where(tour => tour.MaDiemDen == maDiemDen && tour.MaDiemDi == maDiemDi && tour.ThoigianDi > DateTime.Now).Select(tour => new {tour.MaTour , tour.ThoigianDi})
                    .ToListAsync();
                return Json( new { list  }, JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound();
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