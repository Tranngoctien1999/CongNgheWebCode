using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BanVeDiTourDuLich.Utilizer
{
    public static class TimKiemTongHop
    {
        public static DataContext context = new DataContext();


        public static async Task<List<SearchAdminInformation>> Search(string tuKhoa)
        {
            List<KhachHang> khachHangs = await context.KhachHangs.ToListAsync();
            List<SearchAdminInformation> listCustomer = khachHangs.Where(khachHang => khachHang.CheckInformationCustomer(tuKhoa))
                .Select(khachHang => new SearchAdminInformation()
                {
                    Id = khachHang.MaKhachHang,
                    TenThongTin = khachHang.Ten,
                    ViTriTimKiem = ViTriStrings[(int)ViTri.KhachHang],
                    DuongDanAnhThongTin = khachHang.DuongDanAnh,
                    DuongDan = "/Admin/QuanLyNguoiDungSingle/"
                }).ToList();

            List<NhanVien> nhanViens = await context.NhanViens.ToListAsync();
            List<SearchAdminInformation> listManager = nhanViens.Where(nhanVien => nhanVien.CheckInformationManager(tuKhoa))
                .Select(nhanVien => new SearchAdminInformation()
                    {
                        TenThongTin = nhanVien.Ten,
                        ViTriTimKiem = ViTriStrings[(int)ViTri.NhanVien],
                        DuongDanAnhThongTin = nhanVien.DuongDanAnh,
                        Id = nhanVien.MaNhanVien,
                        DuongDan = "/Admin/QuanLyNhanVienSingle/"
                }
                    ).ToList();


            List<DiaDiem> diaDiems = await context.DiaDiems.ToListAsync();
            List<SearchAdminInformation> listDiemDen = diaDiems
                .Where(diaDiem => diaDiem.CheckInformationDiaDiem(tuKhoa))
                .Select(diaDiem => new SearchAdminInformation()
                {
                    ViTriTimKiem = ViTriStrings[(int)ViTri.DiemDen],
                    DuongDanAnhThongTin = diaDiem.DuongDanAnh,
                    TenThongTin = diaDiem.TenDiaDiem,
                    Id = diaDiem.MaDiaDiem,
                    DuongDan = "/Admin/QuanLyDiaDiemSingle/"
                }).ToList();

            List<Tour> tours = await context.Tours.ToListAsync();
            List<SearchAdminInformation> listTour = tours.Where(tour => tour.CheckInformationTour(tuKhoa))
                .Select(tour => new SearchAdminInformation()
                {
                    DuongDanAnhThongTin = tour.DiaDiemDen.DuongDanAnh,
                    TenThongTin = tour.MaTour,
                    Id = tour.MaTour,
                    ViTriTimKiem = ViTriStrings[(int) ViTri.Tour],
                    DuongDan = "/Admin/QuanLyTourSingle/"
                }).ToList();

            return listTour.Union(listCustomer).Union(listCustomer).Union(listDiemDen).Union(listManager).ToList();
        }

        public static bool CheckInformationCustomer(this KhachHang khacHang , string tuKhoa )
        {
            if (!string.IsNullOrEmpty(khacHang.MaKhachHang))
            {
                if (khacHang.MaKhachHang.ToLower().Contains(tuKhoa.ToLower()))
                {
                    return true;
                }
            }

            if (!string.IsNullOrEmpty(khacHang.DiaChi))
            {
                if (khacHang.DiaChi.ToLower().Contains(tuKhoa.ToLower()))
                {
                    return true;
                }
            }

            if (!string.IsNullOrEmpty(khacHang.Ten))
            {
                if (khacHang.Ten.ToLower().Contains(tuKhoa.ToLower()))
                {
                    return true;
                }
            }

            if (!string.IsNullOrEmpty(khacHang.Email))
            {
                if (khacHang.Email.ToLower().Contains(tuKhoa.ToLower()))
                {
                    return true;
                }
            }

            DateTime timeTry;
            if (DateTime.TryParse(tuKhoa, out timeTry))
            {
                if (khacHang.NgaySinh.Date == timeTry)
                {
                    return true;
                }
            }

            if (DateTime.TryParse(tuKhoa, out timeTry))
            {
                if (khacHang.ThoiGianDangKi.Date == timeTry)
                {
                    return true;
                }
            }

            return false;
        }


        public static bool CheckInformationManager(this NhanVien nhanVien, string tuKhoa)
        {
            if (!string.IsNullOrEmpty(nhanVien.MaNhanVien))
            {
                if (nhanVien.MaNhanVien.ToLower().Contains(tuKhoa.ToLower()))
                {
                    return true;
                }
            }

            if (!string.IsNullOrEmpty(nhanVien.Ten))
            {
                if (nhanVien.Ten.ToLower().Contains(tuKhoa.ToLower()))
                {
                    return true;
                }
            }

            if (!string.IsNullOrEmpty(nhanVien.DiaChi))
            {
                if (nhanVien.DiaChi.ToLower().Contains(tuKhoa.ToLower()))
                {
                    return true;
                }
            }

            if (!string.IsNullOrEmpty(nhanVien.Email))
            {
                if (nhanVien.Email.ToLower().Contains(tuKhoa.ToLower()))
                {
                    return true;
                }
            }

            DateTime timeTry;
            if (DateTime.TryParse(tuKhoa, out timeTry))
            {
                if (nhanVien.NgaySinh.Date == timeTry)
                {
                    return true;
                }
            }

            if (DateTime.TryParse(tuKhoa, out timeTry))
            {
                if (nhanVien.NgayVaoLam.Date == timeTry)
                {
                    return true;
                }
            }

            return false;

        }

        public static bool CheckInformationDiaDiem(this DiaDiem diaDiem, string tuKhoa)
        {
            if (!string.IsNullOrEmpty(diaDiem.TenDiaDiem))
            {
                if (diaDiem.TenDiaDiem.ToLower().Contains(tuKhoa.ToLower()))
                {
                    return true;
                }
            }

            if (!string.IsNullOrEmpty(diaDiem.DiaChi))
            {
                if (diaDiem.DiaChi.ToLower().Contains(tuKhoa.ToLower()))
                {
                    return true;
                }
            }

            if (!string.IsNullOrEmpty(diaDiem.MaDiaDiem))
            {
                if (diaDiem.MaDiaDiem.ToLower().Contains(tuKhoa.ToLower()))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CheckInformationTour(this Tour tour, string tuKhoa)
        {
            if (!string.IsNullOrEmpty(tour.MaTour))
            {
                if (tour.MaTour.ToLower().Contains(tuKhoa.ToLower()))
                {
                    return true;
                }
            }

            if (!string.IsNullOrEmpty(tour.DiaDiemDen.TenDiaDiem))
            {
                if (tour.DiaDiemDen.TenDiaDiem.ToLower().Contains(tuKhoa.ToLower()))
                {
                    return true;
                }
            }

            if (!string.IsNullOrEmpty(tour.DiaDiemDi.TenDiaDiem))
            {
                if (tour.DiaDiemDi.TenDiaDiem.ToLower().Contains(tuKhoa.ToLower()))
                {
                    return true;
                }
            }

            return false;
        }


        public enum ViTri
        {
            NhanVien,
            KhachHang,
            DiemDen,
            Tour
        }

        public static string[] ViTriStrings = new string[]{"Nhân Viên" , "Khách Hàng", "Điểm Đến" , "Tour"};
    }
}