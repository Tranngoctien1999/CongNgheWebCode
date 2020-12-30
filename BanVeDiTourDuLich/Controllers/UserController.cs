using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using BanVeDiTourDuLich.Models;
using BanVeDiTourDuLich.Utilizer;
using BanVeDiTourDuLich.ViewModels;

namespace BanVeDiTourDuLich.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _db = new DataContext();
        // GET: User

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        public ActionResult Register(string TenKhachHang, string Email, string TaiKhoanDangNhap, string MatKhau,
            string pass,
            string DiaChi, string SoDienThoai, int Gender, string NgaySinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_db.TaiKhoans.Find(TaiKhoanDangNhap) != null)
                        throw new Exception("Tài khoản này đã được đăng kí ! Vui lòng chọn tên đăng nhập khác");

                    if (string.IsNullOrEmpty(TaiKhoanDangNhap)) throw new Exception("Lỗi Tài khoản đăng nhập");

                    if (string.IsNullOrEmpty(TenKhachHang)) throw new Exception("Lỗi Tên khách hàng");

                    if (!SoDienThoai.ValidatePhoneNumber(true)) throw new Exception("Số điện thoại không hợp lệ");

                    if (!ValidationFunction.IsValidEmail(Email)) throw new Exception("Email không hợp lệ");

                    if (!ValidationFunction.IsValidPassword(MatKhau))
                        throw new Exception(
                            "Mật khẩu không hợp lệ ! Hãy nhập ít nhất 1 chữ số , một chữ cái viết hoa , dài ít nhất 8 kí tự");

                    if (string.Compare(MatKhau, pass) != 0) throw new Exception("Hãy nhập mật khẩu khớp nhau");


                    if (Gender > 1 || Gender < 0) throw new Exception("Lỗi thông tin giới tính");

                    try
                    {
                        DateTime.Parse(NgaySinh);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                }
                catch (Exception e)
                {
                    Response.StatusCode = 400;
                    return Json(new {msg = e.Message}, JsonRequestBehavior.AllowGet);
                }

                var identity = _db.IdentityTraces.Find(1);

                identity.KhachHangIdentity++;
                var khachHang = new KhachHang
                {
                    MaKhachHang = "KHACHHANG" + identity.KhachHangIdentity.ToString("00"),
                    Email = Email,
                    Ten = TenKhachHang,
                    DiaChi = DiaChi,
                    GioiTinh = Gender == 1 ? true : false,
                    MaLoaiKhachHang = "KHACHHANGTHUONG",
                    NgaySinh = DateTime.Parse(NgaySinh),
                    ThoiGianDangKi = DateTime.Now,
                    SoDienThoai = SoDienThoai
                };
                _db.KhachHangs.Add(khachHang);
                _db.SaveChanges();

                var taiKhoan = new TaiKhoan
                {
                    MaTaiKhoan = khachHang.MaKhachHang,
                    TaiKhoanDangNhap = TaiKhoanDangNhap,
                    MatKhau = MatKhau
                };

                _db.TaiKhoans.Add(taiKhoan);
                _db.SaveChanges();

                Response.StatusCode = 200;
                return Json(new {msg = "Thành Công"}, JsonRequestBehavior.AllowGet);
            }

            Response.StatusCode = 400;
            return Json(new {msg = "Lỗi ! Hãy Thử trong vài giây nữa"}, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult ThemThongTin(string TenKhachHang, string SoDienThoai, string Email, string DiaChi)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(TenKhachHang)) throw new Exception("Lỗi Tên khách hàng");

                    if (!ValidationFunction.IsValidEmail(Email)) throw new Exception("Email không hợp lệ");

                    if (!SoDienThoai.ValidatePhoneNumber(true)) throw new Exception("Số điện thoại không hợp lệ");
                }
                catch (Exception e)
                {
                    Response.StatusCode = 400;
                    return Json(new {msg = e.Message}, JsonRequestBehavior.AllowGet);
                }

                var identity = _db.IdentityTraces.Find(1);

                identity.KhachHangIdentity++;
                var khachHang = new KhachHang
                {
                    MaKhachHang = "KHACHHANG" + identity.KhachHangIdentity.ToString("00"),
                    Email = Email,
                    Ten = TenKhachHang,
                    DiaChi = DiaChi,
                    MaLoaiKhachHang = "KHACHHANGTHUONG",
                    ThoiGianDangKi = DateTime.Now,
                    SoDienThoai = SoDienThoai
                };
                _db.KhachHangs.Add(khachHang);
                _db.SaveChanges();
                Session["MaKhachHangVangLai"] = khachHang.MaKhachHang;
                Response.StatusCode = 200;
                return Json(new {msg = "Thành Công"}, JsonRequestBehavior.AllowGet);
            }

            Response.StatusCode = 400;
            return Json(new {msg = "Lỗi ! Hãy Thử trong vài giây nữa"}, JsonRequestBehavior.AllowGet);
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
                var data = _db.TaiKhoans
                    .Where(s => s.TaiKhoanDangNhap.Equals(TaiKhoanDangNhap) && s.MatKhau.Equals(password)).ToList();
                if (data.Count() > 0)
                {
                    if (data.First().NhanVien != null)
                    {
                        //add session
                        Session["MaTaiKhoan"] = data.FirstOrDefault().MaTaiKhoan;
                        Session["TaiKhoanDangNhap"] = data.FirstOrDefault().TaiKhoanDangNhap;
                        return RedirectToAction("Index", "Admin");
                    }

                    //add session
                    Session["MaTaiKhoan"] = data.FirstOrDefault().MaTaiKhoan;
                    Session["TaiKhoanDangNhap"] = data.FirstOrDefault().TaiKhoanDangNhap;
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.error = "Login failed";
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Signout()
        {
            if (Session["MaTaiKhoan"] != null) Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> SignInWithGoogleAndFacebook(string maKhachHang, string tenKhachHang,
            string duongDanAnh)
        {
            var taiKhoan = await _db.TaiKhoans.FindAsync(maKhachHang);
            if (taiKhoan != null)
            {
                Session["MaTaiKhoan"] = taiKhoan.MaTaiKhoan;
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }

            var checKhachHang = await _db.KhachHangs.FindAsync(maKhachHang);
            if (checKhachHang == null)
            {
                var khachHang = new KhachHang
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

            var newAccount = new TaiKhoan
            {
                MaTaiKhoan = maKhachHang
            };
            _db.TaiKhoans.Add(newAccount);
            await _db.SaveChangesAsync();
            Session["MaTaiKhoan"] = newAccount.MaTaiKhoan;
            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        public ActionResult InforUser(string id)
        {
            var check = false;
            if (Session["MaTaiKhoan"] != null)
                if (string.Compare(Session["MaTaiKhoan"].ToString(), id) == 0)
                {
                    check = true;
                    var khachHang = _db.KhachHangs.Find(id);
                    if (khachHang == null)
                    {
                        var nhanVien = _db.NhanViens.Find(id);
                        var thongTin = new ThongTinChiTietViewModel
                        {
                            NhanVien = nhanVien,
                            isCustomer = false
                        };
                        return View(thongTin);
                    }

                    var thongTin1 = new ThongTinChiTietViewModel
                    {
                        KhachHang = khachHang,
                        isCustomer = true
                    };
                    return View(thongTin1);
                }
            return Content("Bạn không có quyền xem trang này!");
        }

        [HttpPost]
        public ActionResult DoiMatKhau(string MatKhauCu, string MatKhauMoi, string NhapLaiMatKhauMoi)
        {
            if (Session["MaTaiKhoan"] != null)
            {
                var khachHang = _db.KhachHangs.Find(Session["MaTaiKhoan"].ToString());
                if (khachHang == null)
                {
                    Response.StatusCode = 400;
                    return Json(new {msg = "Không tìm được tài khoản của bạn! Hãy kiểm tra lại"},
                        JsonRequestBehavior.AllowGet);
                }

                if (string.Compare(khachHang.TaiKhoan.MatKhau, MatKhauCu) != 0)
                {
                    Response.StatusCode = 400;
                    return Json(new {msg = "Mật khẩu cũ không đúng! Thử lại !"},
                        JsonRequestBehavior.AllowGet);
                }

                if (string.Compare(MatKhauMoi, NhapLaiMatKhauMoi) != 0)
                {
                    Response.StatusCode = 400;
                    return Json(new {msg = "Mật khẩu mới nhập vào không khớp"},
                        JsonRequestBehavior.AllowGet);
                }

                Response.StatusCode = (int) HttpStatusCode.Accepted;
                khachHang.TaiKhoan.MatKhau = MatKhauMoi;
                _db.SaveChanges();
                return Json(new {msg = "Đổi mật khẩu thành công!"},
                    JsonRequestBehavior.AllowGet);
            }

            return Content("Bạn không có quyền xem trang này!");
        }


        [HttpPost]
        public ActionResult DoiLuong(string maNhanVien, string giaTri)
        {
            if (string.Compare(Session["MaTaiKhoan"].ToString(), "ADMIN") == 0)
            {
                var nhanVien = _db.NhanViens.Find(maNhanVien);
                if (nhanVien == null)
                {
                    Response.StatusCode = 400;
                    return Json(new {msg = "Không tìm được tài khoản của bạn! Hãy kiểm tra lại"},
                        JsonRequestBehavior.AllowGet);
                }

                double luong = 0;
                try
                {
                    luong = double.Parse(giaTri);
                }
                catch (Exception e)
                {
                    Response.StatusCode = 400;
                    return Json(new {msg = "Giá trị lương không hợp lệ"},
                        JsonRequestBehavior.AllowGet);
                }

                Response.StatusCode = (int) HttpStatusCode.Accepted;
                nhanVien.Luong = luong;
                _db.SaveChanges();
                return Json(
                    new
                    {
                        msg = string.Format("Đổi lương cho nhân viên {0} giá trị {1} thành công", nhanVien.Ten,
                            nhanVien.Luong)
                    },
                    JsonRequestBehavior.AllowGet);
            }
            return Content("Bạn không có quyền đổi lương nhân viên này!");
        }


        public JsonResult UploadFile()
        {
            try
            {
                for (var i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (Session["MaTaiKhoan"] != null)
                    {
                        var nameAndLocation = "/Content/images/Persions/" + file.FileName;
                        file.SaveAs(Server.MapPath(nameAndLocation));
                        var khachHang = _db.KhachHangs.Find(Session["MaTaiKhoan"].ToString());
                        if (khachHang == null)
                        {
                            Response.StatusCode = 400;
                            return Json(new {msg = "Không tìm được tài khoản của bạn! Hãy kiểm tra lại"},
                                JsonRequestBehavior.AllowGet);
                        }

                        khachHang.DuongDanAnh = nameAndLocation;
                        _db.SaveChanges();
                        Response.StatusCode = (int) HttpStatusCode.Accepted;
                        return Json(new {msg = "Thành Công"}, JsonRequestBehavior.AllowGet);
                    }

                    Response.StatusCode = 400;
                    return Json(new {msg = "Bạn không có quyền truy cập trang này"},
                        JsonRequestBehavior.AllowGet);
                }

                return Json(new {msg = "Bạn không có quyền truy cập trang này"},
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int) HttpStatusCode.NotAcceptable;
                return Json(new {msg = e.Message});
            }
        }


        [HttpPost]
        public ActionResult UpdateCustomerInformation(string CustomerName, int Gender, string Birthday, string Email
            , string SoDienThoai, string Address)
        {
            DateTime? NgaySinh = null;
            try
            {
                if (string.IsNullOrEmpty(CustomerName)) throw new Exception("Lỗi Tên Khách Hàng");

                if (Gender > 1 || Gender < 0) throw new Exception("Lỗi Giới Tính");

                if (string.IsNullOrEmpty(Birthday))
                    throw new Exception("Lỗi Ngày Sinh");
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

                if (!SoDienThoai.ValidatePhoneNumber(true)) throw new Exception("Lỗi số điện thoại");
            }
            catch (Exception e)
            {
                Response.StatusCode = 400;
                return Json(new {msg = e.Message}, JsonRequestBehavior.AllowGet);
            }

            if (Session["MaTaiKhoan"] != null)
            {
                var khachHang = _db.KhachHangs.Find(Session["MaTaiKhoan"].ToString());
                if (khachHang == null)
                {
                    Response.StatusCode = 400;
                    return Json(new {msg = "Không tìm được tài khoản của bạn! Hãy kiểm tra lại"},
                        JsonRequestBehavior.AllowGet);
                }

                khachHang.Ten = CustomerName;
                khachHang.DiaChi = Address;
                khachHang.Email = Email;
                khachHang.NgaySinh = NgaySinh.Value;
                khachHang.SoDienThoai = SoDienThoai;
                if (Gender == 0) khachHang.GioiTinh = false;
                _db.SaveChanges();
                Response.StatusCode = (int) HttpStatusCode.Accepted;
                return Json(new {msg = "Thành Công"},
                    JsonRequestBehavior.AllowGet);
            }

            Response.StatusCode = 400;
            return Json(new {msg = "Bạn không có quyền truy cập trang này!"},
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetListTour()
        {
            if (Session["MaTaiKhoan"] != null)
            {
                var khachHang = _db.KhachHangs.Find(Session["MaTaiKhoan"].ToString());
                if (khachHang == null)
                {
                    Response.StatusCode = 400;
                    return Json(new {msg = "Không tìm được tài khoản của bạn! Hãy kiểm tra lại"},
                        JsonRequestBehavior.AllowGet);
                }

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
                return Json(new {data}, JsonRequestBehavior.AllowGet);
            }

            Response.StatusCode = 400;
            return Json(new {msg = "Bạn không có quyền vào trang này !"},
                JsonRequestBehavior.AllowGet);
        }
    }
}