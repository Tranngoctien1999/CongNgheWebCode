using BanVeDiTourDuLich.Models;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

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

        public ActionResult Register(TaiKhoan _user, string pass)
        {
            if (ModelState.IsValid)
            {
                var check = _db.TaiKhoans.FirstOrDefault(s => s.TaiKhoanDangNhap == _user.TaiKhoanDangNhap);
                if (check == null)
                {

                    _db.Configuration.ValidateOnSaveEnabled = false;
                    if (_user.MatKhau == pass)
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
                    if (data.First().NhanVien != null)
                    {
                        //add session
                        Session["MaTaiKhoan"] = data.FirstOrDefault().MaTaiKhoan;
                        Session["TaiKhoanDangNhap"] = data.FirstOrDefault().TaiKhoanDangNhap;
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        //add session
                        Session["MaTaiKhoan"] = data.FirstOrDefault().MaTaiKhoan;
                        Session["TaiKhoanDangNhap"] = data.FirstOrDefault().TaiKhoanDangNhap;
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public ActionResult Signout()
        {
            if (Session["MaTaiKhoan"] != null)
            {
                Session.Clear();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> SignInWithGoogleAndFacebook(string maKhachHang, string tenKhachHang, string duongDanAnh)
        {
            TaiKhoan taiKhoan = await _db.TaiKhoans.FindAsync(maKhachHang);
            if (taiKhoan != null)
            {
                Session["MaTaiKhoan"] = taiKhoan.MaTaiKhoan;
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }
            KhachHang checKhachHang = await _db.KhachHangs.FindAsync(maKhachHang);
            if (checKhachHang == null)
            {
                KhachHang khachHang = new KhachHang()
                {
                    MaKhachHang = maKhachHang,
                    DuongDanAnh = duongDanAnh,
                    Ten = tenKhachHang,
                    ThoiGianDangKi = DateTime.Now,
                    MaLoaiKhachHang = "KHACHHANGTHUONG"
                };
                _db.KhachHangs.Add(khachHang);
                await _db.SaveChangesAsync();
            }
            TaiKhoan newAccount = new TaiKhoan()
            {
                MaTaiKhoan = maKhachHang,
            };
            _db.TaiKhoans.Add(newAccount);
            await _db.SaveChangesAsync();
            Session["MaTaiKhoan"] = newAccount.MaTaiKhoan;
            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        public ActionResult InforUser(string id)
        {
            bool check = false;
            if (Session["MaTaiKhoan"] != null)
            {
                if (string.Compare(Session["MaTaiKhoan"].ToString(),id) == 0)
                {
                    check = true;
                    KhachHang khachHang = _db.KhachHangs.Find(id);
                    return View(khachHang);
                }
            }
            return Content("Bạn không có quyền xem trang này!");
        }

        [HttpPost]
        public ActionResult DoiMatKhau(string MatKhauCu , string MatKhauMoi , string NhapLaiMatKhauMoi)
        {
            if (Session["MaTaiKhoan"] != null)
            {
                KhachHang khachHang = _db.KhachHangs.Find(Session["MaTaiKhoan"].ToString());
                if (khachHang == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                    return Json(  new { msg = "Không tìm được tài khoản của bạn! Hãy kiểm tra lại"} ,
                        JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (string.Compare(khachHang.TaiKhoan.MatKhau, MatKhauCu) != 0)
                    {
                        Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                        return Json(new { msg = "Mật khẩu cũ không đúng! Thử lại !" },
                            JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (string.Compare(MatKhauMoi, NhapLaiMatKhauMoi) != 0)
                        {
                            Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                            return Json(new { msg = "Mật khẩu mới nhập vào không khớp" },
                                JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            Response.StatusCode = (int)HttpStatusCode.Accepted;
                            khachHang.TaiKhoan.MatKhau = MatKhauMoi;
                            _db.SaveChanges();
                            return Json(new { msg = "Đổi mật khẩu thành công!" },
                                JsonRequestBehavior.AllowGet);

                        }
                    }
                }
            }
            return Content("Bạn không có quyền xem trang này!");
        }
    }
}