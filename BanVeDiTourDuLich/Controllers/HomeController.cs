using BanVeDiTourDuLich.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace BanVeDiTourDuLich.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            DataContext context = new DataContext();
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
                         select new DiaDiemGiaTien() { DiaDiem = diaDiem , GiaTien = (double?)g.Min(p => p.GiaTien) ?? 0};
            if (query1.Count() >= 6)
            {
                indexViewModel.CacDiaDiem = query1.Take(6).ToList();
            }
            else
            {
                indexViewModel.CacDiaDiem = query1.ToList();
            }
            return View(indexViewModel);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Destination()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}