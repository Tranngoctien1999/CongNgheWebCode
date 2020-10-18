using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanVeDiTourDuLich.Controllers
{
    public class UserController : Controller
    {
        DataContext _db = new DataContext();
        // GET: User
      
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Index(string TaiKhoanDangNhap, string password)
        {
            if (ModelState.IsValid)
            {


              
                var data = _db.TaiKhoans.Where(s => s.TaiKhoanDangNhap.Equals(TaiKhoanDangNhap) && s.MatKhau.Equals(password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                   
                    Session["MaTaiKhoan"] = data.FirstOrDefault().MaTaiKhoan;
                    Session["TaiKhoanDangNhap"] = data.FirstOrDefault().TaiKhoanDangNhap;
                    return RedirectToAction("/home/index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }


    }
}