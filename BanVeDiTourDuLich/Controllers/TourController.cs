using BanVeDiTourDuLich.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BanVeDiTourDuLich.Controllers
{
    public class TourController : BaseController
    {
        private readonly DataContext context = new DataContext();

        // GET: Tour
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TimKiemTour(string diemden, string ngaydi = null, string ngaygioihan = null,
            double gia = -1)
        {
            DateTime? ngayDiDateTime = null;
            DateTime? ngayGioiHanDateTime = null;

            var checkGiaVe = 0;
            var checkDiemDen = 0;
            var checkNgayGioiHan = 0;
            var checkNgayDi = 0;

            if (!string.IsNullOrEmpty(ngaydi))
            {
                try
                {
                    ngayDiDateTime = DateTime.ParseExact(ngaydi, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    checkNgayDi = 1;
                }
                catch (Exception e)
                {
                    try
                    {
                        ngayDiDateTime = DateTime.ParseExact(ngaydi, "MM/d/yyyy", CultureInfo.InvariantCulture);
                        checkNgayDi = 1;
                    }
                    catch (Exception exception)
                    {
                        return Content("Không hợp lệ");
                    }
                }
            }

            if (!string.IsNullOrEmpty(ngaygioihan))
            {
                try
                {
                    ngayGioiHanDateTime = DateTime.ParseExact(ngaygioihan, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    checkNgayGioiHan = 1;
                }
                catch (Exception e)
                {
                    try
                    {
                        ngayGioiHanDateTime = DateTime.ParseExact(ngaygioihan, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        checkNgayGioiHan = 1;
                    }
                    catch (Exception exception)
                    {
                        return Content("Không hợp lệ");
                    }
                }
            }

            if (!string.IsNullOrEmpty(diemden)) checkDiemDen = 1;

            if (gia != -1) checkGiaVe = 1;

            var input = 8 * checkNgayDi + 4 * checkNgayGioiHan + 2 * checkDiemDen + checkGiaVe;

            var searchViewModel = new SearchViewModel();
            searchViewModel.CacTour = new List<ChiTietTour2>();

            if (input == (int) TimKiemTourThongTin.DiemDen)
            {
                searchViewModel.TuKhoaTimKiem = string.Format("Danh sách các tour có điểm đến là {0}", diemden);
                searchViewModel.CacTour = context.Tours
                    .Where(tour => tour.DiaDiemDen.TenDiaDiem.ToLower().Contains(diemden.ToLower()))
                    .Select(tour => new ChiTietTour2
                    {
                        MaTour = tour.MaTour,
                        GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                        DiaDiemDen = tour.DiaDiemDen,
                        DiaDiemDi = tour.DiaDiemDi,
                        ThoiGianDi = tour.ThoigianDi,
                        SoGio = tour.SoGio
                    })
                    .ToList();
            }

            if (input == (int) TimKiemTourThongTin.DiemDenGiaVe)
            {
                searchViewModel.TuKhoaTimKiem = string.Format("Danh sách các tour có điểm đến là {0} giá vé từ {1}",
                    diemden,
                    AddDotAndCommaToInteger(gia));
                searchViewModel.CacTour = context.Tours
                    .Where(tour => tour.DiaDiemDen.TenDiaDiem.ToLower().Contains(diemden.ToLower()))
                    .Select(tour => new ChiTietTour2
                    {
                        MaTour = tour.MaTour,
                        GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                        DiaDiemDen = tour.DiaDiemDen,
                        DiaDiemDi = tour.DiaDiemDi,
                        ThoiGianDi = tour.ThoigianDi,
                        SoGio = tour.SoGio
                    })
                    .Where(thongTinTour => thongTinTour.GiaTien <= gia)
                    .ToList();
            }

            if (input == (int) TimKiemTourThongTin.GiaVe)
            {
                searchViewModel.TuKhoaTimKiem = string.Format("Danh sách các tour có giá vé từ {1}", diemden,
                    AddDotAndCommaToInteger(gia));
                searchViewModel.CacTour = context.Tours.Select(tour => new ChiTietTour2
                    {
                        MaTour = tour.MaTour,
                        GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                        DiaDiemDen = tour.DiaDiemDen,
                        DiaDiemDi = tour.DiaDiemDi,
                        ThoiGianDi = tour.ThoigianDi,
                        SoGio = tour.SoGio
                    })
                    .Where(thongTinTour => thongTinTour.GiaTien <= gia)
                    .ToList();
            }

            if (input == (int) TimKiemTourThongTin.NgayDi)
            {
                searchViewModel.TuKhoaTimKiem = string.Format("Danh sách các tour từ ngày {0} tháng {1} năm {2}", ngayDiDateTime.Value.Day, ngayDiDateTime.Value.Month.ToString(),
                    ngayDiDateTime.Value.Year.ToString());
                searchViewModel.CacTour = context.Tours.Select(tour => new ChiTietTour2
                    {
                        MaTour = tour.MaTour,
                        GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                        DiaDiemDen = tour.DiaDiemDen,
                        DiaDiemDi = tour.DiaDiemDi,
                        ThoiGianDi = tour.ThoigianDi,
                        SoGio = tour.SoGio
                    })
                    .Where(thongTinTour => thongTinTour.ThoiGianDi >= ngayDiDateTime.Value)
                    .ToList();
            }

            if (input == (int) TimKiemTourThongTin.NgayDiDiemDen)
            {
                searchViewModel.TuKhoaTimKiem = string.Format(
                    "Danh sách các tour có điểm đến là {0} từ ngày {1} tháng {2} năm {3}", diemden,
                    ngayDiDateTime.Value.Day, ngayDiDateTime.Value.Month.ToString(),
                    ngayDiDateTime.Value.Year.ToString());
                searchViewModel.CacTour = context.Tours
                    .Where(tour => tour.DiaDiemDen.TenDiaDiem.ToLower().Contains(diemden.ToLower()))
                    .Select(tour => new ChiTietTour2
                    {
                        MaTour = tour.MaTour,
                        GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                        DiaDiemDen = tour.DiaDiemDen,
                        DiaDiemDi = tour.DiaDiemDi,
                        ThoiGianDi = tour.ThoigianDi,
                        SoGio = tour.SoGio
                    })
                    .Where(thongTinTour => thongTinTour.ThoiGianDi >= ngayDiDateTime.Value)
                    .ToList();
            }

            if (input == (int) TimKiemTourThongTin.NgayDiDiemDenGiaVe)
            {
                searchViewModel.TuKhoaTimKiem = string.Format(
                    "Danh sách các tour có điểm đến là {0} từ ngày {1} tháng {2} năm {3} giá vé từ {4}", diemden,
                    ngayDiDateTime.Value.Day, ngayDiDateTime.Value.Month.ToString(),
                    ngayDiDateTime.Value.Year.ToString(),
                    AddDotAndCommaToInteger(gia));
                searchViewModel.CacTour = context.Tours
                    .Where(tour => tour.DiaDiemDen.TenDiaDiem.ToLower().Contains(diemden.ToLower()))
                    .Select(tour => new ChiTietTour2
                    {
                        MaTour = tour.MaTour,
                        GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                        DiaDiemDen = tour.DiaDiemDen,
                        DiaDiemDi = tour.DiaDiemDi,
                        ThoiGianDi = tour.ThoigianDi,
                        SoGio = tour.SoGio
                    })
                    .Where(thongTinTour =>
                        thongTinTour.ThoiGianDi >= ngayDiDateTime.Value && thongTinTour.GiaTien <= gia)
                    .ToList();
            }

            if (input == (int) TimKiemTourThongTin.NgayDiGiaVe)
            {
                searchViewModel.TuKhoaTimKiem = string.Format(
                    "Danh sách các tour từ ngày {0} tháng {1} năm {2} giá vé từ {3}", ngayDiDateTime.Value.Day,
                    ngayDiDateTime.Value.Month.ToString(), ngayDiDateTime.Value.Year.ToString(),
                    AddDotAndCommaToInteger(gia));
                searchViewModel.CacTour = context.Tours.Select(
                        tour => new ChiTietTour2
                        {
                            MaTour = tour.MaTour,
                            GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                            DiaDiemDen = tour.DiaDiemDen,
                            DiaDiemDi = tour.DiaDiemDi,
                            ThoiGianDi = tour.ThoigianDi,
                            SoGio = tour.SoGio
                        })
                    .Where(thongTinTour =>
                        thongTinTour.ThoiGianDi >= ngayDiDateTime.Value && thongTinTour.GiaTien <= gia)
                    .ToList();
            }

            if (input == (int) TimKiemTourThongTin.NgayDiNgayGioiHan)
            {
                searchViewModel.TuKhoaTimKiem = string.Format(
                    "Danh sách các tour từ ngày {0} tháng {1} năm {2} đến ngày {3}" + " tháng {4} năm {5}",
                    ngayDiDateTime.Value.Day, ngayDiDateTime.Value.Month.ToString(),
                    ngayDiDateTime.Value.Year.ToString(), ngayGioiHanDateTime.Value.Day,
                    ngayGioiHanDateTime.Value.Month.ToString(), ngayGioiHanDateTime.Value.Year.ToString());
                searchViewModel.CacTour = context.Tours.Select(
                        tour => new ChiTietTour2
                        {
                            MaTour = tour.MaTour,
                            GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                            DiaDiemDen = tour.DiaDiemDen,
                            DiaDiemDi = tour.DiaDiemDi,
                            ThoiGianDi = tour.ThoigianDi,
                            SoGio = tour.SoGio
                        })
                    .Where(thongTinTour => thongTinTour.ThoiGianDi >= ngayDiDateTime.Value &&
                                           thongTinTour.ThoiGianDi <= ngayGioiHanDateTime.Value)
                    .ToList();
            }

            if (input == (int) TimKiemTourThongTin.NgayDiNgayGioiHanDiemDen)
            {
                searchViewModel.TuKhoaTimKiem = string.Format(
                    "Danh sách các tour có điểm đến là {0} từ ngày {1} tháng {2} năm {3} đến ngày {4}" +
                    " tháng {5} năm {6}", diemden, ngayDiDateTime.Value.Day, ngayDiDateTime.Value.Month.ToString(),
                    ngayDiDateTime.Value.Year.ToString(), ngayGioiHanDateTime.Value.Day,
                    ngayGioiHanDateTime.Value.Month.ToString(), ngayGioiHanDateTime.Value.Year.ToString());
                searchViewModel.CacTour = context.Tours
                    .Where(tour => tour.DiaDiemDen.TenDiaDiem.ToLower().Contains(diemden.ToLower()))
                    .Select(tour => new ChiTietTour2
                    {
                        MaTour = tour.MaTour,
                        GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                        DiaDiemDen = tour.DiaDiemDen,
                        DiaDiemDi = tour.DiaDiemDi,
                        ThoiGianDi = tour.ThoigianDi,
                        SoGio = tour.SoGio
                    })
                    .Where(thongTinTour => thongTinTour.ThoiGianDi >= ngayDiDateTime.Value &&
                                           thongTinTour.ThoiGianDi <= ngayDiDateTime.Value)
                    .ToList();
            }

            if (input == (int) TimKiemTourThongTin.NgayDiNgayGioiHanDiemDenGiaVe)
            {
                searchViewModel.TuKhoaTimKiem = string.Format(
                    "Danh sách các tour có điểm đến là {0} từ ngày {1} tháng {2} năm {3} đến ngày {4}" +
                    " tháng {5} năm {6} , giá vé từ {7}", diemden, ngayDiDateTime.Value.Day,
                    ngayDiDateTime.Value.Month.ToString(), ngayDiDateTime.Value.Year.ToString(),
                    ngayGioiHanDateTime.Value.Day, ngayGioiHanDateTime.Value.Month.ToString(),
                    ngayGioiHanDateTime.Value.Year.ToString(), AddDotAndCommaToInteger(gia));
                searchViewModel.CacTour = context.Tours
                    .Where(tour => tour.DiaDiemDen.TenDiaDiem.ToLower().Contains(diemden.ToLower()))
                    .Select(tour => new ChiTietTour2
                    {
                        MaTour = tour.MaTour,
                        GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                        DiaDiemDen = tour.DiaDiemDen,
                        DiaDiemDi = tour.DiaDiemDi,
                        ThoiGianDi = tour.ThoigianDi,
                        SoGio = tour.SoGio
                    })
                    .Where(thongTinTour => thongTinTour.ThoiGianDi >= ngayDiDateTime.Value &&
                                           thongTinTour.ThoiGianDi <= ngayDiDateTime.Value &&
                                           thongTinTour.GiaTien <= gia)
                    .ToList();
            }

            if (input == (int) TimKiemTourThongTin.NgayGioiHan)
            {
                searchViewModel.TuKhoaTimKiem = string.Format(
                    "Danh sách các tour đến ngày {0}" + " tháng {1} năm {2} , giá vé từ {3}", ngayGioiHanDateTime.Value.Day,
                    ngayGioiHanDateTime.Value.Month.ToString(), ngayGioiHanDateTime.Value.Year.ToString(),
                    AddDotAndCommaToInteger(gia));
                searchViewModel.CacTour = context.Tours.Select(tour => new ChiTietTour2
                    {
                        MaTour = tour.MaTour,
                        GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                        DiaDiemDen = tour.DiaDiemDen,
                        DiaDiemDi = tour.DiaDiemDi,
                        ThoiGianDi = tour.ThoigianDi,
                        SoGio = tour.SoGio
                    })
                    .Where(thongTinTour => thongTinTour.ThoiGianDi <= ngayGioiHanDateTime)
                    .ToList();
            }

            if (input == (int) TimKiemTourThongTin.NgayGioiHanGiaVe)
            {
                searchViewModel.TuKhoaTimKiem = string.Format(
                    "Danh sách các tour đến ngày {0}" + " tháng {1} năm {2} , giá vé từ {3}", ngayGioiHanDateTime.Value.Day,
                    ngayGioiHanDateTime.Value.Month.ToString(), ngayGioiHanDateTime.Value.Year.ToString(),
                    AddDotAndCommaToInteger(gia));
                searchViewModel.CacTour = context.Tours.Select(
                        tour => new ChiTietTour2
                        {
                            MaTour = tour.MaTour,
                            GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                            DiaDiemDen = tour.DiaDiemDen,
                            DiaDiemDi = tour.DiaDiemDi,
                            ThoiGianDi = tour.ThoigianDi,
                            SoGio = tour.SoGio
                        })
                    .Where(thongTinTour =>
                        thongTinTour.ThoiGianDi <= ngayGioiHanDateTime && thongTinTour.GiaTien <= gia)
                    .ToList();
            }

            if (input == (int) TimKiemTourThongTin.NgayGioiHanDiemDen)
            {
                searchViewModel.TuKhoaTimKiem = string.Format(
                    "Danh sách các tour có điểm đến là {0} đến ngày {1}" + " tháng {2} năm {3}", diemden, ngayGioiHanDateTime.Value.Day,
                    ngayGioiHanDateTime.Value.Month.ToString(), ngayGioiHanDateTime.Value.Year.ToString());
                searchViewModel.CacTour = context.Tours
                    .Where(tour => tour.DiaDiemDen.TenDiaDiem.ToLower().Contains(diemden.ToLower()))
                    .Select(tour => new ChiTietTour2
                    {
                        MaTour = tour.MaTour,
                        GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                        DiaDiemDen = tour.DiaDiemDen,
                        DiaDiemDi = tour.DiaDiemDi,
                        ThoiGianDi = tour.ThoigianDi,
                        SoGio = tour.SoGio
                    })
                    .Where(thongTinTour => thongTinTour.ThoiGianDi <= ngayGioiHanDateTime)
                    .ToList();
            }

            if (input == (int) TimKiemTourThongTin.NgayGioiHanDiemDenGiaVe)
            {
                searchViewModel.TuKhoaTimKiem = string.Format(
                    "Danh sách các tour có điểm đến là {0} đến ngày {1}" + " tháng {2} năm {3} , giá vé từ {4}",
                    diemden, ngayGioiHanDateTime.Value.Day,
                    ngayGioiHanDateTime.Value.Month.ToString(), ngayGioiHanDateTime.Value.Year.ToString(),
                    AddDotAndCommaToInteger(gia));
                searchViewModel.CacTour = context.Tours
                    .Where(tour => tour.DiaDiemDen.TenDiaDiem.ToLower().Contains(diemden.ToLower()))
                    .Select(tour => new ChiTietTour2
                    {
                        MaTour = tour.MaTour,
                        GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                        DiaDiemDen = tour.DiaDiemDen,
                        DiaDiemDi = tour.DiaDiemDi,
                        ThoiGianDi = tour.ThoigianDi,
                        SoGio = tour.SoGio
                    })
                    .Where(thongTinTour =>
                        thongTinTour.ThoiGianDi <= ngayGioiHanDateTime && thongTinTour.GiaTien <= gia)
                    .ToList();
            }

            if (input == (int) TimKiemTourThongTin.NgayGioiHanGiaVe)
            {
                searchViewModel.TuKhoaTimKiem = string.Format(
                    "Danh sách các tour đến ngày {0}" + " tháng {1} năm {2} , giá vé từ {3}", ngayGioiHanDateTime.Value.Day,
                    ngayGioiHanDateTime.Value.Month.ToString(), ngayGioiHanDateTime.Value.Year.ToString(),
                    AddDotAndCommaToInteger(gia));
                searchViewModel.CacTour = context.Tours.Select(
                        tour => new ChiTietTour2
                        {
                            MaTour = tour.MaTour,
                            GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                            DiaDiemDen = tour.DiaDiemDen,
                            DiaDiemDi = tour.DiaDiemDi,
                            ThoiGianDi = tour.ThoigianDi,
                            SoGio = tour.SoGio
                        })
                    .Where(thongTinTour =>
                        thongTinTour.ThoiGianDi <= ngayGioiHanDateTime && thongTinTour.GiaTien <= gia)
                    .ToList();
            }

            if (input == (int) TimKiemTourThongTin.None)
            {
                searchViewModel.TuKhoaTimKiem = string.Format("Danh sách các tour");
                searchViewModel.CacTour = context.Tours.Select(tour => new ChiTietTour2
                    {
                        MaTour = tour.MaTour,
                        GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                        DiaDiemDen = tour.DiaDiemDen,
                        DiaDiemDi = tour.DiaDiemDi,
                        ThoiGianDi = tour.ThoigianDi,
                        SoGio = tour.SoGio
                    })
                    .ToList();
            }

            if (input == (int) TimKiemTourThongTin.NgayDiNgayGioiHanGiaVe)
            {
                searchViewModel.TuKhoaTimKiem = string.Format(
                    "Danh sách các tour từ ngày {0} tháng {1} năm {2} đến ngày {3}" +
                    " tháng {4} năm {5} , giá vé từ {6}", ngayDiDateTime.Value.Day,
                    ngayDiDateTime.Value.Month.ToString(), ngayDiDateTime.Value.Year.ToString(),
                    ngayGioiHanDateTime.Value.Day, ngayGioiHanDateTime.Value.Month.ToString(),
                    ngayGioiHanDateTime.Value.Year.ToString(), AddDotAndCommaToInteger(gia));
                searchViewModel.CacTour = context.Tours
                    .Select(tour => new ChiTietTour2
                    {
                        MaTour = tour.MaTour,
                        GiaTien = tour.LoaiVes.Max(loaiVe => loaiVe.GiaTien),
                        DiaDiemDen = tour.DiaDiemDen,
                        DiaDiemDi = tour.DiaDiemDi,
                        ThoiGianDi = tour.ThoigianDi,
                        SoGio = tour.SoGio
                    })
                    .Where(thongTinTour => thongTinTour.ThoiGianDi >= ngayDiDateTime.Value &&
                                           thongTinTour.ThoiGianDi <= ngayGioiHanDateTime.Value &&
                                           thongTinTour.GiaTien <= gia)
                    .ToList();
            }

            return View(searchViewModel);
        }

        public ActionResult ChiTietChuyenDi(string id)
        {
            var key = new StripeKey
            {
                PublishableKey = ConfigurationManager.AppSettings["stripePublishableKey"],
                SecretKey = ConfigurationManager.AppSettings["stripeSecretKey"]
            };
            var chiTietViewModel = new ChiTietViewModel();
            chiTietViewModel.StripeKey = key;
            var chuyenDi = context.Tours.Where(tour => tour.MaTour == id)
                .Include(tour => tour.LoaiVes)
                .Include(tour => tour.DiaDiemDen)
                .SingleOrDefault();
            var loaiVes = chuyenDi.LoaiVes.ToList();
            foreach (var loaiVe in loaiVes)
            {
                var data = new LoaiVeSoLuongCon
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
            var query2 = from lichtrinh in context.LichTrinhs where lichtrinh.MaTour.Contains(id) select lichtrinh;
            chiTietViewModel.LichTrinh = query2.OrderBy(x => x.Ngay).ToList();
            return View(chiTietViewModel);
        }

        public ActionResult ChitietTourDiemDen(string id)
        {
            var indexView = new SearchViewModel();
            var query1 = from diaDiem in context.DiaDiems
                join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                join diaDiemDi in context.DiaDiems on tour.MaDiemDi equals diaDiemDi.MaDiaDiem
                join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour into g
                where diaDiem.MaDiaDiem.Contains(id)
                select new ChiTietTour2
                {
                    DiaDiemDen = diaDiem,
                    DiaDiemDi = diaDiemDi,
                    ThoiGianDi = tour.ThoigianDi,
                    MaTour = tour.MaTour,
                    GiaTien = (double?) g.Min(p => p.GiaTien) ?? 0
                };
            indexView.CacTour = query1.ToList();
            var query2 = from tour in context.Tours
                join diaDiem in context.DiaDiems on tour.MaDiemDi equals diaDiem.MaDiaDiem
                where tour.MaDiemDen.Contains(id)
                select diaDiem.TenDiaDiem;
            //indexView.DiemKhoiHanh = query2.ToList();
            return View(indexView);
        }

        [HttpPost]
        public async Task<ActionResult> FindTourWithStartingAndDestinationPoint(string maDiemDi, string maDiemDen)
        {
            if (!string.IsNullOrEmpty(maDiemDi) && !string.IsNullOrEmpty(maDiemDen))
            {
                var list = await context.Tours
                    .Where(tour =>
                        tour.MaDiemDen == maDiemDen && tour.MaDiemDi == maDiemDi && tour.ThoigianDi > DateTime.Now)
                    .Select(tour => new {tour.MaTour, tour.ThoigianDi})
                    .ToListAsync();
                return Json(new {list}, JsonRequestBehavior.AllowGet);
            }

            return HttpNotFound();
        }

        public static string AddDotAndCommaToInteger(double amount)
        {
            var parts = amount.ToString("N0",
                new NumberFormatInfo {NumberGroupSizes = new[] {3}, NumberGroupSeparator = "."});
            return parts;
        }

        private enum TimKiemTourThongTin
        {
            None,
            GiaVe,
            DiemDen,
            DiemDenGiaVe,
            NgayGioiHan,
            NgayGioiHanGiaVe,
            NgayGioiHanDiemDen,
            NgayGioiHanDiemDenGiaVe,
            NgayDi,
            NgayDiGiaVe,
            NgayDiDiemDen,
            NgayDiDiemDenGiaVe,
            NgayDiNgayGioiHan,
            NgayDiNgayGioiHanGiaVe,
            NgayDiNgayGioiHanDiemDen,
            NgayDiNgayGioiHanDiemDenGiaVe
        }
    }
}