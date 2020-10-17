using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanVeDiTourDuLich.ViewModels;

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
            return View();
        }

        public ActionResult QuanLyBanVe()
        {
            QuanLyVeViewModel quanLyVeViewModel = new QuanLyVeViewModel();
            quanLyVeViewModel.DanhSachThongTinVe = _context.Ves.Join(_context.Tours , ve => ve.MaTour , tour => tour.MaTour,
                (ve, tour) =>
                new {
                    Ve = ve,
                    DiaDiemDen = tour.DiaDiemDen,
                    DiaDiemDi = tour.DiaDiemDi
                }).Join(_context.LoaiVes , c => c.Ve.MaLoaiVe , loaiVe => loaiVe.MaLoaiVe ,
                (c , loaiVe) => new ThongTinVeExpanded()
                {
                    Ve = c.Ve,
                    GiaTien = loaiVe.GiaTien,
                    DiaDiemDen = c.DiaDiemDen,
                    DiaDiemDi = c.DiaDiemDi
                }).ToList();
            //var list = _context.Ves.ToList();

            return View(quanLyVeViewModel);
        }

        public ActionResult QuanLyNguoiDung()
        {
            _context.KhachHangs.GroupJoin((_context.HoaDons, khachHang => khachHang.MaKhachHang,
                hoaDon => hoaDon.MaKhachHang
                , (hang, don) => new
                {
                    TenKhachHang = hang. ,
                    SoTien = 
                });
                

            return View();
        }
    }
}