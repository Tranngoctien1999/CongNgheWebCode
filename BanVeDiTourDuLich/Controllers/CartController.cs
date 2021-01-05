﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BanVeDiTourDuLich.Hubs;
using BanVeDiTourDuLich.ViewModels;
using Microsoft.AspNet.SignalR;
using Stripe;

namespace BanVeDiTourDuLich.Controllers
{
    public class CartController : BaseController
    {
        public static DataContext context = new DataContext();
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
        public async Task<ActionResult> Charge(FormCollection formCollection)
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
            var listKhachHang = context.KhachHangs.ToList();
            if (Session["MaTaiKhoan"] == null)
            {
                if (Session["MaKhachHangVangLai"] != null)
                {
                    var hoaDon = new HoaDon()
                    {
                        MaHoaDon = "HD" + (identityHoaDon + 1).ToString("00"),
                        MaKhachHang = Session["MaKhachHangVangLai"].ToString(),
                        MaNhanVien = "ADMIN",
                        ThoiGianXuat = DateTime.Now
                    };
                    context.HoaDons.Add(hoaDon);
                    string idTour = formCollection[1];
                    for (int i = 2; i < formCollection.Count - 3; i += 2)
                    {
                        int quantity = 0;
                        if(Int32.TryParse(formCollection[i + 1] , out quantity))
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
                }
                else
                {
                    return Content("Bạn chưa điền thông tin! Hãy đăng nhập hoặc điền thông tin trước khi đặt");
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
                    int soLuongVe = 0;
                    try
                    {
                        soLuongVe = int.Parse(formCollection[i + 1]);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                    for (int j = 0; j < soLuongVe; j++)
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
                    await Chat.UpdateToClientBrower(idTour, formCollection[i], soLuongVe);
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
                await SumerizeRevenue();
                return View("ThanhCong");
            }
            catch (Exception e)
            {
                return View("ThatBai");
            }
        }

        

        public static async Task SumerizeRevenue()
        {
            var data = context.HoaDons.GroupBy(hoaDon => new {hoaDon.ThoiGianXuat.Month, hoaDon.ThoiGianXuat.Year})
                .Select(g => new
                {
                    Thang = g.Key.Month,
                    Nam = g.Key.Year,
                    TongTien = g.Sum(hoaDon => hoaDon.Ves.Sum(ve => ve.LoaiVe.GiaTien))
                }).ToList();

            DateTime pivotTimeClear = DateTime.Now.AddMonths(-5);
            foreach (var item in data)
            {
                DateTime dateTimeItem = new DateTime(item.Nam , item.Thang , DateTime.Now.Day);
                if (dateTimeItem.Date < pivotTimeClear.Date)
                {
                    data.Remove(item);
                }
            }

            for (int i = 0; i < 5; i++)
            {
                DateTime pivotTime = DateTime.Now.AddMonths(-i);
                bool check = false;
                foreach (var item in data)
                {
                    if (check)
                    {
                        continue;
                    }
                    DateTime dateTimeItem = new DateTime(item.Nam, item.Thang, DateTime.Now.Day);
                    if (dateTimeItem.Date == pivotTime.Date)
                    {
                        check = true;
                    }
                }

                if (!check)
                {
                    data.Add(new
                    {
                        Thang = pivotTime.Month,
                        Nam = pivotTime.Year,
                        TongTien = (double)0
                    });
                }
            }

            data = data.OrderBy(item => item.Thang).OrderBy(item => item.Nam).ToList();

            int soVeDatTrongThang = context.Ves.Where(ve => ve.HoaDon.ThoiGianXuat.Month == DateTime.Now.Month && ve.HoaDon.ThoiGianXuat.Year == DateTime.Now.Year).Count();

            int soKhachHangDangKiTrongThang =
                context.KhachHangs.Where(khachHang => khachHang.ThoiGianDangKi.Month == DateTime.Now.Month && khachHang.ThoiGianDangKi.Year == DateTime.Now.Year && !khachHang.MaKhachHang.Contains("NONAME")).Count();

            await Chat.UpdateChartToManagerBrower(data.Select(c => c.Thang.ToString()).ToArray(),
                data.Select(c => Int32.Parse(c.TongTien.ToString())).ToArray() , soVeDatTrongThang , soKhachHangDangKiTrongThang);
        }
    }
}