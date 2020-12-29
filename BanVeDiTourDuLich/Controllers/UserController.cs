using BanVeDiTourDuLich.Models;
using System;
using System.Globalization;
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
                    Response.StatusCode = 400;
                    return Json(  new { msg = "Không tìm được tài khoản của bạn! Hãy kiểm tra lại"} ,
                        JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (string.Compare(khachHang.TaiKhoan.MatKhau, MatKhauCu) != 0)
                    {
                        Response.StatusCode = 400;
                        return Json(new { msg = "Mật khẩu cũ không đúng! Thử lại !" },
                            JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (string.Compare(MatKhauMoi, NhapLaiMatKhauMoi) != 0)
                        {
                            Response.StatusCode = 400;
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

        public JsonResult UploadFile()
        {
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (Session["MaTaiKhoan"] != null)
                    {
                        string nameAndLocation = "/Content/images/Persions/" + file.FileName;
                        file.SaveAs(Server.MapPath(nameAndLocation));
                        KhachHang khachHang = _db.KhachHangs.Find(Session["MaTaiKhoan"].ToString());
                        if (khachHang == null)
                        {
                            Response.StatusCode = 400;
                            return Json(new { msg = "Không tìm được tài khoản của bạn! Hãy kiểm tra lại" },
                                JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            khachHang.DuongDanAnh = nameAndLocation;
                            _db.SaveChanges();
                            Response.StatusCode = (int)HttpStatusCode.Accepted;
                            return Json(new { msg = "Thành Công" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return Json(new { msg = "Bạn không có quyền truy cập trang này" },
                            JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { msg = "Bạn không có quyền truy cập trang này" },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                return Json(new { msg = e.Message });
            }
        }


        [HttpPost]
        public ActionResult UpdateCustomerInformation(string CustomerName, int Gender, string Birthday, string Email,
            string Address)
        {
            DateTime? NgaySinh = null;
            try
            {
                if (string.IsNullOrEmpty(CustomerName))
                {
                    throw new Exception("Lỗi Tên Khách Hàng");
                }

                if (Gender > 1 || Gender < 0)
                {
                    throw new Exception("Lỗi Giới Tính");
                }

                if (string.IsNullOrEmpty(Birthday))
                {
                    throw new Exception("Lỗi Ngày Sinh");
                }
                else
                {
                    try
                    {
                        NgaySinh = DateTime.ParseExact(Birthday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            NgaySinh = DateTime.ParseExact(Birthday, "d/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        catch (Exception exception)
                        {
                            throw new Exception("Lỗi Ngày Sinh");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Response.StatusCode = 400;
                return Json(new {msg = e.Message}, JsonRequestBehavior.AllowGet);
            }

            if (Session["MaTaiKhoan"] != null)
            {
                KhachHang khachHang = _db.KhachHangs.Find(Session["MaTaiKhoan"].ToString());
                if (khachHang == null)
                {
                    Response.StatusCode = 400;
                    return Json(new {msg = "Không tìm được tài khoản của bạn! Hãy kiểm tra lại"},
                        JsonRequestBehavior.AllowGet);
                }
                else
                {
                    khachHang.Ten = CustomerName;
                    khachHang.DiaChi = Address;
                    khachHang.Email = Email;
                    khachHang.NgaySinh = NgaySinh.Value;
                    if (Gender == 0)
                    {
                        khachHang.GioiTinh = false;
                    }
                    _db.SaveChanges();
                    Response.StatusCode = (int)HttpStatusCode.Accepted;
                    return Json(new { msg = "Thành Công" },
                        JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                Response.StatusCode = 400;
                return Json(new { msg = "Bạn không có quyền truy cập trang này!" },
                    JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetListTour()
        {
            if (Session["MaTaiKhoan"] != null)
            {
                KhachHang khachHang = _db.KhachHangs.Find(Session["MaTaiKhoan"].ToString());
                if (khachHang == null)
                {
                    Response.StatusCode = 400;
                    return Json(new { msg = "Không tìm được tài khoản của bạn! Hãy kiểm tra lại" },
                        JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = khachHang.HoaDons.SelectMany(hoaDon => hoaDon.Ves
                        .GroupBy(ve => new {ve.Tour.MaTour, ve.LoaiVe.MaLoaiVe}).Select(g => new
                        {
                            SoLuongVe = g.Count(),
                            LoaiVe = _db.LoaiVes.Find(g.Key.MaLoaiVe).Ten,
                            TongTien = _db.LoaiVes.Find(g.Key.MaLoaiVe).GiaTien * g.Count(),
                            TourCode = g.Key.MaTour,
                            NgayKhoiHanh = _db.Tours.Find(g.Key.MaTour).ThoigianDi.ToString("dd/MM/yyyy"),
                            TinhTrang = _db.Tours.Find(g.Key.MaTour).ThoigianDi > DateTime.Now,
                            TenTour = _db.Tours.Find(g.Key.MaTour).DiaDiemDen.TenDiaDiem,
                            DiemDen = _db.Tours.Find(g.Key.MaTour).DiaDiemDen.TenDiaDiem
                        }).ToList()).ToList();
                    Response.StatusCode = 200;
                    return Json(new { data = data }, JsonRequestBehavior.AllowGet);
                }
            }
            Response.StatusCode = 400;
            return Json(new { msg = "Bạn không có quyền vào trang này !" },
                JsonRequestBehavior.AllowGet);
        }
    }
}