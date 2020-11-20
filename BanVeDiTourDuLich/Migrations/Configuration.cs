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
            context.NhanViens.AddOrUpdate(nhanVien1);
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
                MaTaiKhoan = "KH01",
                TaiKhoanDangNhap = "khachhang",
                MatKhau = "123456",
            };
            context.TaiKhoans.AddOrUpdate(tk2);
            // Thêm Loại khách hàng

            LoaiKhachHang loaiKhachHang1 = new LoaiKhachHang()
            {
                MaLoaiKhachHang = "KHACHHANGVANG",
                Ten = "Khách hàng loại vàng",
                ChiTiet = "Khách hàng có số tiền tích lũy đạt 2.000.0000 VNĐ trở lên"
            };

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

            context.ChiTietTours.AddOrUpdate(chiTiet);
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
