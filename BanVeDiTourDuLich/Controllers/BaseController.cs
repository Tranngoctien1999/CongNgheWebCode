using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanVeDiTourDuLich.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            // your code here
            if (Session["TaiKhoanDangNhap"] == null)
            {
                DataContext _db = new DataContext();
                if (Request.Cookies["TaiKhoanDangNhap"] != null && Request.Cookies["password"] != null)
                {
                    string TaiKhoanDangNhap = Request.Cookies["TaiKhoanDangNhap"].Value;
                    string password = Request.Cookies["password"].Value;
                    var data = _db.TaiKhoans
                        .Where(s => s.TaiKhoanDangNhap.Equals(TaiKhoanDangNhap) && s.MatKhau.Equals(password)).ToList();
                    if (data.Count() > 0)
                    {
                        if (data.First().NhanVien != null)
                        {
                            //add session
                            Session["MaTaiKhoan"] = data.FirstOrDefault().MaTaiKhoan;
                            Session["TaiKhoanDangNhap"] = data.FirstOrDefault().TaiKhoanDangNhap;
                        }

                        //add session
                        Session["MaTaiKhoan"] = data.FirstOrDefault().MaTaiKhoan;
                        Session["TaiKhoanDangNhap"] = data.FirstOrDefault().TaiKhoanDangNhap;
                    }
                }
            }
        }
    }
}