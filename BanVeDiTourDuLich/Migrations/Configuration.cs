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
                DiaChi = "Hạ Long , Quảng Ninh",
                TenDiaDiem = "Vịnh Hạ Long",
                DuongDanAnh = "/Content/images/Destinations/halong.jpg"
            };

            context.DiaDiems.AddOrUpdate(diaDiem1, diaDiem2 , diaDiem3 , diaDiem4 , diaDiem5);


            // Thêm Tour
            Tour tour = new Tour()
            {
                MaTour = "TOUR1",
                MaDiemDen = diaDiem1.MaDiaDiem,
                MaDiemDi = diaDiem2.MaDiaDiem,
                SoGio = 10,
                ThoigianDi = new DateTime(2020, 10, 29),
            };

            Tour tour2 = new Tour()
            {
                MaTour = "TOUR2",
                MaDiemDen = diaDiem2.MaDiaDiem,
                MaDiemDi = diaDiem3.MaDiaDiem,
                SoGio = 20,
                ThoigianDi = new DateTime(2020, 10, 29),
            };

            Tour tour3 = new Tour()
            {
                MaTour = "TOUR3",
                MaDiemDen = diaDiem3.MaDiaDiem,
                MaDiemDi = diaDiem4.MaDiaDiem,
                SoGio = 15,
                ThoigianDi = new DateTime(2020, 10, 29),
            };

            Tour tour4 = new Tour()
            {
                MaTour = "TOUR5",
                MaDiemDen = diaDiem4.MaDiaDiem,
                MaDiemDi = diaDiem5.MaDiaDiem,
                SoGio = 48,
                ThoigianDi = new DateTime(2020, 10, 29),
            };

            context.Tours.AddOrUpdate(tour , tour2 , tour3 , tour4);


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
            context.LoaiVes.AddOrUpdate(loaiThuong);

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
