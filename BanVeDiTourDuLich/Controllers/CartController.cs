using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanVeDiTourDuLich.ViewModels;

namespace BanVeDiTourDuLich.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            StripeKey key = new StripeKey(){
                PublishableKey = ConfigurationManager.AppSettings["stripePublishableKey"],
                SecretKey = ConfigurationManager.AppSettings["stripeSecretKey"]
            };
            if (Session["MaTaiKhoan"] != null)
            {
                List<ThongTinVeTrongGio> gioHang = new List<ThongTinVeTrongGio>();
                if (Session["GioHang"] == null)
                {
                    Session["GioHang"] = new List<ThongTinVeTrongGio>();
                    gioHang = (List<ThongTinVeTrongGio>)Session["GioHang"];
                }
                else
                {
                    gioHang = (List<ThongTinVeTrongGio>)Session["GioHang"];
                }
                return View("~/Views/Cart/Cart.cshtml" , gioHang);
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}