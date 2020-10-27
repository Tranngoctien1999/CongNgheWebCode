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

        public ActionResult Search(string diemden,string ngaydi=null,double gia=0)
        {
            SearchViewModel searchViewModel = new SearchViewModel();
            
            var query1 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour
                         where diaDiem.TenDiaDiem.Contains(diemden)

                         select new DiaDiemGiaTien() { DiaDiem = diaDiem, GiaTien = loaive.GiaTien };
            var query2 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour
                         where tour.ThoigianDi.ToString().Contains(ngaydi)
                         select new DiaDiemGiaTien() { DiaDiem = diaDiem, GiaTien = loaive.GiaTien };
            var query3 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour
                         where loaive.GiaTien <= gia
                         select new DiaDiemGiaTien() { DiaDiem = diaDiem, GiaTien = loaive.GiaTien };
            var query4 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour
                         where diaDiem.TenDiaDiem.Contains(diemden) && tour.ThoigianDi.ToString().Contains(ngaydi)
                         select new DiaDiemGiaTien() { DiaDiem = diaDiem, GiaTien = loaive.GiaTien };
            var query5 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour
                         where diaDiem.TenDiaDiem.Contains(diemden) && loaive.GiaTien <= gia
                         select new DiaDiemGiaTien() { DiaDiem = diaDiem, GiaTien = loaive.GiaTien };

            var query6 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour
                         where tour.ThoigianDi.ToString().Contains(ngaydi) && loaive.GiaTien <= gia
                         select new DiaDiemGiaTien() { DiaDiem = diaDiem, GiaTien = loaive.GiaTien };
            var query7 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour
                         where tour.ThoigianDi.ToString().Contains(ngaydi) && loaive.GiaTien <= gia && diaDiem.TenDiaDiem.Contains(diemden)
                         select new DiaDiemGiaTien() { DiaDiem = diaDiem, GiaTien = loaive.GiaTien };
            if (diemden != null && ngaydi == "" && gia == 0)
            {
                searchViewModel.CacDiaDiem = query1.ToList();
            }
            else if (diemden == "" && ngaydi != null && gia == 0)
            {
                searchViewModel.CacDiaDiem = query2.ToList();
            }
            else if (diemden == "" && ngaydi == "" && gia != 0)
            {
                searchViewModel.CacDiaDiem = query3.ToList();
            }
            else if(diemden != null && ngaydi != null && gia == 0)
            {
                searchViewModel.CacDiaDiem = query4.ToList();
            }
            else if (diemden != null && ngaydi == "" && gia != 0)
            {
                searchViewModel.CacDiaDiem = query5.ToList();
            }
            else if (diemden == "" && ngaydi != null && gia != 0)
            {
                searchViewModel.CacDiaDiem = query6.ToList();
            }
            else
            {
                searchViewModel.CacDiaDiem = query7.ToList();
            }
            return View(searchViewModel);
        }

        public ActionResult About()
        {
            IndexViewModel indexView = new IndexViewModel();
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
            indexView.CacNhanXet = query2.ToList();
            return View(indexView);
        }

        public ActionResult Destination(string Id)
        {
            if (Id != null)
            {
                return View("~/Views/Home/ChiTietChuyenDi.cshtml");
            }
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
        public ActionResult Tour(string id)
        {
            SearchViewModel indexView = new SearchViewModel();
            var query1 = from diaDiem in context.DiaDiems
                         join tour in context.Tours on diaDiem.MaDiaDiem equals tour.MaDiemDen
                         join loaive in context.LoaiVes on tour.MaTour equals loaive.MaTour
                         where diaDiem.MaDiaDiem.Contains(id)
                         select new DiaDiemGiaTien() { DiaDiem = diaDiem, GiaTien = loaive.GiaTien };
            indexView.CacDiaDiem = query1.ToList();
            return View(indexView);
        }

        
        
    }
}