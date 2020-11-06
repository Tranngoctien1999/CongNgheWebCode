using System;
using System.Collections.Generic;
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
            if (Session["MaTaiKhoan"] != null)
            {
                List<ThongTinHangTrongGio> gioHang = new List<ThongTinHangTrongGio>();
                if (Session["GioHang"] == null)
                {
                    Session["GioHang"] = new List<ThongTinHangTrongGio>();
                    gioHang = (List<ThongTinHangTrongGio>)Session["GioHang"];
                }
                else
                {
                    gioHang = (List<ThongTinHangTrongGio>)Session["GioHang"];
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