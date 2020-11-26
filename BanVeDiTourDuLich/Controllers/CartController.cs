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
        public ActionResult Charge(FormCollection formCollection)
        {
            Stripe.StripeConfiguration.SetApiKey(ConfigurationManager.AppSettings["stripePublishableKey"]);
            Stripe.StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["stripeSecretKey"];
            var myCharge = new Stripe.ChargeCreateOptions();
            // always set these properties
            myCharge.Amount = long.Parse(formCollection[0]);
            myCharge.Currency = "VND";
            var listVe = context.Ves.ToList();
            string maVeCuoi = listVe[listVe.Count - 1].MaVe;
            int identity = int.Parse(maVeCuoi.Substring(2));
            for (int i = 0; i < formCollection.Count - 3; i+= 2)
            {
                Ve ve = new Ve()
                {
                    MaVe = "Ve" + identity.ToString("000"),
                    MaTour = 
                };
            }
            myCharge.ReceiptEmail = formCollection[formCollection.Count - 1];
            myCharge.Source = formCollection[formCollection.Count - 3];
            myCharge.Capture = true;
            var chargeService = new Stripe.ChargeService();
            try
            {
                Charge stripeCharge = chargeService.Create(myCharge);
                return View("ThanhCong");
            }
            catch (Exception e)
            {
                return View("ThatBai");
            }
        }
    }
}