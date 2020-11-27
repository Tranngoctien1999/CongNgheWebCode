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
            var listHoaDon = context.HoaDons.ToList();
            string maHoaDonCuoi = listHoaDon[listHoaDon.Count - 1].MaHoaDon;
            int identityHoaDon = int.Parse(maHoaDonCuoi.Substring(2));
            int identityKhachHang = 0;
            var listKhachHang = context.KhachHangs.ToList();
            for (int i = listKhachHang.Count - 1 ; i >= 0 ; i--)
            {
                if (int.TryParse(listKhachHang[i].MaKhachHang, out identityKhachHang))
                {
                    break;
                }
            }

            if (Session["MaTaiKhoan"] == null)
            {
                KhachHang khachHang = new KhachHang()
                {
                    ThoiGianDangKi = DateTime.Now,
                    MaKhachHang = (identityKhachHang + 1).ToString(),
                    NgaySinh = DateTime.Now,
                    Ten = formCollection[formCollection.Count - 1],
                    MaLoaiKhachHang = "KHACHHANGTHUONG"
                };

                context.KhachHangs.Add(khachHang);

                var hoaDon = new HoaDon()
                {
                    MaHoaDon = "HD" + (identityHoaDon + 1).ToString("00"),
                    MaKhachHang = khachHang.MaKhachHang,
                    MaNhanVien = "ADMIN",
                    ThoiGianXuat = DateTime.Now
                };
                context.HoaDons.Add(hoaDon);
                string idTour = formCollection[1];
                for (int i = 2; i < formCollection.Count - 3; i += 2)
                {
                    for (int j = 0; j < int.Parse(formCollection[i + 1]); j++)
                    {
                        Ve ve = new Ve()
                        {
                            MaVe = "Ve" + (identity + 1).ToString("00"),
                            MaTour = idTour,
                            MaLoaiVe = formCollection[i],
                            MaHoaDon = hoaDon.MaHoaDon
                        };
                        context.Ves.Add(ve);
                        identity++;
                    }
                }
            }
            else
            {
                KhachHang khachHang = context.KhachHangs.Find(Session["MaTaiKhoan"]);
                var hoaDon = new HoaDon()
                {
                    MaHoaDon = "HD" + (identityHoaDon + 1).ToString("00"),
                    MaKhachHang = khachHang.MaKhachHang,
                    MaNhanVien = "ADMIN",
                    ThoiGianXuat = DateTime.Now
                };
                context.HoaDons.Add(hoaDon);
                string idTour = formCollection[1];
                for (int i = 2; i < formCollection.Count - 3; i += 2)
                {
                    for (int j = 0; j < int.Parse(formCollection[i + 1]); j++)
                    {
                        Ve ve = new Ve()
                        {
                            MaVe = "Ve" + (identity + 1).ToString("00"),
                            MaTour = idTour,
                            MaLoaiVe = formCollection[i],
                            MaHoaDon = hoaDon.MaHoaDon
                        };
                        context.Ves.Add(ve);
                        identity++;
                    }
                }
            }
            
            myCharge.ReceiptEmail = formCollection[formCollection.Count - 1];
            myCharge.Source = formCollection[formCollection.Count - 3];
            myCharge.Capture = true;
            var chargeService = new Stripe.ChargeService();
            try
            {
                Charge stripeCharge = chargeService.Create(myCharge);
                context.SaveChanges();
                return View("ThanhCong");
            }
            catch (Exception e)
            {
                return View("ThatBai");
            }
        }
    }
}