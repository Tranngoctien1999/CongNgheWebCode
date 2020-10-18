using BanVeDiTourDuLich.ViewModels;
using System.Linq;
using System.Web.Mvc;

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
            //var list = _context.Ves.ToList();

            return View(quanLyVeViewModel);
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
            //data.ThongTinCacNguoiDung = query.ToList();
            return View(data);
        }
    }
}