using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BanVeDiTourDuLich.ViewModels;
using Stripe;

namespace BanVeDiTourDuLich.Controllers
{
    public class CartController : Controller
    {
        DataContext context = new DataContext();
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
                CartViewModel data = new CartViewModel()
                {
                    StripeKey = key,
                    ThongTinVeTrongGios = gioHang
                };
                return View("~/Views/Cart/Cart.cshtml" , data);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Charge(long amount, string name , string discription ,string stripeToken, string tokenType ,string stripeEmail)
        {
            Stripe.StripeConfiguration.SetApiKey(ConfigurationManager.AppSettings["stripePublishableKey"]);
            Stripe.StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["stripeSecretKey"];
    
            var myCharge = new Stripe.ChargeCreateOptions();
            // always set these properties
            myCharge.Amount = amount;
            myCharge.Currency = "VND";
            myCharge.ReceiptEmail = stripeEmail;
            myCharge.Description = discription;
            myCharge.Source = stripeToken;
            myCharge.Capture = true;
            var chargeService = new Stripe.ChargeService();
            try
            {
                Charge stripeCharge = chargeService.Create(myCharge);
                var gioHang = (List<ThongTinVeTrongGio>)Session["GioHang"];
                KhachHang khachHang = context.KhachHangs.Find("MaTaiKhoan");
                //HoaDon hoaDon = new HoaDon(){MaHoaDon = "HoaDon" , MaKhachHang = khachHang.MaKhachHang , ThoiGianXuat = Date};
                //foreach (var thongTinVeTrongGio in gioHang)
                //{
                //    context.Ves.Add(new Ve{})
                //}
                gioHang.Clear();
                return View("ThanhCong");
            }
            catch (Exception e)
            {
                return View("ThatBai");
            }
        }
    }
}