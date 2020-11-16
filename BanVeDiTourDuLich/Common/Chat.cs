using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Web;
using BanVeDiTourDuLich.Models;
using BanVeDiTourDuLich.Utilizer;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.SignalR;

namespace BanVeDiTourDuLich.Hubs
{
    public class Chat : Hub
    {
        public static List<ConnectionIdUser> connections = new List<ConnectionIdUser>();
        public static List<ConnectionIdUser> manager = new List<ConnectionIdUser>();
        public static List<ConnectionIdUser> user = new List<ConnectionIdUser>();
        public DataContext DataContext { get; set; }
        public Chat()
        {
            DataContext = new DataContext();
        }

        // Lưu lại tin nhắn gửi từ khách hàng tới nhân viên cụ thể
        public void SaveNewMessageFromKhachHangToNhanVien(string maNhanVien , string maKhachHang, string noiDung)
        {
            KhachHang khachHang = DataContext.KhachHangs.Find(maKhachHang);
            NhanVien nhanVien = DataContext.NhanViens.Find(maNhanVien);
            TinNhan tinNhan = new TinNhan()
            {
                MaKhachHang = khachHang.MaKhachHang,
                MaNhanVien = nhanVien.MaNhanVien,
                NoiDung = noiDung,
                ThoiGianGui = DateTime.Now
            };
            DataContext.TinNhans.Add(tinNhan);
            DataContext.SaveChanges();
        }

        // Lưu lại tin nhắn khách hàng gửi mà chưa có nhân viên trợ giúp
        public void SaveNewMessageFromKhachHang(string maKhachHang, string noiDung)
        {
            KhachHang khachHang = DataContext.KhachHangs.Find(maKhachHang);
            TinNhan tinNhan = new TinNhan()
            {
                MaKhachHang = khachHang.MaKhachHang,
                NoiDung = noiDung,
                ThoiGianGui = DateTime.Now
            };
            DataContext.TinNhans.Add(tinNhan);
            DataContext.SaveChanges();
        }
        //Gửi tin nhắn từ khách hàng tới đối tượng cụ thể
        public void SendMessageFromClientToManager(string maNhanVien,string maKhachHang)
        {
            TinNhan tinNhanCuoi = DataContext.TinNhans.Last(m => m.MaKhachHang == maKhachHang);
            ConnectionIdUser connection = connections.Find(c => c.MaTaiKhoan == maNhanVien);
            if (connection != null && tinNhanCuoi != null)
            {
                Clients.Client(connection.ConnectionId).addNewMessageToManager(maKhachHang,tinNhanCuoi.NoiDung , tinNhanCuoi.ThoiGianGui.ToString("d dddd-M-yyyy"));
            }
        }

        // Gửi tin nhắn tới tất cả các nhân viên ( hàm này cần chỉnh sao cho phù hợp với admin page)
        public void SendMassageFromClientToAllManager(string maKhachHang, string noiDung)
        {
            TinNhan tinNhanCuoi = DataContext.TinNhans.Where(m => m.MaKhachHang == maKhachHang)
                .OrderByDescending(c => c.ThoiGianGui).First();
            if (tinNhanCuoi != null)
            {
                Clients.Clients(manager.Select(m => m.ConnectionId).ToList()).addNewMessageToManager(maKhachHang,noiDung,tinNhanCuoi.ThoiGianGui.ToString("d dddd-M-yyyy"));
            }
        }

        public void SendMassageFromDbToCurrentBrower(string maKhachHang)
        {
            TinNhan tinNhanCuoi = DataContext.TinNhans.Where(m => m.MaKhachHang == maKhachHang)
                .OrderByDescending(c => c.ThoiGianGui).First();
            ConnectionIdUser connection = connections.Find(c => c.MaTaiKhoan == maKhachHang);
            if (connection != null && tinNhanCuoi != null)
            {
                Clients.Client(connection.ConnectionId).addNewMessageCurrentClientBrower(tinNhanCuoi.NoiDung , tinNhanCuoi.ThoiGianGui.ToString("d dddd-M-yyyy"));
            }
        }

        // Thêm connection Id khi người dùng hoặc nhân viên đăng nhập vào
        public void AddConnectionId(string maTaiKhoan, string connectionId)
        {
            TaiKhoan taiKhoan = DataContext.TaiKhoans.Find(maTaiKhoan);
            if (taiKhoan != null)
            {
                taiKhoan.ConnectionId = connectionId;
                DataContext.SaveChanges();
                if (taiKhoan.KhachHang != null)
                {
                    user.Add(new ConnectionIdUser(){MaTaiKhoan = taiKhoan.MaTaiKhoan , ConnectionId = taiKhoan.ConnectionId});
                }
                else
                {
                    manager.Add(new ConnectionIdUser(){MaTaiKhoan = taiKhoan.MaTaiKhoan , ConnectionId = taiKhoan.ConnectionId});
                }
                connections.Add(new ConnectionIdUser(){MaTaiKhoan = taiKhoan.MaTaiKhoan , ConnectionId = taiKhoan.ConnectionId});
            }
        }

        // Xóa connection Id khi người dùng ngắt kết nối
        public void DeleteConnectionId(string maTaiKhoan, string connectionId)
        {
            TaiKhoan taiKhoan = DataContext.TaiKhoans.Find(maTaiKhoan);
            if (taiKhoan != null)
            {
                DataContext.SaveChanges();
                ConnectionIdUser currentConnection = connections.First(connection => connection.MaTaiKhoan == taiKhoan.MaTaiKhoan);
                connections.Remove(currentConnection);
            }
        }
    }
}