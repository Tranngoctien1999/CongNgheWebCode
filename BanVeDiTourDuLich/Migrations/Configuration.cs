using System;
using BanVeDiTourDuLich.Models;

namespace BanVeDiTourDuLich.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BanVeDiTourDuLich.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BanVeDiTourDuLich.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.


            // Thêm địa điểm
            DiaDiem diaDiem1 = new DiaDiem()
            {
                MaDiaDiem = "DIADIEM1",
                DiaChi = "236 Hoàng Quốc Việt , Bắc Từ Liêm , Hà Nội",
                TenDiaDiem = "Học viện kĩ thuật quân sự",
                DuongDanAnh = "/Content/images/Destinations/MADIADIEM1.jpg"
            };

            DiaDiem diaDiem2 = new DiaDiem()
            {
                MaDiaDiem = "DIADIEM2",
                DiaChi = "Lực Điền ,Minh Châu , Yên mỹ ,Hưng Yên",
                TenDiaDiem = "Đảo Cò",
                DuongDanAnh = "/Content/images/Destinations/MADIADIEM2.jpg"
            };


            DiaDiem diaDiem3 = new DiaDiem()
            {
                MaDiaDiem = "DIADIEM3",
                DiaChi = "Yên Đồng , Vĩnh Tường , Vĩnh Phúc",
                TenDiaDiem = "Tam Đảo",
                DuongDanAnh = "/Content/images/Destinations/MADIADIEM3.jpg"
            };

            DiaDiem diaDiem4 = new DiaDiem()
            {
                MaDiaDiem = "DIADIEM4",
                DiaChi = "Xóm 17 , Xuân Tín , Thọ Xuân , Thanh Hóa",
                TenDiaDiem = "Làng Nghề 17",
                DuongDanAnh = "/Content/images/Destinations/MADIADIEM4.jpg"
            };

            DiaDiem diaDiem5 = new DiaDiem()
            {
                MaDiaDiem = "DIADIEM5",
                DiaChi = "Hoằng Hóa , Thanh Hóa",
                TenDiaDiem = "Biển Hoằng Hóa",
                DuongDanAnh = "/Content/images/Destinations/MADIADIEM5.jpg"
            };
           
            DiaDiem diaDiem6 = new DiaDiem()
            {
                MaDiaDiem = "DIADIEM6",
                DiaChi = "Hà Nội",
                TenDiaDiem = "Thành phố Hà Nội",
                DuongDanAnh = "/Content/images/Destinations/MADIADIEM6.jpg"
            };
            DiaDiem diaDiem7 = new DiaDiem()
            {
                MaDiaDiem = "DIADIEM7",
                DiaChi = "Hồ Chí Minh",
                TenDiaDiem = "Thành phố Hồ Chí Minh",
                DuongDanAnh = "/Content/images/Destinations/MADIADIEM7.jpg"
            };
            DiaDiem diaDiem8 = new DiaDiem()
            {
                MaDiaDiem = "DIADIEM8",
                DiaChi = "Sa Pa - Lào Cai",
                TenDiaDiem = "Thị trấn Sa Pa mờ sương",
                DuongDanAnh = "/Content/images/Destinations/MADIADIEM8.jpg"
            };
            context.DiaDiems.AddOrUpdate(diaDiem1, diaDiem2 , diaDiem3 , diaDiem4 , diaDiem5,diaDiem6,diaDiem7,diaDiem8);


            // Thêm Tour
            Tour tour = new Tour()
            {
                MaTour = "TOUR1",
                MaDiemDen = diaDiem1.MaDiaDiem,
                MaDiemDi = diaDiem6.MaDiaDiem,
                SoGio = 10,
                ThoigianDi = new DateTime(2020, 12, 29),
            };

            Tour tour9 = new Tour()
            {
                MaTour = "TOUR12",
                MaDiemDen = diaDiem1.MaDiaDiem,
                MaDiemDi = diaDiem6.MaDiaDiem,
                SoGio = 12,
                ThoigianDi = new DateTime(2020, 12, 30)
            };

            Tour tour2 = new Tour()
            {
                MaTour = "TOUR2",
                MaDiemDen = diaDiem2.MaDiaDiem,
                MaDiemDi = diaDiem6.MaDiaDiem,
                SoGio = 20,
                ThoigianDi = new DateTime(2020, 12, 29),
            };

            Tour tour3 = new Tour()
            {
                MaTour = "TOUR3",
                MaDiemDen = diaDiem3.MaDiaDiem,
                MaDiemDi = diaDiem6.MaDiaDiem,
                SoGio = 15,
                ThoigianDi = new DateTime(2020, 12, 29),
            };

            Tour tour4 = new Tour()
            {
                MaTour = "TOUR5",
                MaDiemDen = diaDiem4.MaDiaDiem,
                MaDiemDi = diaDiem6.MaDiaDiem,
                SoGio = 48,
                ThoigianDi = new DateTime(2020, 12, 29),
            };
            Tour tour5 = new Tour()
            {
                MaTour = "TOUR6",
                MaDiemDen = diaDiem8.MaDiaDiem,
                MaDiemDi = diaDiem6.MaDiaDiem,
                SoGio = 48,
                ThoigianDi = new DateTime(2020, 12, 29),
            };
            Tour tour6 = new Tour()
            {
                MaTour = "TOUR7",
                MaDiemDen = diaDiem8.MaDiaDiem,
                MaDiemDi = diaDiem7.MaDiaDiem,
                SoGio = 48,
                ThoigianDi = new DateTime(2020, 12, 18),
            };

            Tour tour7 = new Tour()
            {
                MaTour = "TOUR8",
                MaDiemDen = diaDiem4.MaDiaDiem,
                MaDiemDi = diaDiem7.MaDiaDiem,
                SoGio = 48,
                ThoigianDi = new DateTime(2020, 12, 19),
            };
            Tour tour8 = new Tour()
            {
                MaTour = "TOUR9",
                MaDiemDen = diaDiem3.MaDiaDiem,
                MaDiemDi = diaDiem7.MaDiaDiem,
                SoGio = 48,
                ThoigianDi = new DateTime(2020, 12, 19),
            };

            context.Tours.AddOrUpdate(tour , tour2 , tour3 , tour4,tour5,tour6,tour7,tour8 , tour9);


            // Thêm loại vé

            LoaiVe loaiTreEm8 = new LoaiVe()
            {
                MaLoaiVe = "VETREEMTOUR8",
                Ten = "Vé Trẻ Em",
                SoLuong = 10,
                MaTour = tour8.MaTour,
                GiaTien = 2000000
            };
            context.LoaiVes.AddOrUpdate(loaiTreEm8);

            LoaiVe loaiNguoiLon8 = new LoaiVe()
            {
                MaLoaiVe = "VENGUOILONTOUR8",
                Ten = "Vé Người Lớn",
                SoLuong = 20,
                MaTour = tour8.MaTour,
                GiaTien = 2500000
            };
            context.LoaiVes.AddOrUpdate(loaiTreEm8 , loaiNguoiLon8);
            LoaiVe loaiTreEm7 = new LoaiVe()
            {
                MaLoaiVe = "VETREEMTOUR7",
                Ten = "Vé Trẻ Em",
                SoLuong = 10,
                MaTour = tour7.MaTour,
                GiaTien = 2000000
            };
            context.LoaiVes.AddOrUpdate(loaiTreEm7);

            LoaiVe loaiNguoiLon7 = new LoaiVe()
            {
                MaLoaiVe = "VENGUOILONTOUR7",
                Ten = "Vé Người Lớn",
                SoLuong = 20,
                MaTour = tour7.MaTour,
                GiaTien = 2500000
            };
            context.LoaiVes.AddOrUpdate(loaiTreEm7 , loaiNguoiLon7);

            LoaiVe loaiTreEm6 = new LoaiVe()
            {
                MaLoaiVe = "VETREEMTOUR6",
                Ten = "Vé Trẻ Em",
                SoLuong = 10,
                MaTour = tour6.MaTour,
                GiaTien = 2000000
            };
            context.LoaiVes.AddOrUpdate(loaiTreEm6);

            LoaiVe loaiNguoiLon6 = new LoaiVe()
            {
                MaLoaiVe = "VENGUOILONTOUR6",
                Ten = "Vé Người Lớn",
                SoLuong = 20,
                MaTour = tour6.MaTour,
                GiaTien = 2500000
            };
            context.LoaiVes.AddOrUpdate(loaiTreEm6, loaiNguoiLon6);


            LoaiVe loaiTreEm5 = new LoaiVe()
            {
                MaLoaiVe = "VETREEMTOUR5",
                Ten = "Vé Trẻ Em",
                SoLuong = 10,
                MaTour = tour5.MaTour,
                GiaTien = 2000000
            };
            context.LoaiVes.AddOrUpdate(loaiTreEm5);

            LoaiVe loaiNguoiLon5 = new LoaiVe()
            {
                MaLoaiVe = "VENGUOILONTOUR5",
                Ten = "Vé Người Lớn",
                SoLuong = 20,
                MaTour = tour5.MaTour,
                GiaTien = 2500000
            };
            context.LoaiVes.AddOrUpdate(loaiTreEm5, loaiNguoiLon5);

            LoaiVe loaiTreEm4 = new LoaiVe()
            {
                MaLoaiVe = "VETREEMTOUR4",
                Ten = "Vé Trẻ Em",
                SoLuong = 10,
                MaTour = tour4.MaTour,
                GiaTien = 2000000
            };
            context.LoaiVes.AddOrUpdate(loaiTreEm4);

            LoaiVe loaiNguoiLon4 = new LoaiVe()
            {
                MaLoaiVe = "VENGUOILONTOUR4",
                Ten = "Vé Người Lớn",
                SoLuong = 20,
                MaTour = tour4.MaTour,
                GiaTien = 2500000
            };
            context.LoaiVes.AddOrUpdate(loaiTreEm4, loaiNguoiLon4);

            LoaiVe loaiTreEm = new LoaiVe()
            {
                MaLoaiVe = "VETREEMTOUR1",
                Ten = "Vé Trẻ Em",
                SoLuong = 10,
                MaTour = tour.MaTour,
                GiaTien = 2000000
            };
            context.LoaiVes.AddOrUpdate(loaiTreEm);

            LoaiVe loaiNguoiLon = new LoaiVe()
            {
                MaLoaiVe = "VENGUOILONTOUR1",
                Ten = "Vé Người Lớn",
                SoLuong = 20,
                MaTour = tour.MaTour,
                GiaTien = 2500000
            };

            LoaiVe loaiNguoiLon1 = new LoaiVe()
            {
                MaLoaiVe = "VENGUOILONTOUR2",
                Ten = "Vé Người Lớn",
                SoLuong = 10,
                MaTour = tour2.MaTour,
                GiaTien = 800000
            };
            context.LoaiVes.AddOrUpdate(loaiNguoiLon1 , loaiNguoiLon);

            LoaiVe loaiTreEm1 = new LoaiVe()
            {
                MaLoaiVe = "VETREEMTOUR2",
                Ten = "Vé Trẻ Em",
                SoLuong = 20,
                MaTour = tour2.MaTour,
                GiaTien = 100000
            };
            context.LoaiVes.AddOrUpdate(loaiTreEm1);
            LoaiVe loaiNguoiLon2 = new LoaiVe()
            {
                MaLoaiVe = "VENGUOILONTOUR3",
                Ten = "Vé Người Lớn",
                SoLuong = 10,
                MaTour = tour3.MaTour,
                GiaTien = 1000000
            };
            context.LoaiVes.AddOrUpdate(loaiNguoiLon2);

            LoaiVe loaiTreEm2 = new LoaiVe()
            {
                MaLoaiVe = "VETREEMTOUR3",
                Ten = "Vé Trẻ Em",
                SoLuong = 20,
                MaTour = tour3.MaTour,
                GiaTien = 800000
            };
            context.LoaiVes.AddOrUpdate(loaiTreEm2);

            // Thêm Loại Nhân viên
            LoaiNhanVien nhanVien = new LoaiNhanVien()
            {
                MaLoaiNhanVien = "NHANVIEN0",
                TenLoaiNhanVien = "Nhân viên bán hàng",
                ChiTiet = "Nhân viên bán hàng tại bàn hỗ trợ của công ty"
            };

            LoaiNhanVien quanTriVien = new LoaiNhanVien()
            {
                MaLoaiNhanVien = "ADMIN",
                TenLoaiNhanVien = "Quản trị viên",
                ChiTiet = "Quản trị viên của các nhân viên"
            };
            context.LoaiNhanViens.AddOrUpdate(nhanVien);
            context.LoaiNhanViens.AddOrUpdate(quanTriVien);

            // -- Thêm nhân viên

            NhanVien nhanVien1 = new NhanVien()
            {
                MaNhanVien = "NHANVIEN01",
                Ten = "Nguyễn Thành Công",
                MaLoaiNhanVien = nhanVien.MaLoaiNhanVien,
                Luong = 20000000,
                NgaySinh = new DateTime(1999, 10, 29),
                NgayVaoLam = new DateTime(2020, 09, 10),
                DuongDanAnh = "/Content/images/Persions/NHANVIEN01.jpg",
            };

            NhanVien nhanVien2 = new NhanVien()
            {
                MaNhanVien = "ADMIN",
                Ten = "Bùi Đăng Việt",
                MaLoaiNhanVien = quanTriVien.MaLoaiNhanVien,
                Luong = 200000,
                NgaySinh = new DateTime(1999 , 12 , 20),
                NgayVaoLam = DateTime.Now,
                DuongDanAnh = "/Content/images/Persions/NHANVIEN0.jpg"
            };

            context.NhanViens.AddOrUpdate(nhanVien1);
            context.NhanViens.AddOrUpdate(nhanVien2);
            //them tai khoan nhan vien
            TaiKhoan tk2 = new TaiKhoan()
            {
                MaTaiKhoan = "ADMIN",
                TaiKhoanDangNhap = "admin",
                MatKhau = "123456",
            };
            context.TaiKhoans.AddOrUpdate(tk2);

            TaiKhoan account = new TaiKhoan()
            {
                MaTaiKhoan = nhanVien1.MaNhanVien,
                MatKhau = "123456",
                TaiKhoanDangNhap = "user1"
            };

            context.TaiKhoans.AddOrUpdate(account);

            // Thêm Loại khách hàng

            LoaiKhachHang loaiKhachHang1 = new LoaiKhachHang()
            {
                MaLoaiKhachHang = "KHACHHANGVANG",
                Ten = "Khách hàng loại vàng",
                ChiTiet = "Khách hàng có số tiền tích lũy đạt 2.000.0000 VNĐ trở lên"
            };

            LoaiKhachHang loaiKhachHang = new LoaiKhachHang()
            {
                MaLoaiKhachHang = "KHACHHANGTHUONG",
                Ten = "Khách hàng loại thường",
                ChiTiet = "Khách hàng mới đăng kí tài khoản"
            };
            context.LoaiKhachHangs.AddOrUpdate(loaiKhachHang);
            context.LoaiKhachHangs.AddOrUpdate(loaiKhachHang1);

            ////// Thêm khách hàng
            KhachHang khachHang1 = new KhachHang()
            {
                MaKhachHang = "1",
                Ten = "Trần Ngọc Tiến",
                NgaySinh = new DateTime(1999, 10, 29),
                MaLoaiKhachHang = loaiKhachHang1.MaLoaiKhachHang,
                ThoiGianDangKi = new DateTime(2020, 10, 1),
                DuongDanAnh = "/Content/images/Persions/KH01.jpg",
                GioiTinh = true,
                Email = "NgocTien123@gmail.com",
                DiaChi = "236 Hoàng Quốc Việt",
            };
            context.KhachHangs.AddOrUpdate(khachHang1);

            TaiKhoan account1 = new TaiKhoan()
            {
                MaTaiKhoan = khachHang1.MaKhachHang,
                MatKhau = "123456",
                TaiKhoanDangNhap = "user2"
            };

            context.TaiKhoans.AddOrUpdate(account1);

            KhachHang khachHang2 = new KhachHang()
            {
                MaKhachHang = "2",
                Ten = "Phạm Xuân Tiến",
                NgaySinh = new DateTime(1999, 10, 29),
                MaLoaiKhachHang = loaiKhachHang1.MaLoaiKhachHang,
                ThoiGianDangKi = new DateTime(2020, 10, 1),
                DuongDanAnh = "/Content/images/Persions/KH02.jpg",
                GioiTinh = true,
                Email = "NgocTien123@gmail.com",
                DiaChi = "236 Hoàng Quốc Việt",
            };
            context.KhachHangs.AddOrUpdate(khachHang2);

            TaiKhoan account2 = new TaiKhoan()
            {
                MaTaiKhoan = khachHang2.MaKhachHang,
                MatKhau = "123456",
                TaiKhoanDangNhap = "user3"
            };

            context.TaiKhoans.AddOrUpdate(account2);


           // Thêm hóa đơn
           HoaDon hoaDon1 = new HoaDon()
           {
               MaHoaDon = "HD01",
               MaNhanVien = nhanVien1.MaNhanVien,
               MaKhachHang = khachHang1.MaKhachHang,
               ThoiGianXuat = DateTime.Now
           };

           HoaDon hoaDon2 = new HoaDon()
           {
               MaHoaDon = "HD02",
               MaNhanVien = nhanVien1.MaNhanVien,
               MaKhachHang = khachHang1.MaKhachHang,
               ThoiGianXuat = DateTime.Now
           };
            context.HoaDons.AddOrUpdate(hoaDon1 , hoaDon2);

            //// Thêm vé
            Ve ve = new Ve()
            {
                MaVe = "Ve01",
                MaTour = tour.MaTour,
                MaHoaDon = hoaDon1.MaHoaDon,
                MaLoaiVe = loaiNguoiLon.MaLoaiVe,
            };

            Ve ve1 = new Ve()
            {
                MaVe = "Ve02",
                MaTour = tour2.MaTour,
                MaHoaDon = hoaDon2.MaHoaDon,
                MaLoaiVe = loaiNguoiLon1.MaLoaiVe,
            };
            context.Ves.AddOrUpdate(ve, ve1);

            // Add Comment

            NhanXet nhanXet = new NhanXet()
            {
                MaKhachHang = khachHang1.MaKhachHang,
                MaTour = tour.MaTour,
                NoiDung = "Tour này hay quá"
            };
            context.NhanXets.AddOrUpdate(nhanXet);
            


            // Them Chi tiet Tour

            ChiTietTour chiTiet = new ChiTietTour()
            {
                MaTour = tour.MaTour,
                ChiTiet = "Di Ha Giang"
            };
            ChiTietTour chiTiet2 = new ChiTietTour()
            {
                MaTour = tour2.MaTour,
                ChiTiet = "Hàng năm, cứ đến mùa gió heo may thổi về vào khoảng tháng 9 âm lịch, là các loài cò, vạc và chim nước lại bay về quần tụ ở Đảo cò Chi Lăng Nam cho đến tận tháng 4 năm sau. Đặc biệt vào tháng 12, những ngày đất trời lập đông là thời điểm tập trung số lượng chim lớn nhất trong năm. Đến tham quan đảo cò Chi Lăng Nam mùa này, ấn tượng đầu tiên với du khách sẽ là cảm giác choáng ngợp trước khung cảnh hàng vạn chú chim đậu san sát trên các tầng cây, trông xa như những cành hoa điểm đầy bông trắng. Nhộn nhịp nhất là lúc bình mình vừa ló dạng, và hoàng hôn chiều ráng. Sáng sớm, từng đàn cò kéo nhau đi kiếm ăn, tung đôi cánh trắng chao lượn giữa không trung rồi mất hút. Tà dương, khi những tia nắng cuối ngày ngả dài trên mặt hồ phẳng lặng, từng đàn cò lại nối nhau bay về tổ, cất tiếng kêu huyên náo cả một vùng. Đây còn là lúc “giao ca” giữa cò và vạc. Trên một chiếc xuồng cỡ nhỏ để không làm náo động không gian, người chèo xuồng sẽ chầm chậm dạo quanh hồ, cho du khách thỏa thích quan sát cuộc sống sinh động của các lài chim. Bạn sẽ càng thích thú khi được xem tổ, ngắm những chú cò con tập tễnh, đôi chân vẫn còn chưa vững...Nếu đi ít người, bạn có thể thong thả đạp vịt, tự mình khám phá thiên nhiên kỳ thú nơi đây. Và nếu muốn tìm hiểu nhiều hơn về tập tính loài cò, vạc thì hãy ở lại một đêm bên Đảo cò Chi Lăng Nam. Đặc biệt, sẽ là một kỷ niệm khó quên khi xuồng lướt nhẹ trên mặt hồ sóng sánh ánh trăng.Với cảnh quan hữu tình và thế giới sinh động của các loài chim, Đảo cò Chi Lăng Nam sẽ mang đến cho bạn những phút giây thư thái, hòa mình cùng thiên nhiên."
            };
            ChiTietTour chiTiet3 = new ChiTietTour()
            {
                MaTour = tour3.MaTour,
                ChiTiet = "Thị trấn Tam Đảo thuộc huyện Tam Đảo có tổng diện tích tự nhiên là 214,85ha. Dân số là 693 nhân khẩu với 259 hộ chia làm 02 thôn: Thôn 1 và thôn 2;  Khu du lịch Tam Đảo nằm chủ yếu tại thôn 1 và 1 phần của thôn 2 (Khu nghỉ dưỡng cao cấp Belvedere Resort . Khu du lịch Tam Đảo nằm trên dãy núi Tam Đảo ở độ cao trên 900 m so với mực nước biển. Cách thủ đô Hà Nội khoảng 80 km bao gồm 50 km theo quốc lộ 2 và khoảng 24 km theo đường quốc lộ 2B trong đó có 13 km đường đèo. Khu du lịch Tam Đảo có phong cảnh núi non hùng vĩ, baoquát cả một vùng đồng bằng Bắc bộ rộng lớn. Khí hậu mát mẻ quan năm, nhiệt độ trung bình là 18oC – 25oC. Mùa hè từ tháng 5 đến tháng 9 nhiệt độ tại các tỉnh đồng bằng thường oi bức từ khoảng 27oC – 38oC thì Tam Đảo là nơi nghỉ mát lý tưởng với sự luân chuyển rõ rệt 4 mùa trong một ngày. Buổi sáng se se gió xuân, buổi trưa nóng ấm mùa hạ, buổi chiều lãng đãng heo may mùa thu, buổi tối lạnh giá của mùa đông. Khu du lịch nhỏ bé, xinh xắn với những con đường lên xuống ngoằn ngoèo, quanh co nho nhỏ, một dòng suối như vệt nước cắt ngang chảy suốt bốn mùa. Cái tên Tam Đảo có được là do ba ngọn núi cao Thạch Bàn (1.388m), Thiên Thị (1.375m) và Phù Nghĩa(1.400m) nhô lên trên biển mây. Đứng giữa đất trời, nhìn ba hòn đảo nhấp nhô lên trên đám sóng mây, ta mới hiểu vì sao vùng đất mát mẻ này có tên là Tam Đảo.Khu du lịch Tam Ðảo được người Pháp phát hiện và xây dựng từ những năm đầu thế kỷ 19. Ðến năm 1940, Tam Ðảo đã là một đô thị trên núi cao với 145 tòa nhà, biệt thự cao cấp, lộng lẫy; trong số này có tới 60 biệt thự với kiến trúc theo nhiều kiểu cách khác nhau. Nay những tòa biệt thự ngày xưa chỉ còn là phế tích trong hoang tàn, đổ nát, trơ ra những móng, tường, công trình ngầm nằm lẫn với cỏ cây, rêu phong, nắng mưa..."
            };
            ChiTietTour chiTiet4 = new ChiTietTour()
            {
                MaTour = tour4.MaTour,
                ChiTiet = "Quê thầy Phiêu có làng nghề code"
            };
            ChiTietTour chiTiet5 = new ChiTietTour()
            {
                MaTour = tour5.MaTour,
                ChiTiet = "Sa Pa là một điểm du lịch cách trung tâm thành phố Lào Cai khoảng hơn 30 km. Nằm ở độ cao trung bình 1500 – 1800 m so với mặt nước biển, Thị Trấn Sapa luôn chìm trong làn mây bồng bềnh, tạo nên một bức tranh huyền ảo đẹp đến kỳ lạ. Nơi đây, có thứ tài nguyên vô giá đó là khí hậu quanh năm trong lành mát mẻ, với nhiệt độ trung bình 15-18°C. Khách du lịch đến đây không chỉ để tận hưởng không khí trong lành, sự yên bình giản dị của một vùng đất phía Tây Bắc, mà Sapa còn là điểm đến để bạn chiêm ngưỡng những vẻ đẹp hoang sơ của những ruộng bậc thang, thác nước, những ngọn vúi hùng vĩ, khám phá những phong tục tập quán, nét đẹp văn hóa của các dân tộc trên núi như : H’Mong đen, Dzao đỏ, Tày, Dzáy…"
            };
            ChiTietTour chiTiet6 = new ChiTietTour()
            {
                MaTour = tour6.MaTour,
                ChiTiet = "Sa Pa là một điểm du lịch cách trung tâm thành phố Lào Cai khoảng hơn 30 km. Nằm ở độ cao trung bình 1500 – 1800 m so với mặt nước biển, Thị Trấn Sapa luôn chìm trong làn mây bồng bềnh, tạo nên một bức tranh huyền ảo đẹp đến kỳ lạ. Nơi đây, có thứ tài nguyên vô giá đó là khí hậu quanh năm trong lành mát mẻ, với nhiệt độ trung bình 15-18°C. Khách du lịch đến đây không chỉ để tận hưởng không khí trong lành, sự yên bình giản dị của một vùng đất phía Tây Bắc, mà Sapa còn là điểm đến để bạn chiêm ngưỡng những vẻ đẹp hoang sơ của những ruộng bậc thang, thác nước, những ngọn vúi hùng vĩ, khám phá những phong tục tập quán, nét đẹp văn hóa của các dân tộc trên núi như : H’Mong đen, Dzao đỏ, Tày, Dzáy…"

            };
            ChiTietTour chiTiet7 = new ChiTietTour()
            {
                MaTour = tour7.MaTour,
                ChiTiet = "Quê thầy Phiêu có làng nghề code"
            };
            ChiTietTour chiTiet8 = new ChiTietTour()
            {
                MaTour = tour8.MaTour,
                ChiTiet = "Thị trấn Tam Đảo thuộc huyện Tam Đảo có tổng diện tích tự nhiên là 214,85ha. Dân số là 693 nhân khẩu với 259 hộ chia làm 02 thôn: Thôn 1 và thôn 2;  Khu du lịch Tam Đảo nằm chủ yếu tại thôn 1 và 1 phần của thôn 2 (Khu nghỉ dưỡng cao cấp Belvedere Resort . Khu du lịch Tam Đảo nằm trên dãy núi Tam Đảo ở độ cao trên 900 m so với mực nước biển. Cách thủ đô Hà Nội khoảng 80 km bao gồm 50 km theo quốc lộ 2 và khoảng 24 km theo đường quốc lộ 2B trong đó có 13 km đường đèo. Khu du lịch Tam Đảo có phong cảnh núi non hùng vĩ, baoquát cả một vùng đồng bằng Bắc bộ rộng lớn. Khí hậu mát mẻ quan năm, nhiệt độ trung bình là 18oC – 25oC. Mùa hè từ tháng 5 đến tháng 9 nhiệt độ tại các tỉnh đồng bằng thường oi bức từ khoảng 27oC – 38oC thì Tam Đảo là nơi nghỉ mát lý tưởng với sự luân chuyển rõ rệt 4 mùa trong một ngày. Buổi sáng se se gió xuân, buổi trưa nóng ấm mùa hạ, buổi chiều lãng đãng heo may mùa thu, buổi tối lạnh giá của mùa đông. Khu du lịch nhỏ bé, xinh xắn với những con đường lên xuống ngoằn ngoèo, quanh co nho nhỏ, một dòng suối như vệt nước cắt ngang chảy suốt bốn mùa. Cái tên Tam Đảo có được là do ba ngọn núi cao Thạch Bàn (1.388m), Thiên Thị (1.375m) và Phù Nghĩa(1.400m) nhô lên trên biển mây. Đứng giữa đất trời, nhìn ba hòn đảo nhấp nhô lên trên đám sóng mây, ta mới hiểu vì sao vùng đất mát mẻ này có tên là Tam Đảo.Khu du lịch Tam Ðảo được người Pháp phát hiện và xây dựng từ những năm đầu thế kỷ 19. Ðến năm 1940, Tam Ðảo đã là một đô thị trên núi cao với 145 tòa nhà, biệt thự cao cấp, lộng lẫy; trong số này có tới 60 biệt thự với kiến trúc theo nhiều kiểu cách khác nhau. Nay những tòa biệt thự ngày xưa chỉ còn là phế tích trong hoang tàn, đổ nát, trơ ra những móng, tường, công trình ngầm nằm lẫn với cỏ cây, rêu phong, nắng mưa..."

            };

            context.ChiTietTours.AddOrUpdate(chiTiet,chiTiet2,chiTiet3,chiTiet4,chiTiet5,chiTiet6,chiTiet7,chiTiet8);
            // Them Cac Tinh Nang Cua Tour

            TinhNang tinhNangGanBien = new TinhNang()
            {
                MaTour = tour.MaTour,
                TenTinhNang = "Địa điểm",
                ThongTinTinhNang = "Gần biển"
            };


            TinhNang tinhNangPhongNgu = new TinhNang()
            {
                MaTour = tour.MaTour,
                TenTinhNang = "Số giường ngủ",
                ThongTinTinhNang = "4 giường"
            };
            context.TinhNangs.AddOrUpdate(tinhNangPhongNgu);
            context.TinhNangs.AddOrUpdate(tinhNangGanBien);
            Models.LichTrinh lich1 = new Models.LichTrinh()
            {
                MaTour = "TOUR3",
                Ngay = 1,
                MoTa = "HÀ NỘI – TAM ĐẢO (ĂN TRƯA, TỐI)",
                ChiTiet = "Xe và hướng dẫn viên của đón đoàn tại điểm hẹn trong phố Cổ hoặc Nhà hát lớn khởi hànhcho chuyến đi thăm quan Tam Đảo. Đến khu danh thắng Tây Thiên. Quý khách bách bộ đi thăm quan công trình Đại Bảo tháp Mandalamô phỏng theo phong cách của Ấ Độ. Sau đó thăm đền Thõng với cây đa chín cội. Quý khách bách bộ đi thăm quan Thiền Viện Trúc Lâm một trong những trường họcdành cho các tăng ni phật tử ở nước ta. Thăm quan Lầu Trống, Lầu Chuông, lớp học…Chỉnh phụclên được tầng cao nhất của Thiền Viện quý khách như được phóng tầm nhìn từ Thiên Đình xuống hạgiới một quang cảnh hiếm nơi nào có được.Chinh phục đỉnh Tây Thiên. Quý khách ngồi cáp treo ngắm cảnh núi rừng hùng vỹ củaTây Thiên, ngắm Thác Bạc từ trên cabin cáp treo. Lên tới đỉnh cáp, Quý khách tiếp tục chinh phụcđỉnh Tây Thiên với một loạt các công trình như: đền Cô Chín, đền Mẫu Địa, đền Thượng QuốcMẫu, Nhà thờ Tổ, một tổ Thiền Sư, chùa Tây Thiên rồi cuối cùng là chinh phục tới Bàn cờ Tiên ởtrên đỉnh Tây Thiên và nhiều các công trình khác (chưa bao gồm giá vé cáp treo Tây Thiên).Ăn trưa tại nhà hàng. Sau bữa trưa, Quý khách tiếp tục lên xe đi Tam Đảo.Đến Tam Đảo, Quý khách thăm quan chụp hình những cảnh đẹp xung quanh thị trấn như: Quảngtrường trung tâm Tam Đảo, khu phố sắc màu với lối kiến trúc độc đáo; Chụp hình với nhà thờ đá Tam Đảo với lối kiến trúc độc đáo được xây dựng trong những năm từ 1906 đến năm 1912, tọa lạc tại trung tâmthị trấn Tam Đảo, bên con đường dẫn lên đỉnh núi Thiên.Hướng dẫn viên dẫn Quý khách bách bộ đi thăm:Thác Bạc Tam Đảo: Đường dẫn xuống thác Bạc không quá dài, nhưng cheo leo, dựng đứng. Để đếnđây, bạn phải vượt qua những bậc tam cấp lót đá xanh. Con đường được mở trong núi, một bên lànúi, một bên là vực thẳm. Quán Gió Tam Đảo: Quán cafe vô cùng nổi tiếng tại đây để nhâm nhi ly cafe ấm nóng (chi phí tựtúc) và ngắm nhìn khung cảnh hùng vĩ, nên thơ của vùng đất này. Ăn tối. Buổi tối tự do nghỉ ngơi, tận hưởng không khí trong lành và mát mẻ tại thị trấn Tam Đảo về đêm. Nghỉ đêm tại thị trấn Tam Đảo.",
            };
            Models.LichTrinh lich11 = new Models.LichTrinh()
            {
                MaTour = "TOUR3",
                Ngay = 2,
                MoTa = "TAM ĐẢO – HÀ NỘI (Ăn: Sáng/ Trưa)",
                ChiTiet = "Ăn sáng tại khách sạn và nghỉ ngơi.- 08h30: Hướng dẫn viên đưa Quý khách đi chinh phục tháp Truyền hình Tam Đảo cao hơn 1400m so vớimực nước biển bằng việc leo bộ lên 1.400 bậc đá dẫn lên đỉnh Thiên Nhị. Cảm nhận cảm giác của một ngườichinh phục đỉnh cao, hít một hơi căng đầy lồng ngực bằng luồng không khí mát lạnh trong lành của TamĐảo, bỗng thấy lòng mình thanh thản. Tất cả những ưu phiền, sầu não, những lo toan hàng ngày dường nhưđã tan biến. Quý khách ghé thăm đền bà chúa Thượng Ngàn trên đường chinh phục ngọn tháp truyền hình.- 11h30: Quý khách trả phòng khách sạn (gửi đồ lễ tân), sau đó ăn trưa tại nhà hàng. Quý khách có thời giantự do mua sắm hoặc nhâm nhi ly cafe trước khi rời Tam Đảo về Hà Nội.- 15h30: Quý khách tập trung lên xe về Hà Nội.- 17h30: Về đến Hà Nội. Kết thúc chương trình. Hẹn gặp lại!",
            };
            Models.LichTrinh lich10 = new Models.LichTrinh()
            {
                MaTour = "TOUR1",
                Ngay = 1,
                MoTa = "Thăm quan học viện kỹ thuật quân sự",
                ChiTiet = "Học viện Kỹ thuật Quân sự, tên gọi khác: Trường Đại học Lê Quý Đôn, là một viện đại học kỹ thuật tổng hợp, đa ngành, đa lĩnh vực, trường đại học trọng điểm quốc gia Việt Nam, là đại học nghiên cứu- ứng dụng [1] và đào tạo kỹ sư, kỹ sư trưởng, công trình sư, nhà quản trị khoa học và công nghiệp trình độ Đại học, Thạc sĩ, Tiến sĩ trong các ngành khoa học kỹ thuật, công nghệ quân sự, công nghiệp quốc phòng và công nghệ cao phục vụ sự nghiệp hiện đại hoá quân đội và các ngành kinh tế quốc dân.[2]. Mục tiêu đến năm 2030, Học viện sẽ trở thành một trong 5 trường đại học hàng đầu của Việt Nam và nằm trong top 500 trường đại học hàng đầu thế giới [3].",
            };
            Models.LichTrinh lich2 = new Models.LichTrinh()
            {
                MaTour = "TOUR6",
                Ngay = 1,
                MoTa = "Hà Nội – Sapa",
                ChiTiet = "Xe Vietravel đón đoàn tại sân bay Nội Bài, khởi hành đi Sa Pa theo cung đường cao tốc hiện đại và dài nhất Việt Nam.Đến Sapa, nhận phòng nghỉ ngơi. Buổi chiều quý khách thăm  Bản Cát Cát - đẹp như một bức tranh giữa vùng phố cổ Sapa, nơi đây thu hút du khách bởi cầu treo, thác nước, guồng nước và những mảng màu hoa mê hoặc du khách khi lạc bước đến đây. Thăm những nếp nhà của người Mông, Dao, Giáy trong bản, du khách sẽ không khỏi ngỡ ngàng trước vẻ đẹp mộng mị của một trong những ngôi làng cổ đẹp nhất Sapa. Buổi tối Quý khách dạo phố, ngắm nhà thờ Đá Sapa, tự do thưởng thức đặc sản vùng cao như: thịt lợn cắp nách nướng, trứng nướng, rượu táo mèo, giao lưu với người dân tộc vùng cao. Nghỉ đêm tại Sapa."
            };
            Models.LichTrinh lich3 = new Models.LichTrinh()
            {
                MaTour = "TOUR6",
                Ngay = 2,
                MoTa = "SA PA - FANSIPAN (Ăn sáng, trưa, tối)",
                ChiTiet = "Xe đưa đoàn ra ga Sapa, Quý khách trải nghiệm đến khu du lịch Fansipan Legend bằng Tàu hỏa leo núi Mường Hoa hiện đại nhất Việt Nam với tổng chiều dài gần 2000m, thưởng ngoạn bức tranh phong cảnh đầy màu sắc của cánh rừng nguyên sinh, thung lũng Mường Hoa. Đến khu du lịch Fansipan Legend quý khách tự do tham quan: Tham quan tiểu cảnh Vườn tre, Chiêm bái chùa Trình – Bảo An Thiền Tư hoặc tự do mua sắm…Chinh phục đỉnh núi Fansipan với độ cao 3.143m hùng vĩ bằng cáp treo (chi phí tự túc) và cầu phúc lộc, bình an cho gia đình tại Bích Vân Thiền Tự hay tự thưởng cho mình ly ca cao nóng tại Café Du Soleil – Quán cà phê cao nhất Đông Dương. Xe đưa Quý khách về khách sạn tự do nghỉ ngơi. Nghỉ đêm tại Sapa.",
            };
            Models.LichTrinh lich4 = new Models.LichTrinh()
            {
                MaTour = "TOUR6",
                Ngay = 3,
                MoTa = "SaPa -Hà Nội",
                ChiTiet = "Quý khách ăn sáng và trả phòng khách sạn. Xe đưa Quý khách đi tham quan:Cửa khẩu biên giới Việt - Trung “Lào Cai- Hà Khẩu”Mua sắm tại chợ Cốc Lếu - Trung tâm thương mại lớn nhất, của thành phố nói riêng và Tỉnh Lào Cai nói chung. Nơi đây bày bán đa dạng đủ các loại mặt hàng từ thủ công mỹ nghệ, tranh nghệ thuật phong cảnh đến quần áo…sẽ là một điểm mua sắm tuyệt vời với du khách.Theo cung đường cao tốc trở về Hà Nội. Xe đưa Quý khách ra sân bay Nội Bài đáp chuyến bay về Tp.HCM. Chia tay Quý khách và kết thúc chương trình du lịch tại sân bay Tân Sơn Nhất.",
            };
            Models.LichTrinh lich5 = new Models.LichTrinh()
            {
                MaTour = "TOUR5",
                Ngay = 1,
                MoTa = "Hà Nội-Thanh Hóa(ăn trưa, tối)",
                ChiTiet = "6h00:Xe và hướng dẫn viên của công ty du lịch Linh Bình sẽ đón qúy khách tại điểm hẹn, bắt đầu khởi hành chuyến đi tham quan danh thắng nổi tiếng trong tỉnh.7h00:xe đưa quí khách đến Thành nhà Hồ Di sãn Văn hóa thế giới(45km) – di tích lịch sử còn lưu lại dấu tích cũ nhiều giá trị. Nghe hướng dẫn viên kể về những bí ẩn của ngôi thành này.8h00 : Rời Thành nhà Hồ xe đưa quí khách đến suối cá thần Cẩm Lương – nơi đã được đưa lên chương trình” Chuyện lạ Việt Nam”, nếu may mắn quí khách có cơ hội nhìn thấy cá chúa nặng tới 30kg. Tiếp đó quí khách leo núi thăm động Đăng, Động Tăng với những thạch nhũ phát sáng lạ mắt.11h30 rời suối cá, xe đưa quí khách trở lại thị trấn Cẩm Thủy dùng bữa trưa tại đây với những món ăn lạ miệng như: Dê núi, gà rừng, …",
            };
            Models.LichTrinh lich6 = new Models.LichTrinh()
            {
                MaTour = "TOUR5",
                Ngay = 2,
                MoTa = "Thanh Hóa - Hà Nội",
                ChiTiet = "Qúy khách tự do dạo chơi tham quan Thành Phố.Xe và HDV đưa Qúy khách khởi hành quý khách lên xe đi Sầm sơn , thăm Đền Độc Cước, hòn Trống Mái,Chùa Cô Tiên với những câu chuyện sự tích huyền thoại. Sau đó Qúy khách tự do dạo chơi tắm biển.Chiều tối: Xe đón Qúy khách Về khách sạn nghỉ ngơi.Kết Thúc Chương Trình tour du lịch thanh hóa 2 ngày 1 đêmHẹn gặp lại Qúy khách trong những lộ trình sau.",
            };
            Models.LichTrinh lich7 = new Models.LichTrinh()
            {
                MaTour = "TOUR2",
                Ngay = 1,
                MoTa = "HÀ NỘI – HẢI DƯƠNG (ĂN TRƯA, TỐI)",
                ChiTiet = "07h00: Xe và HDV đón quý khách tại điểm hẹn trong TP.Hà Nội khởi hành đi Hải Dương. Trên đường đi, quý khách sẽ được ngắm nhìn toàn cảnh vùng nông thôn đồng bằng Bắc Bộ Việt Nam. 09h00: Xe tới Hải Dương, quý khách dừng chân tham quan và thưởng thức nghệ thuật Múa rối nước của phường rối nước Hồng Phong – nơi những người nông dân cũng chính là những người nghệ sĩ chuyên nghiệp nhất. Quý khách sẽ được khám phá và tìm hiểu về loại hình nghệ thuật truyền thống và lâu đời của Việt Nam qua nhiều tích trò đặc sắc.10h30: Quý khách đến với Khu du lịch Sinh thái Đảo Cò – Chi Lăng Nam; đi thuyền khám phá vùng không gian xanh mát mang đậm nét thôn quê Bắc Bộ với những cánh cò cánh vạc; tiếng chim hót líu lo và thiên nhiên tươi đẹp, yên bình.12h00: Quý khách thưởng thức bữa trưa với các đặc sản cây nhà lá vườn, sau đó nghỉ ngơi.Buổi chiều: Quý khách tìm hiểu và khám phá cuộc sống thôn quê với các hoạt động dân dã hàng ngày của người dân địa phương:- Học cách cuốc đất trồng rau, quý khách sẽ được thực hành các công đoạn từ lúc làm đất, vun luống, trồng rau, tưới rau, chăm sóc và thu hoạch rau. Trải nghiệm cách làm cần câu, cách móc mồi câu và thư giãn với trò câu cá. Sản phẩm câu được sẽ được chế biến thành món ăn hấp dẫn cho bữa tối.",
            };
            Models.LichTrinh lich8 = new Models.LichTrinh()
            {
                MaTour = "TOUR2",
                Ngay = 1,
                MoTa = "HẢI DƯƠNG – HÀ NỘI (ĂN SÁNG, TRƯA)",
                ChiTiet = "05h00 – 05h30: Quý khách có thể dậy sớm cùng đón bình minh trên hồ An Dương với cảnh hàng nghìn con cò chao lượn, phủ trắng một vùng trời.06h00 – 07h00: Quý khách đạp xe khám phá phiên chợ quê thanh bình, thưởng thức bữa sáng với đặc sản bánh đa cá rô Hải Dương và mua các đặc sản địa phương về làm quà.08h30 – 09h30: Quý khách tham gia 1 số trò chơi tập thể vô cùng vui nhộn như “Vượt chướng ngại vật – đi qua Cầu khỉ”;  “Bịt mắt bắt vịt trên cạn”Sau đó, quý khách tự do khám phá và chụp hình lưu niệm tại Khu Du lịch Đảo Cò.11h00 - 12h00: Quý khách chuẩn bị bữa trưa với chiến lợi phẩm thu về từ trò chơi bắt vịt.12h30 – 13h30: Quý khách thưởng thức bữa trưa và nghỉ ngơi.14h00: Quý khách lên thuyền trở lại bến đò, chia tay đảo Cò15h00: Xe đón quý khách trở về Hà Nội. Trên đường về đoàn dừng chân mua đặc sản Hải Dương như bánh đậu xanh về làm quà cho gia đình.17h00: Xe đưa quý khách về đến Hà Nội, kết thúc chương trình. Xin kính chào và hẹn gặp lại quý khách trong những hành trình tiếp theo.",
            };
            context.LichTrinhs.AddOrUpdate(lich1, lich2, lich3, lich4, lich11, lich5, lich6, lich7, lich8, lich10);
        }
    }
}
