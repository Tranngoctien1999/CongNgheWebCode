using System;
using System.Diagnostics;
using BanVeDiTourDuLich.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace BanVeDiTourDuLich.Controllers
{
    public class AdminController : Controller
    {
        private DataContext _context;
        public AdminController()
        {
            _context = new DataContext();
        }
        // GET: Admin
        public ActionResult Index()
        {
            if (CheckUser())
            {
                return View("Index");
            }
            return HttpNotFound("Hãy Đăng Nhập");
        }

        public ActionResult QuanLyBanVe(string id)
        {
            if (CheckUser())
            {
                if (id != null)
                {
                    ThongTinHoaDon thongTin = new ThongTinHoaDon();
                    thongTin.HoaDon = _context.Ves.Find(id).HoaDon;
                    thongTin.CacVe = thongTin.HoaDon.Ves.ToList();
                    thongTin.KhachHang = thongTin.HoaDon.KhachHang;
                    thongTin.NhanVien = thongTin.HoaDon.NhanVien;
                    return View("~/Views/Admin/ChiTietVe.cshtml", thongTin);
                }
                QuanLyVeViewModel quanLyVeViewModel = new QuanLyVeViewModel();
                quanLyVeViewModel.DanhSachThongTinVe = _context.Ves.Join(_context.Tours, ve => ve.MaTour, tour => tour.MaTour,
                    (ve, tour) =>
                        new
                        {
                            Ve = ve,
                            DiaDiemDen = tour.DiaDiemDen,
                            DiaDiemDi = tour.DiaDiemDi
                        }).Join(_context.LoaiVes, c => c.Ve.MaLoaiVe, loaiVe => loaiVe.MaLoaiVe,
                    (c, loaiVe) => new ThongTinVeExpanded()
                    {
                        Ve = c.Ve,
                        GiaTien = loaiVe.GiaTien,
                        DiaDiemDen = c.DiaDiemDen,
                        DiaDiemDi = c.DiaDiemDi
                    }).ToList();
                return View(quanLyVeViewModel);
            }
            return HttpNotFound("Hãy Đăng Nhập");
        }

        public ActionResult QuanLyNguoiDung()
        {
            QuanLyNguoiDungViewModel data = new QuanLyNguoiDungViewModel();
            foreach(KhachHang khach in _context.KhachHangs.ToList())
            {
                double tongTien = khach.HoaDons.Sum(hoaDon => hoaDon.Ves.Sum(ve => ve.LoaiVe.GiaTien));
                int soVe = khach.HoaDons.Sum(hoaDon => hoaDon.Ves.Count);
                data.ThongTinCacNguoiDung.Add(new NguoiDungViewModel
                {
                    TenNguoiDung = khach.Ten,
                    SoTienMua = tongTien,
                    SoVeMua = soVe
                });
            };
            return View(data);
        }
        public ActionResult QuanLyTour()
        {
            SearchViewModel indexView = new SearchViewModel();

            var query1 = from diaDiem in _context.DiaDiems
                         join tour in _context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in _context.LoaiVes on tour.MaTour equals loaive.MaTour into g
                         select new ViewModels.ChiTietTour2() { DiaDiem = diaDiem, DuongDanAnh = diaDiem.DuongDanAnh, ThoiGianDi = tour.ThoigianDi, TenDiaDiem = diaDiem.TenDiaDiem, MaTour = tour.MaTour, GiaTien = (double?)g.Min(p => p.GiaTien) ?? 0 };
            indexView.CacTour = query1.ToList();
            var query2 = from tour in _context.Tours
                         join diaDiem in _context.DiaDiems on tour.MaDiemDi equals diaDiem.MaDiaDiem
                         select diaDiem.TenDiaDiem;

            indexView.DiemKhoiHanh = query2.ToList();
            return View(indexView);
        }
        public ActionResult ThemMoiTour()
        {
            var model = _context.DiaDiems.Where(x => x.TenDiaDiem != null).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult ThemMoiTour(Tour tour,LoaiVe loaiVe)
        {
            try
            {
                _context.Tours.Add(tour);
                _context.SaveChanges();
                _context.LoaiVes.Add(loaiVe);
                _context.SaveChanges();
                return RedirectToAction("QuanLyTour");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult XoaTour(string id)
        {
            if (ModelState.IsValid)
            {
                var tour = _context.Tours.Find(id);
                if (tour != null)
                {
                    _context.Tours.Remove(tour);
                    _context.SaveChanges();


                    return RedirectToAction("QuanLyTour");
                }
            }
            return HttpNotFound();
        }
        public ActionResult ThemDiaDiemMoi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemDiaDiemMoi(DiaDiem diaDiem)
        {
            try
            {
                _context.DiaDiems.Add(diaDiem);
                _context.SaveChanges();
                return RedirectToAction("ThemMoiTour");
            }
            catch
            {
                return View();
            }
        }
        public bool CheckUser()
        {
            var userId = Session["MaTaiKhoan"];
            if (userId != null)
            {
                NhanVien nhanVien = _context.NhanViens.Find(userId);
                if (nhanVien == null)
                {
                    return false;
                }
            }
            return true;
        }
    }
}