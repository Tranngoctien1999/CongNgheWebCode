using System;
using System.Diagnostics;
using BanVeDiTourDuLich.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web;
using System.IO;

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
            foreach (KhachHang khach in _context.KhachHangs.ToList())
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
            QuanLyTourViewModel quanLyTourViewModel = new QuanLyTourViewModel();

            var query1 = from diaDiem in _context.DiaDiems
                         join tour in _context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in _context.LoaiVes on tour.MaTour equals loaive.MaTour
                         select new ThongTinTourExpanded
                         {
                             Tour = tour,
                             DuongDanAnh = diaDiem.DuongDanAnh,
                             GiaTien = loaive.GiaTien,
                             Ten = loaive.Ten,
                             DiaDiemDi=tour.DiaDiemDi,
                             DiaDiemDen=tour.DiaDiemDen
                         };
            quanLyTourViewModel.danhsachtour = query1.ToList();

            return View(quanLyTourViewModel);

        }
        public ActionResult ThemMoiTour()
        {
            var model = _context.DiaDiems.Where(x => x.TenDiaDiem != null).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult ThemMoiTour(Tour tour, LoaiVe loaiVe)
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
        [HttpPost]
        public ActionResult XoaTour(string id)
        {
            if (ModelState.IsValid)
            {
                var tour = _context.Tours.Find(id);
                string maloaive = (from lve in _context.LoaiVes where lve.MaTour.Contains(id) select lve.MaLoaiVe).FirstOrDefault();
                var loaive = _context.LoaiVes.Find(maloaive);
                if (tour != null)
                {
                    _context.LoaiVes.Remove(loaive);
                    _context.SaveChanges();
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
        public ActionResult ThemDiaDiemMoi(DiaDiem diaDiem, HttpPostedFileBase postfile)
        {
            try
            {
                if (postfile != null)
                {
                    var paths = Server.MapPath("~/content/images/Destinations/");
                    var path = "/Content/images/Destinations/";
                    postfile.SaveAs(paths + postfile.FileName);
                    diaDiem.DuongDanAnh = path + postfile.FileName;
                }

                _context.DiaDiems.Add(diaDiem);
                _context.SaveChanges();
                return RedirectToAction("ThemMoiTour");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult EditTour(string id)
        {
            ChiTietTourAdmin chiTietTourAdmin = new ChiTietTourAdmin();
            var chuyenDi = _context.Tours.Where(tour => tour.MaTour == id).Include(tour => tour.LoaiVes).Where(loaive => loaive.MaTour == id)
                .Include(tour => tour.DiaDiemDen).FirstOrDefault();
            chiTietTourAdmin.LoaiVe = chuyenDi.LoaiVes.FirstOrDefault();
            chiTietTourAdmin.Tour = chuyenDi;
            return View(chiTietTourAdmin);
        }
        [HttpPost]
        public ActionResult EditTour(Tour t,LoaiVe loaive)
        {
            var lv = _context.LoaiVes.Find(loaive.MaLoaiVe);
            lv.GiaTien = loaive.GiaTien;
            lv.SoLuong = loaive.SoLuong;
            _context.SaveChanges();
            var tour = _context.Tours.Find(t.MaTour);
            tour.MaDiemDen = t.MaDiemDen;
            tour.MaDiemDi = t.MaDiemDi;
            tour.SoGio = t.SoGio;
            tour.ThoigianDi = t.ThoigianDi;
            _context.SaveChanges();
            
            return RedirectToAction("QuanLyTour");
        }
        public ActionResult QuanLyDiaDiem()
        {
            var model = _context.DiaDiems.Where(x => x.MaDiaDiem != null).ToList();
            return View(model);
        }
        public ActionResult ThemMoiDiaDiem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoiDiaDiem(DiaDiem diaDiem, HttpPostedFileBase post)
        {
            try
            {
                if (post != null)
                {
                    var paths = Server.MapPath("~/content/images/Destinations/");
                    var path = "/Content/images/Destinations/";
                    post.SaveAs(paths + post.FileName);
                    diaDiem.DuongDanAnh = path + post.FileName;
                }

                _context.DiaDiems.Add(diaDiem);
                _context.SaveChanges();
                return RedirectToAction("QuanLyDiaDiem");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult XoaDiaDiem(string id)
        {
            if (ModelState.IsValid)
            {
                var diadiem = _context.DiaDiems.Find(id);

                if (diadiem!= null)
                {
                    _context.DiaDiems.Remove(diadiem);
                    _context.SaveChanges();

                    return RedirectToAction("QuanLyDiaDiem");
                }
            }
            return HttpNotFound();
        }
        public ActionResult EditDiaDiem(string id)
        {
            var model = _context.DiaDiems.Where(dd => dd.MaDiaDiem == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult EditDiaDiem(DiaDiem diaDiem, HttpPostedFileBase postfiles)
        {
            try
            {
                var dd = _context.DiaDiems.Find(diaDiem.MaDiaDiem);
                if (postfiles != null)
                {
                    var paths = Server.MapPath("~/content/images/Destinations/");
                    var path = "/Content/images/Destinations/";
                    postfiles.SaveAs(paths + postfiles.FileName);
                    diaDiem.DuongDanAnh = path + postfiles.FileName;
                    dd.DuongDanAnh = diaDiem.DuongDanAnh;
                }
                dd.MaDiaDiem = diaDiem.MaDiaDiem;
                dd.DiaChi = diaDiem.DiaChi;
                dd.TenDiaDiem = diaDiem.TenDiaDiem;
                _context.SaveChanges();
                return RedirectToAction("QuanLyDiaDiem");
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