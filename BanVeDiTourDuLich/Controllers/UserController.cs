using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanVeDiTourDuLich.Models;

namespace BanVeDiTourDuLich.Controllers
{
    public class UserController : Controller
    {
        DataContext _db = new DataContext();
        // GET: User

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]

        public ActionResult Register(TaiKhoan _user,string pass)
        {
            if (ModelState.IsValid)
            {
                var check = _db.TaiKhoans.FirstOrDefault(s => s.TaiKhoanDangNhap == _user.TaiKhoanDangNhap);
                if (check == null)
                {
                   
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    if(_user.MatKhau==pass)
                    {
                        _db.TaiKhoans.Add(_user);
                        _db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error = "Mật khẩu phải giống nhau";
                        return View();

                    }
                    
                }
                
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View("Index");


        }

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
                    return View("~/Views/Admin/Index.cshtml");

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