using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanVeDiTourDuLich.Controllers
{
    public class QuanLyBanVeController : Controller
    {
        private DataContext _context;

        public QuanLyBanVeController()
        {
            _context = new DataContext();
        }

        [HttpPost]
        public ActionResult Xoa(string id)
        {
            if (ModelState.IsValid)
            {
                var ve = _context.Ves.Find(id);
                if (ve != null)
                {
                    _context.Ves.Remove(ve);
                    _context.SaveChanges();
                    return RedirectToAction("QuanLyBanVe", "Admin");
                }
            }
            return HttpNotFound();
        }
    }
}