using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        // giữa nhân viên và khách hàng

        // Lưu lại tin nhắn gửi giữa khách hàng và nhân viên cụ thể
        public async Task SaveNewMessageBetweenNhanVienAndClient(string maNhanVien , string maKhachHang, string noiDung)
        {
            KhachHang khachHang = await DataContext.KhachHangs.FindAsync(maKhachHang);
            NhanVien nhanVien = await DataContext.NhanViens.FindAsync(maNhanVien);
            TinNhan tinNhan = new TinNhan()
            {
                MaKhachHang = khachHang.MaKhachHang,
                MaNhanVien = nhanVien.MaNhanVien,
                NoiDung = noiDung,
                ThoiGianGui = DateTime.Now
            };
            DataContext.TinNhans.Add(tinNhan);
            await DataContext.SaveChangesAsync();
        }



        // Khách hàng gửi tin

        // Lưu lại tin nhắn khách hàng gửi mà chưa có nhân viên trợ giúp
        public async Task SaveNewMessageFromKhachHang(string maKhachHang, string noiDung)
        {
            KhachHang khachHang = await DataContext.KhachHangs.FindAsync(maKhachHang);
            TinNhan tinNhan = new TinNhan()
            {
                MaKhachHang = khachHang.MaKhachHang,
                NoiDung = noiDung,
                ThoiGianGui = DateTime.Now
            };
            DataContext.TinNhans.Add(tinNhan);
            await DataContext.SaveChangesAsync();
        }
        //Gửi tin nhắn từ khách hàng tới đối tượng cụ thể
        public async Task SendMessageFromClientToManager(string maNhanVien,string maKhachHang)
        {
            TinNhan tinNhanCuoi = await DataContext.TinNhans.Where(m => m.MaKhachHang == maKhachHang)
                .OrderByDescending(c => c.ThoiGianGui).FirstAsync();
            ConnectionIdUser connection = connections.Find(c => c.MaTaiKhoan == maNhanVien);
            if (connection != null && tinNhanCuoi != null)
            {
                Clients.Client(connection.ConnectionId).addNewMessageToManager(maKhachHang,tinNhanCuoi.NoiDung , tinNhanCuoi.ThoiGianGui.ToString("d dddd-M-yyyy"));
            }
        }

        // Gửi tin nhắn tới tất cả các nhân viên ( hàm này cần chỉnh sao cho phù hợp với admin page)
        public async Task SendMassageFromClientToAllManager(string maKhachHang, string noiDung)
        {
            TinNhan tinNhanCuoi = await DataContext.TinNhans.Where(m => m.MaKhachHang == maKhachHang)
                .OrderByDescending(c => c.ThoiGianGui).FirstAsync();
            if (tinNhanCuoi != null)
            {
                Clients.Clients(manager.Select(m => m.ConnectionId).ToList()).addNewMessageToManager(maKhachHang,noiDung,tinNhanCuoi.ThoiGianGui.ToString("d dddd-M-yyyy"));
                Clients.Clients(manager.Select(m => m.ConnectionId).ToList()).addMessageInformationInCard(maKhachHang,noiDung,tinNhanCuoi.ThoiGianGui.ToString("d dddd-M-yyyy"));
            }
        }

        public async Task SendMassageFromDbToCurrentClientBrower(string maTaiKhoan)
        {
            TinNhan tinNhanCuoi = await DataContext.TinNhans.Where(m => m.MaKhachHang == maTaiKhoan)
                .OrderByDescending(c => c.ThoiGianGui).FirstAsync();
            ConnectionIdUser connection = connections.Find(c => c.MaTaiKhoan == maTaiKhoan);
            if (connection != null && tinNhanCuoi != null)
            {
                Clients.Client(connection.ConnectionId).sendMassageFromDbToCurrentClientBrower(tinNhanCuoi.NoiDung , tinNhanCuoi.ThoiGianGui.ToString("d dddd-M-yyyy"));
            }
        }

        // Nhân viên gửi tin

        // Gửi tin nhắn cho khách hàng
        public async Task SendMassageFromManagerToClient(string maNhanVien, string maKhachHang, string noiDung)
        {
            TinNhan tinNhanCuoi = await DataContext.TinNhans.Where(m => m.MaNhanVien == maNhanVien)
                .OrderByDescending(c => c.ThoiGianGui).FirstAsync();
            ConnectionIdUser connection = user.Find(c => c.MaTaiKhoan == maKhachHang);
            if (connection != null && tinNhanCuoi != null)
            {
                Clients.Client(connection.ConnectionId).addNewMessageToClient(maNhanVien,tinNhanCuoi.NoiDung , tinNhanCuoi.ThoiGianGui.ToString("d dddd-M-yyyy"));
            }
        }
        
        // Gửi tin nhắn cho người nhân viên hiện tại
        public async Task SendMassageFromDbToCurrentManagerBrower(string maTaiKhoan)
        {
            TinNhan tinNhanCuoi = await DataContext.TinNhans.Where(m => m.MaNhanVien == maTaiKhoan)
                .OrderByDescending(c => c.ThoiGianGui).FirstAsync();
            ConnectionIdUser connection = connections.Find(c => c.MaTaiKhoan == maTaiKhoan);
            if (connection != null && tinNhanCuoi != null)
            {
                Clients.Client(connection.ConnectionId).addNewMessageCurrentManagerBrower(tinNhanCuoi.NoiDung , tinNhanCuoi.ThoiGianGui.ToString("d dddd-M-yyyy"));
            }
        }

        // 

        // Thêm connection Id khi người dùng hoặc nhân viên đăng nhập vào
        public async Task AddConnectionId(string maTaiKhoan, string connectionId)
        {
            TaiKhoan taiKhoan = await DataContext.TaiKhoans.FindAsync(maTaiKhoan);
            if (taiKhoan != null)
            {
                taiKhoan.ConnectionId = connectionId;
                await DataContext.SaveChangesAsync();
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
        public async Task DeleteConnectionId(string maTaiKhoan, string connectionId)
        {
            TaiKhoan taiKhoan = await DataContext.TaiKhoans.FindAsync(maTaiKhoan);
            if (taiKhoan != null)
            {
                await DataContext.SaveChangesAsync();
                ConnectionIdUser currentConnection = connections.First(connection => connection.MaTaiKhoan == taiKhoan.MaTaiKhoan);
                connections.Remove(currentConnection);
            }
        }
    }
}