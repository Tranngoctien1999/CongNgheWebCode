using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BanVeDiTourDuLich.ViewModels;
using Microsoft.Ajax.Utilities;

namespace BanVeDiTourDuLich.Controllers
{
    public class DatVeController : Controller
    {
        private DataContext context = new DataContext();
        [HttpPost]
        public ActionResult Index(string maTour , string maLoaiVe ,int soLuong )
        {
            if (Session["MaTaiKhoan"] == null)
            {
                Response.StatusCode = 400;
                return Json(new {errDetail = "Bạn chưa đăng nhập ! Hãy đăng nhập !"},JsonRequestBehavior.AllowGet);
            }

            var LoaiVe = context.Tours.Find(maTour).LoaiVes.Where(loaiVe => loaiVe.MaLoaiVe == maLoaiVe)
                .FirstOrDefault();

            if (LoaiVe.SoLuong < soLuong)
            {
                Response.StatusCode = 400;
                return Json(new {errDetail = "Không có đủ vé"},JsonRequestBehavior.AllowGet);
            }

            List<ThongTinHangTrongGio> gioHang;
            if (Session["GioHang"] == null)
            {
                Session["GioHang"] = new List<ThongTinHangTrongGio>();
                gioHang = (List<ThongTinHangTrongGio>) Session["GioHang"];
            }
            else
            {
                gioHang = (List<ThongTinHangTrongGio>) Session["GioHang"];
            }

            var checkList = gioHang.Where(hang => hang.MaTour == maTour && hang.MaLoaiVe == maLoaiVe);
            if (checkList.Count() > 0)
            {
                checkList.First().SoLuong += soLuong;
            }
            else
            {
                gioHang.Add(new ThongTinHangTrongGio(){ MaTour =  maTour , MaLoaiVe = maLoaiVe , SoLuong = soLuong});
            }
            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }
    }
}