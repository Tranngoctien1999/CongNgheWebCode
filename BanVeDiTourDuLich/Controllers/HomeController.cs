using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Mvc;

namespace BanVeDiTourDuLich.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<DiaDiem> diaDiemList = new List<DiaDiem>();
            diaDiemList.Add(new DiaDiem()
            {
                MaDiaDiem = "123" , DiaChi = "236 Hoang Quoc Viet" 
                , TenDiaDiem = "Hoc Vien Ki Thuat Quan Su"
            });
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