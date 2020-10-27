using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanVeDiTourDuLich.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            if(Session["MaTaiKhoan"] != null)
                return View("~/Views/Cart/Cart.cshtml");
            else
            {
                return HttpNotFound();
            }
        }
    }
}