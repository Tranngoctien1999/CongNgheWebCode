using System;

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
                TenDiaDiem = "Nhà",
                DuongDanAnh = "/Content/images/Destinations/MADIADIEM2.jpg"
            };

            context.DiaDiems.AddOrUpdate(diaDiem1, diaDiem2);


            // Thêm Tour
            Tour tour = new Tour()
            {
                MaTour = "TOUR1",
                MaDiemDen = diaDiem1.MaDiaDiem,
                MaDiemDi = diaDiem2.MaDiaDiem,
                SoGio = 10,
                ThoigianDi = new DateTime(2020, 10, 29, 8, 20, 10),
            };

            context.Tours.AddOrUpdate(tour);


            // Thêm loại vé
            LoaiVe loaiThuongGia = new LoaiVe()
            {
                MaLoaiVe = "VETHUONGGIATOUR1",
                Ten = "Vé thương gia",
                SoLuong = 10
            };
            context.LoaiVes.AddOrUpdate(loaiThuongGia);

            LoaiVe loaiThuong = new LoaiVe()
            {
                MaLoaiVe = "VETHUONGTOUR1",
                Ten = "Vé thường",
                SoLuong = 20
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
                NgayVaoLam = new DateTime(2020, 09, 10)
            };
            context.NhanViens.AddOrUpdate(nhanVien1);

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
                ThoiGianDangKi = new DateTime(2020, 10, 1)
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
            context.HoaDons.AddOrUpdate(hoaDon1);

            //// Thêm vé
            Ve ve = new Ve()
            {
                MaVe = "Ve01",
                MaTour = tour.MaTour,
                MaHoaDon = hoaDon1.MaHoaDon,
                MaLoaiVe = loaiThuongGia.MaLoaiVe,
                GiaTien = 200000
            };
            context.Ves.AddOrUpdate(ve);
        }
    }
}
