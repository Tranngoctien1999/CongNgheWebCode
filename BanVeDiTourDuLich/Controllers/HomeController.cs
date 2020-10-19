using System;
using BanVeDiTourDuLich.Models;
using BanVeDiTourDuLich.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace BanVeDiTourDuLich.Controllers
{
    public class HomeController : Controller
    {
        private DataContext context = new DataContext();

        public ActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            // Linq select top 4 poupular destionation base on number of tours
            var query = from diaDiem in context.DiaDiems
                join tour in context.Tours on diaDiem.MaDiaDiem
                    equals tour.MaDiemDen into g
                orderby g.Count() descending
                select new ExpandedDiaDiemViewModel() {DiaDiem = diaDiem, SoChuyen = g.Count()};
            indexViewModel.CacDiaDiemBinhChon = query.Take(4).ToList();


            // Select 6 newest tour 
            var query1 = from diaDiem in context.DiaDiems
                join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                join loaiVe in context.LoaiVes on tour.MaTour equals loaiVe.MaTour
                    into g
                select new DiaDiemGiaTien() {DiaDiem = diaDiem, GiaTien = (double?) g.Min(p => p.GiaTien) ?? 0};
            if (query1.Count() >= 6)
            {
                indexViewModel.CacDiaDiem = query1.Take(6).ToList();
            }
            else
            {
                indexViewModel.CacDiaDiem = query1.ToList();
            }


            // Select 6 newest comments
            var query2 = (from nhanXet in context.NhanXets
                join KhachHang in context.KhachHangs
                    on nhanXet.MaKhachHang equals KhachHang.MaKhachHang
                    into g
                select new
                {
                    MaKhachHang = nhanXet.MaKhachHang,
                    MaTour = nhanXet.MaTour,
                    NoiDung = nhanXet.NoiDung,
                    Ten = g.FirstOrDefault().Ten,
                    DuongDanAnh = g.FirstOrDefault().DuongDanAnh
                }).ToList().Select(nhanXetExpand => new NhanXetExpandedViewModel()
            {
                NhanXet = new NhanXet()
                {
                    MaKhachHang = nhanXetExpand.MaKhachHang,
                    MaTour = nhanXetExpand.MaTour,
                    NoiDung = nhanXetExpand.NoiDung
                },
                DuongDanAnh = nhanXetExpand.DuongDanAnh,
                TenKhachHang = nhanXetExpand.Ten
            });
            indexViewModel.CacNhanXet = query2.ToList();
            return View(indexViewModel);
        }

        public ActionResult Search(string diemden)
        {
            SearchViewModel searchViewModel = new SearchViewModel();
            var query1 = from diaDiem in context.DiaDiems
                join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                where diaDiem.TenDiaDiem.Contains(diemden)
                select new DiaDiemGiaTien() {DiaDiem = diaDiem, GiaTien = 0};
            searchViewModel.CacDiaDiem = query1.ToList();
            if (diemden != null)
            {
                searchViewModel.TuKhoaTimKiem = "\"" + diemden + "\"";
            }
            return View(searchViewModel);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Destination()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            // Linq select top 4 poupular destionation base on number of tours
            var query = from diaDiem in context.DiaDiems
                        join tour in context.Tours on diaDiem.MaDiaDiem
                            equals tour.MaDiemDen into g
                        orderby g.Count() descending
                        select new ExpandedDiaDiemViewModel() { DiaDiem = diaDiem, SoChuyen = g.Count() };
            indexViewModel.CacDiaDiemBinhChon = query.Take(4).ToList();
            // Select 6 newest tour 
            var query1 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaiVe in context.LoaiVes on tour.MaTour equals loaiVe.MaTour
                             into g
                         select new DiaDiemGiaTien() { DiaDiem = diaDiem, GiaTien = (double?)g.Min(p => p.GiaTien) ?? 0 };
            if (query1.Count() >= 6)
            {
                indexViewModel.CacDiaDiem = query1.Take(6).ToList();
            }
            else
            {
                indexViewModel.CacDiaDiem = query1.ToList();
            }


            // Select 6 newest comments
            var query2 = (from nhanXet in context.NhanXets
                          join KhachHang in context.KhachHangs
                              on nhanXet.MaKhachHang equals KhachHang.MaKhachHang
                              into g
                          select new
                          {
                              MaKhachHang = nhanXet.MaKhachHang,
                              MaTour = nhanXet.MaTour,
                              NoiDung = nhanXet.NoiDung,
                              Ten = g.FirstOrDefault().Ten,
                              DuongDanAnh = g.FirstOrDefault().DuongDanAnh
                          }).ToList().Select(nhanXetExpand => new NhanXetExpandedViewModel()
                          {
                              NhanXet = new NhanXet()
                              {
                                  MaKhachHang = nhanXetExpand.MaKhachHang,
                                  MaTour = nhanXetExpand.MaTour,
                                  NoiDung = nhanXetExpand.NoiDung
                              },
                              DuongDanAnh = nhanXetExpand.DuongDanAnh,
                              TenKhachHang = nhanXetExpand.Ten
                          });
            indexViewModel.CacNhanXet = query2.ToList();

            return View(indexViewModel);
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult ChiTietChuyenDi()
        {
            return View();
        }
    }
}