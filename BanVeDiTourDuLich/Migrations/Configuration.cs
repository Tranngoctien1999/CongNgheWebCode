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
                ThoigianDi = new DateTime(2020, 10, 29),
            };

            Tour tour2 = new Tour()
            {
                MaTour = "TOUR2",
                MaDiemDen = diaDiem2.MaDiaDiem,
                MaDiemDi = diaDiem6.MaDiaDiem,
                SoGio = 20,
                ThoigianDi = new DateTime(2020, 10, 29),
            };

            Tour tour3 = new Tour()
            {
                MaTour = "TOUR3",
                MaDiemDen = diaDiem3.MaDiaDiem,
                MaDiemDi = diaDiem6.MaDiaDiem,
                SoGio = 15,
                ThoigianDi = new DateTime(2020, 10, 29),
            };

            Tour tour4 = new Tour()
            {
                MaTour = "TOUR5",
                MaDiemDen = diaDiem4.MaDiaDiem,
                MaDiemDi = diaDiem6.MaDiaDiem,
                SoGio = 48,
                ThoigianDi = new DateTime(2020, 10, 29),
            };
            Tour tour5 = new Tour()
            {
                MaTour = "TOUR6",
                MaDiemDen = diaDiem8.MaDiaDiem,
                MaDiemDi = diaDiem6.MaDiaDiem,
                SoGio = 48,
                ThoigianDi = new DateTime(2020, 11, 29),
            };
            Tour tour6 = new Tour()
            {
                MaTour = "TOUR7",
                MaDiemDen = diaDiem8.MaDiaDiem,
                MaDiemDi = diaDiem7.MaDiaDiem,
                SoGio = 48,
                ThoigianDi = new DateTime(2020, 11, 18),
            };

            Tour tour7 = new Tour()
            {
                MaTour = "TOUR8",
                MaDiemDen = diaDiem4.MaDiaDiem,
                MaDiemDi = diaDiem7.MaDiaDiem,
                SoGio = 48,
                ThoigianDi = new DateTime(2020, 11, 19),
            };
            Tour tour8 = new Tour()
            {
                MaTour = "TOUR9",
                MaDiemDen = diaDiem3.MaDiaDiem,
                MaDiemDi = diaDiem7.MaDiaDiem,
                SoGio = 48,
                ThoigianDi = new DateTime(2020, 11, 19),
            };

            context.Tours.AddOrUpdate(tour , tour2 , tour3 , tour4,tour5,tour6,tour7,tour8);


            // Thêm loại vé
            LoaiVe loaiThuongGia = new LoaiVe()
            {
                MaLoaiVe = "VETHUONGGIATOUR1",
                Ten = "Vé thương gia",
                SoLuong = 10,
                MaTour = tour.MaTour,
                GiaTien = 2500000
            };
            context.LoaiVes.AddOrUpdate(loaiThuongGia);

            LoaiVe loaiThuong = new LoaiVe()
            {
                MaLoaiVe = "VETHUONGTOUR1",
                Ten = "Vé thường",
                SoLuong = 20,
                MaTour = tour.MaTour,
                GiaTien = 2000000
            };

            LoaiVe loaiThuongGia1 = new LoaiVe()
            {
                MaLoaiVe = "VETHUONGGIATOUR2",
                Ten = "Vé thương gia",
                SoLuong = 10,
                MaTour = tour2.MaTour,
                GiaTien = 1000000
            };
            context.LoaiVes.AddOrUpdate(loaiThuongGia1);

            LoaiVe loaiThuong1 = new LoaiVe()
            {
                MaLoaiVe = "VETHUONGTOUR2",
                Ten = "Vé thường",
                SoLuong = 20,
                MaTour = tour2.MaTour,
                GiaTien = 800000
            };
            context.LoaiVes.AddOrUpdate(loaiThuong1);
            LoaiVe loaiThuongGia2 = new LoaiVe()
            {
                MaLoaiVe = "VETHUONGGIATOUR3",
                Ten = "Vé thương gia",
                SoLuong = 10,
                MaTour = tour3.MaTour,
                GiaTien = 1000000
            };
            context.LoaiVes.AddOrUpdate(loaiThuongGia2);

            LoaiVe loaiThuong2 = new LoaiVe()
            {
                MaLoaiVe = "VETHUONGTOUR3",
                Ten = "Vé thường",
                SoLuong = 20,
                MaTour = tour3.MaTour,
                GiaTien = 800000
            };
            context.LoaiVes.AddOrUpdate(loaiThuong2);

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
                MaLoaiNhanVien = nhanVien.MaLoaiNhanVien,
                Luong = 200000,
                NgaySinh = new DateTime(1999 , 12 , 20),
                NgayVaoLam = DateTime.Now,
                DuongDanAnh = "/Content/images/Persions/NHANVIEN0.jpg"
            };

            context.NhanViens.AddOrUpdate(nhanVien1);
            context.NhanViens.AddOrUpdate(nhanVien2);
            //them tai khoan nhan vien
            TaiKhoan tk1 = new TaiKhoan()
            {
                MaTaiKhoan = "NHANVIEN01",
                TaiKhoanDangNhap = "user",
                MatKhau = "123456",
            };
            context.TaiKhoans.AddOrUpdate(tk1);

            TaiKhoan tk2 = new TaiKhoan()
            {
                MaTaiKhoan = "ADMIN",
                TaiKhoanDangNhap = "admin",
                MatKhau = "123456",
            };
            context.TaiKhoans.AddOrUpdate(tk1);
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
                MaKhachHang = "KH01",
                Ten = "Trần Ngọc Tiến",
                NgaySinh = new DateTime(1999, 10, 29),
                MaLoaiKhachHang = loaiKhachHang1.MaLoaiKhachHang,
                ThoiGianDangKi = new DateTime(2020, 10, 1),
                DuongDanAnh = "/Content/images/Persions/KH01.jpg"
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
                MaKhachHang = "KH02",
                Ten = "Phạm Xuân Tiến",
                NgaySinh = new DateTime(1999, 10, 29),
                MaLoaiKhachHang = loaiKhachHang1.MaLoaiKhachHang,
                ThoiGianDangKi = new DateTime(2020, 10, 1),
                DuongDanAnh = "/Content/images/Persions/KH02.jpg"
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
                MaLoaiVe = loaiThuongGia.MaLoaiVe,
            };

            Ve ve1 = new Ve()
            {
                MaVe = "Ve02",
                MaTour = tour2.MaTour,
                MaHoaDon = hoaDon2.MaHoaDon,
                MaLoaiVe = loaiThuongGia.MaLoaiVe,
                
            };
            context.Ves.AddOrUpdate(ve ,ve1);

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
        }
    }
}
