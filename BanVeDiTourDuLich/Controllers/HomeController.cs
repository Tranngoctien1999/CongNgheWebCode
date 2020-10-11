using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Mvc;

namespace BanVeDiTourDuLich.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<DiaDiem> diaDiemList = new List<DiaDiem>();
            DataContext context = new DataContext();
            diaDiemList = context.DiaDiems.ToList();
            return View(diaDiemList);
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