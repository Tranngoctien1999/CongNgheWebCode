using System;
using System.Collections.Generic;
using System.Diagnostics;
using BanVeDiTourDuLich.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using BanVeDiTourDuLich.Utilizer;
using Microsoft.Ajax.Utilities;

namespace BanVeDiTourDuLich.Controllers
{
    public class AdminController : BaseController
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
                return View("QuanLyBanVe", quanLyVeViewModel);
            }
            return HttpNotFound("Hãy Đăng Nhập");
        }

        public ActionResult QuanLyBanVeSort(string sortValue, int? sortDirection)
        {
            if (CheckUser())
            {
                QuanLyVeViewModel quanLyVeViewModel = new QuanLyVeViewModel();
                quanLyVeViewModel.DanhSachThongTinVe = _context.Ves.Join(_context.Tours, ve => ve.MaTour,
                    tour => tour.MaTour,
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

                if (sortValue.IsNullOrWhiteSpace() || (sortDirection != 0 && sortDirection != 1))
                {
                    return View("QuanLyBanVe", quanLyVeViewModel);
                }

                if (string.Compare("MaVe", sortValue) == 0)
                {
                    if (sortDirection == 0)
                    {
                        quanLyVeViewModel.DanhSachThongTinVe =
                            quanLyVeViewModel.DanhSachThongTinVe.OrderBy(thongTinVe => thongTinVe.Ve.MaVe).ToList();
                    }
                    else
                    {
                        quanLyVeViewModel.DanhSachThongTinVe = quanLyVeViewModel.DanhSachThongTinVe
                            .OrderByDescending(thongTinVe => thongTinVe.Ve.MaVe).ToList();
                    }
                }


                if (string.Compare("NguoiMua", sortValue) == 0)
                {
                    if (sortDirection == 0)
                    {
                        quanLyVeViewModel.DanhSachThongTinVe =
                            quanLyVeViewModel.DanhSachThongTinVe
                                .OrderBy(thongTinVe => thongTinVe.Ve.HoaDon.KhachHang.Ten).ToList();
                    }
                    else
                    {
                        quanLyVeViewModel.DanhSachThongTinVe = quanLyVeViewModel.DanhSachThongTinVe
                            .OrderByDescending(thongTinVe => thongTinVe.Ve.HoaDon.KhachHang.Ten).ToList();
                    }
                }

                if (string.Compare("NgayThanhToan", sortValue) == 0)
                {
                    if (sortDirection == 0)
                    {
                        quanLyVeViewModel.DanhSachThongTinVe =
                            quanLyVeViewModel.DanhSachThongTinVe
                                .OrderBy(thongTinVe => thongTinVe.Ve.HoaDon.ThoiGianXuat).ToList();
                    }
                    else
                    {
                        quanLyVeViewModel.DanhSachThongTinVe = quanLyVeViewModel.DanhSachThongTinVe
                            .OrderByDescending(thongTinVe => thongTinVe.Ve.HoaDon.ThoiGianXuat).ToList();
                    }
                }

                if (string.Compare("SoTien", sortValue) == 0)
                {
                    if (sortDirection == 0)
                    {
                        quanLyVeViewModel.DanhSachThongTinVe =
                            quanLyVeViewModel.DanhSachThongTinVe
                                .OrderBy(thongTinVe => thongTinVe.Ve.LoaiVe.GiaTien).ToList();
                    }
                    else
                    {
                        quanLyVeViewModel.DanhSachThongTinVe = quanLyVeViewModel.DanhSachThongTinVe
                            .OrderByDescending(thongTinVe => thongTinVe.Ve.LoaiVe.GiaTien).ToList();
                    }
                }

                if (string.Compare("DiemDen", sortValue) == 0)
                {
                    if (sortDirection == 0)
                    {
                        quanLyVeViewModel.DanhSachThongTinVe = quanLyVeViewModel.DanhSachThongTinVe
                            .OrderBy(thongTinVe => thongTinVe.Ve.Tour.DiaDiemDi.TenDiaDiem).ToList();
                    }
                    else
                    {
                        quanLyVeViewModel.DanhSachThongTinVe = quanLyVeViewModel.DanhSachThongTinVe
                            .OrderByDescending(thongTinVe => thongTinVe.Ve.Tour.DiaDiemDi.TenDiaDiem).ToList();
                    }
                }

                if (string.Compare("DiemDi", sortValue) == 0)
                {
                    if (sortDirection == 0)
                    {
                        quanLyVeViewModel.DanhSachThongTinVe = quanLyVeViewModel.DanhSachThongTinVe
                            .OrderBy(thongTinVe => thongTinVe.Ve.Tour.DiaDiemDen.TenDiaDiem).ToList();
                    }
                    else
                    {
                        quanLyVeViewModel.DanhSachThongTinVe = quanLyVeViewModel.DanhSachThongTinVe
                            .OrderByDescending(thongTinVe => thongTinVe.Ve.Tour.DiaDiemDen.TenDiaDiem).ToList();
                    }
                }
                return View("QuanLyBanVe", quanLyVeViewModel);
            }
            return HttpNotFound();
        }


        public ActionResult QuanLyBanVeSingle(string id)
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
                return View("QuanLyBanVe" , quanLyVeViewModel);
            }
            return HttpNotFound("Hãy Đăng Nhập");
        }

        public ActionResult QuanLyNguoiDungSort(string sortValue, int? sortDirection)
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
                    SoVeMua = soVe,
                    NgayTaoTaiKhoan = khach.ThoiGianDangKi,
                    MaNguoiDung = khach.MaKhachHang
                });
            };

            if (sortValue.IsNullOrWhiteSpace() || (sortDirection != 0 && sortDirection != 1))
            {
                return View("QuanLyNguoiDung", data);
            }

            if (string.Compare("Ten", sortValue) == 0)
            {
                if (sortDirection == 0)
                {
                    data.ThongTinCacNguoiDung = data.ThongTinCacNguoiDung
                        .OrderBy(nguoiDung => nguoiDung.TenNguoiDung).ToList();
                }
                else
                {
                    data.ThongTinCacNguoiDung = data.ThongTinCacNguoiDung
                        .OrderByDescending(nguoiDung => nguoiDung.TenNguoiDung).ToList();
                }
            }

            if (string.Compare("SoTien", sortValue) == 0)
            {
                if (sortDirection == 0)
                {
                    data.ThongTinCacNguoiDung = data.ThongTinCacNguoiDung
                        .OrderBy(nguoiDung => nguoiDung.SoTienMua).ToList();
                }
                else
                {
                    data.ThongTinCacNguoiDung = data.ThongTinCacNguoiDung
                        .OrderByDescending(nguoiDung => nguoiDung.SoTienMua).ToList();
                }
            }

            if (string.Compare("SoVe", sortValue) == 0)
            {
                if (sortDirection == 0)
                {
                    data.ThongTinCacNguoiDung = data.ThongTinCacNguoiDung
                        .OrderBy(nguoiDung => nguoiDung.SoVeMua).ToList();
                }
                else
                {
                    data.ThongTinCacNguoiDung = data.ThongTinCacNguoiDung
                        .OrderByDescending(nguoiDung => nguoiDung.SoVeMua).ToList();
                }
            }

            if (string.Compare("SoVe", sortValue) == 0)
            {
                if (sortDirection == 0)
                {
                    data.ThongTinCacNguoiDung = data.ThongTinCacNguoiDung
                        .OrderBy(nguoiDung => nguoiDung.NgayTaoTaiKhoan).ToList();
                }
                else
                {
                    data.ThongTinCacNguoiDung = data.ThongTinCacNguoiDung
                        .OrderByDescending(nguoiDung => nguoiDung.NgayTaoTaiKhoan).ToList();
                }
            }

            return View( "QuanLyNguoiDung" , data);
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
                    SoVeMua = soVe,
                    NgayTaoTaiKhoan = khach.ThoiGianDangKi,
                    MaNguoiDung = khach.MaKhachHang
                });
            };
            return View(data);
        }

        public ActionResult QuanLyNguoiDungSingle(string id)
        {
            if (CheckUser())
            {
                QuanLyNguoiDungViewModel data = new QuanLyNguoiDungViewModel();
                foreach (KhachHang khach in _context.KhachHangs.Where(khachHang => khachHang.MaKhachHang == id).ToList())
                {
                    double tongTien = khach.HoaDons.Sum(hoaDon => hoaDon.Ves.Sum(ve => ve.LoaiVe.GiaTien));
                    int soVe = khach.HoaDons.Sum(hoaDon => hoaDon.Ves.Count);
                    data.ThongTinCacNguoiDung.Add(new NguoiDungViewModel
                    {
                        TenNguoiDung = khach.Ten,
                        SoTienMua = tongTien,
                        SoVeMua = soVe,
                        NgayTaoTaiKhoan = khach.ThoiGianDangKi,
                        MaNguoiDung = khach.MaKhachHang
                    });
                };
                return View("QuanLyNguoiDung" ,data);
            }
            else
            {
                return Content("Bạn không có quyền vào trang này!");
            }
        }

        public ActionResult QuanLyTourSort(string sortValue, int? sortDirection)
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
                    DiaDiemDi = tour.DiaDiemDi,
                    DiaDiemDen = tour.DiaDiemDen
                };
            quanLyTourViewModel.danhsachtour = query1.ToList();

            if (sortValue.IsNullOrWhiteSpace() || (sortDirection != 0 && sortDirection != 1))
            {
                return View("QuanLyTour", quanLyTourViewModel);
            }


            if (string.Compare("MaTour", sortValue) == 0)
            {
                if (sortDirection == 0)
                {
                    quanLyTourViewModel.danhsachtour =
                        quanLyTourViewModel.danhsachtour.OrderBy(thongTinTour => thongTinTour.Tour.MaTour).ToList();
                }
                else
                {
                    quanLyTourViewModel.danhsachtour =
                        quanLyTourViewModel.danhsachtour.OrderByDescending(thongTinTour => thongTinTour.Tour.MaTour).ToList();
                }
            }

            if (string.Compare("DiemDen", sortValue) == 0)
            {
                if (sortDirection == 0)
                {
                    quanLyTourViewModel.danhsachtour =
                        quanLyTourViewModel.danhsachtour.OrderBy(thongTinTour => thongTinTour.DiaDiemDen.TenDiaDiem).ToList();
                }
                else
                {
                    quanLyTourViewModel.danhsachtour =
                        quanLyTourViewModel.danhsachtour.OrderByDescending(thongTinTour => thongTinTour.DiaDiemDen.TenDiaDiem).ToList();
                }
            }

            if (string.Compare("DiemDi", sortValue) == 0)
            {
                if (sortDirection == 0)
                {
                    quanLyTourViewModel.danhsachtour =
                        quanLyTourViewModel.danhsachtour.OrderBy(thongTinTour => thongTinTour.DiaDiemDi.TenDiaDiem).ToList();
                }
                else
                {
                    quanLyTourViewModel.danhsachtour =
                        quanLyTourViewModel.danhsachtour.OrderByDescending(thongTinTour => thongTinTour.DiaDiemDi.TenDiaDiem).ToList();
                }
            }

            if (string.Compare("SoTien", sortValue) == 0)
            {
                if (sortDirection == 0)
                {
                    quanLyTourViewModel.danhsachtour =
                        quanLyTourViewModel.danhsachtour.OrderBy(thongTinTour => thongTinTour.GiaTien).ToList();
                }
                else
                {
                    quanLyTourViewModel.danhsachtour =
                        quanLyTourViewModel.danhsachtour.OrderByDescending(thongTinTour => thongTinTour.GiaTien).ToList();
                }
            }

            if (string.Compare("ThoiGian", sortValue) == 0)
            {
                if (sortDirection == 0)
                {
                    quanLyTourViewModel.danhsachtour =
                        quanLyTourViewModel.danhsachtour.OrderBy(thongTinTour => thongTinTour.Tour.ThoigianDi).ToList();
                }
                else
                {
                    quanLyTourViewModel.danhsachtour =
                        quanLyTourViewModel.danhsachtour.OrderByDescending(thongTinTour => thongTinTour.Tour.ThoigianDi).ToList();
                }
            }
            return View("QuanLyTour", quanLyTourViewModel);
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

        public ActionResult QuanLyTourSingle(string id)
        {
            if (CheckUser())
            {
                QuanLyTourViewModel quanLyTourViewModel = new QuanLyTourViewModel();

                var query1 = from diaDiem in _context.DiaDiems
                    join tour in _context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                    join loaive in _context.LoaiVes on tour.MaTour equals loaive.MaTour
                    where tour.MaTour == id
                    select new ThongTinTourExpanded
                    {
                        Tour = tour,
                        DuongDanAnh = diaDiem.DuongDanAnh,
                        TenDiaDiem = diaDiem.TenDiaDiem,
                        GiaTien = loaive.GiaTien,
                        Ten = loaive.Ten,
                        DiaDiemDen = tour.DiaDiemDen,
                        DiaDiemDi = tour.DiaDiemDi
                    };
                quanLyTourViewModel.danhsachtour = query1.ToList();

                return View("QuanLyTour" ,quanLyTourViewModel);
            }
            else
            {
                return Content("Bạn không có quyền vào trang này!");
            }
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
        public ActionResult QuanLyDiaDiemSort(string sortValue, int? sortDirection)
        {
            var model = _context.DiaDiems.Where(x => x.MaDiaDiem != null).ToList();

            if (sortValue.IsNullOrWhiteSpace() || (sortDirection != 0 && sortDirection != 1))
            {
                return View("QuanLyDiaDiem", model);
            }

            if (string.Compare("Ma", sortValue) == 0)
            {
                if (sortDirection == 0)
                {
                    model = model.OrderBy(diaDiem => diaDiem.MaDiaDiem).ToList();
                }
                else
                {
                    model = model.OrderByDescending(diaDiem => diaDiem.MaDiaDiem).ToList();
                }
            }

            if (string.Compare("Ten", sortValue) == 0)
            {
                if (sortDirection == 0)
                {
                    model = model.OrderBy(diaDiem => diaDiem.TenDiaDiem).ToList();
                }
                else
                {
                    model = model.OrderByDescending(diaDiem => diaDiem.TenDiaDiem).ToList();
                }
            }

            if (string.Compare("DiaChi", sortValue) == 0)
            {
                if (sortDirection == 0)
                {
                    model = model.OrderBy(diaDiem => diaDiem.DiaChi).ToList();
                }
                else
                {
                    model = model.OrderByDescending(diaDiem => diaDiem.DiaChi).ToList();
                }
            }

            return View( "QuanLyDiaDiem", model);
        }

        public ActionResult QuanLyDiaDiemSingle(string id)
        {
            if (CheckUser())
            {
                var model = _context.DiaDiems.Where(x => x.MaDiaDiem == id).ToList();
                return View("QuanLyDiaDiem" ,model);
            }
            else
            {
                return Content("Bạn không có quyền vào trang này!");
            }
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

        public bool CheckAdmin()
        {
            var userId = Session["MaTaiKhoan"];
            if (userId != null)
            {
                NhanVien nhanVien = _context.NhanViens.Find(userId);
                if (nhanVien == null)
                {
                    return false;
                }
                else
                {
                    if (nhanVien.MaNhanVien != "ADMIN")
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public ActionResult QuanLyNhanVien()
        {
            if (CheckAdmin())
            {
                var data = _context.NhanViens.ToList();
                return View(data);
            }
            else
            {
                return Content("Bạn không có quyền truy cập trang này");
            }
        }

        public ActionResult QuanLyNhanVienSort(string sortValue, int? sortDirection)
        {
            if (CheckAdmin())
            {
                var data = _context.NhanViens.ToList();

                if (sortValue.IsNullOrWhiteSpace() || (sortDirection != 0 && sortDirection != 1))
                {
                    return View("QuanLyNhanVien", data);
                }

                if (string.Compare("Ten", sortValue) == 0)
                {
                    if (sortDirection == 0)
                    {
                        data = data.OrderBy(nhanVien => nhanVien.Ten).ToList();
                    }
                    else
                    {
                        data = data.OrderByDescending(nhanVien => nhanVien.Ten).ToList();
                    }
                }

                if (string.Compare("Luong", sortValue) == 0)
                {
                    if (sortDirection == 0)
                    {
                        data = data.OrderBy(nhanVien => nhanVien.Luong).ToList();
                    }
                    else
                    {
                        data = data.OrderByDescending(nhanVien => nhanVien.Luong).ToList();
                    }
                }

                if (string.Compare("NgayVaoLam", sortValue) == 0)
                {
                    if (sortDirection == 0)
                    {
                        data = data.OrderBy(nhanVien => nhanVien.NgayVaoLam).ToList();
                    }
                    else
                    {
                        data = data.OrderByDescending(nhanVien => nhanVien.NgayVaoLam).ToList();
                    }
                }

                return View("QuanLyNhanVien" ,data);
            }
            else
            {
                return Content("Bạn không có quyền truy cập trang này");
            }
        }

        public ActionResult QuanLyNhanVienSingle(string id)
        {
            if (CheckAdmin())
            {
                var data = _context.NhanViens.Where(nhanVien => nhanVien.MaNhanVien == id).ToList();
                return View("QuanLyNhanVien" , data);
            }
            else
            {
                return Content("Bạn không có quyền truy cập trang này");
            }
        }

        [HttpPost]
        public async Task<ActionResult> TimKiemTongHop(string tuKhoa)
        {
            if (CheckUser())
            {
               List<SearchAdminInformation> data = await Utilizer.TimKiemTongHop.Search(tuKhoa);
               Response.StatusCode = 200;
               return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.StatusCode = 400;
                return Json("Bạn không có quyền làm việc này", JsonRequestBehavior.AllowGet);
            }
        }
    }
}