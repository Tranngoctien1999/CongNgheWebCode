using BanVeDiTourDuLich.Models;
using BanVeDiTourDuLich.Utilizer;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task SaveNewMessageBetweenNhanVienAndClient(string maNhanVien, string maKhachHang, string noiDung)
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

        


        #region Khách hàng gửi tin

        // Qua trinh khach hang gui tin toi tat ca nhan vien hien tai

        public async Task SendNewMessageFromKhachHang(string maKhachHang, string noiDung)
        {
            //Luu lai tin nhan moi tu khach hang
            await SaveNewMessageFromKhachHang(maKhachHang, noiDung);
            // Gui tin nhan toi tat cac nhan vien hien co
            await SendMessageFromClientToAllManager(maKhachHang, noiDung);
            // Gui tin nhan lai tu database cho nhan vien hien tai
            await SendMessageFromDbToCurrentClientBrower(maKhachHang);
        }

        // Quá trình khách hàng gửi tin nhắn tới nhân viên cụ thể

        public async Task SendNewMessageFromSpecificClientToManager(string maKhachHang,string maNhanVien , string noiDung)
        {
            await SaveNewMessageBetweenNhanVienAndClient(maNhanVien, maKhachHang, noiDung);
            await SendMessageFromClientToManager(maNhanVien, maKhachHang);
            await SendMessageFromDbToCurrentClientBrower(maKhachHang);
        }

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
        public async Task SendMessageFromClientToManager(string maNhanVien, string maKhachHang)
        {
            TinNhan tinNhanCuoi = await DataContext.TinNhans.Where(m => m.MaKhachHang == maKhachHang)
                .OrderByDescending(c => c.ThoiGianGui).FirstAsync();
            ConnectionIdUser connection = connections.Find(c => c.MaTaiKhoan == maNhanVien);
            if (connection != null && tinNhanCuoi != null)
            {
                KhachHang client = await DataContext.KhachHangs.Where(khachHang => khachHang.MaKhachHang == maKhachHang).FirstAsync();
                Clients.Client(connection.ConnectionId).addNewMessage(maKhachHang, tinNhanCuoi.NoiDung, tinNhanCuoi.ThoiGianGui.ToString("d dddd-M-yyyy") , client.Ten);
            }
        }

        // Gửi tin nhắn tới tất cả các nhân viên ( hàm này cần chỉnh sao cho phù hợp với admin page)
        public async Task SendMessageFromClientToAllManager(string maKhachHang, string noiDung)
        {
            TinNhan tinNhanCuoi = await DataContext.TinNhans.Where(m => m.MaKhachHang == maKhachHang)
                .OrderByDescending(c => c.ThoiGianGui).FirstAsync();
            if (tinNhanCuoi != null)
            {
                // Gửi trong trang thái đăng nhập tại user
                Clients.Clients(manager.Select(m => m.ConnectionId).ToList()).addNewMessageToManager(maKhachHang, noiDung, tinNhanCuoi.ThoiGianGui.ToString("d dddd-M-yyyy"));
                // Gửi trong trnang thái đăng nhập với manager
                KhachHang client = await DataContext.KhachHangs.Where(khachHang => khachHang.MaKhachHang == maKhachHang).FirstAsync();
                Clients.Clients(manager.Select(m => m.ConnectionId).ToList()).addNewMessage(maKhachHang, noiDung, tinNhanCuoi.ThoiGianGui.ToString("d dddd-M-yyyy") ,client.Ten);
            }
        }

        public async Task SendMessageFromDbToCurrentClientBrower(string maTaiKhoan)
        {
            TinNhan tinNhanCuoi = await DataContext.TinNhans.Where(m => m.MaKhachHang == maTaiKhoan)
                .OrderByDescending(c => c.ThoiGianGui).FirstAsync();
            ConnectionIdUser connection = connections.Find(c => c.MaTaiKhoan == maTaiKhoan);
            if (connection != null && tinNhanCuoi != null)
            {
                Clients.Client(connection.ConnectionId).sendMassageFromDbToCurrentClientBrower(tinNhanCuoi.NoiDung, tinNhanCuoi.ThoiGianGui.ToString("d dddd-M-yyyy"));
            }

        }

        #endregion


        #region Nhân viên gửi tin

        // Gửi tin nhắn từ một nhân viên cụ thể đến khách hàng cụ thể
        public async Task SendMessageFromSpecificManagerToSpecificClient(string maNhanVien, string maKhachHang,
            string noiDung)
        {
            await SaveNewMessageBetweenNhanVienAndClient(maNhanVien, maKhachHang, noiDung);
            await SendMassageFromManagerToClient(maNhanVien, maKhachHang);
            await SendMassageFromDbToCurrentManagerBrower(maNhanVien , maKhachHang);
        }

        // Gửi tin nhắn cho khách hàng
        public async Task SendMassageFromManagerToClient(string maNhanVien, string maKhachHang)
        {
            TinNhan tinNhanCuoi = await DataContext.TinNhans.Where(m => m.MaNhanVien == maNhanVien)
                .OrderByDescending(c => c.ThoiGianGui).FirstAsync();
            ConnectionIdUser connection = user.Find(c => c.MaTaiKhoan == maKhachHang);
            if (connection != null && tinNhanCuoi != null)
            {
                Clients.Client(connection.ConnectionId).addNewMessageToClient(maNhanVien, tinNhanCuoi.NoiDung, tinNhanCuoi.ThoiGianGui.ToString("d dddd-M-yyyy"));
            }
        }

        // Gửi tin nhắn cho người nhân viên hiện tại
        public async Task SendMassageFromDbToCurrentManagerBrower(string maTaiKhoan, string maKhachHang)
        {
            TinNhan tinNhanCuoi = await DataContext.TinNhans.Where(m => m.MaNhanVien == maTaiKhoan)
                .OrderByDescending(c => c.ThoiGianGui).FirstAsync();
            ConnectionIdUser connection = connections.Find(c => c.MaTaiKhoan == maTaiKhoan);
            if (connection != null && tinNhanCuoi != null)
            {
                NhanVien nhanVien = await DataContext.NhanViens.Where(n => n.MaNhanVien == maTaiKhoan).FirstAsync();
                Clients.Client(connection.ConnectionId).addNewMessageCurrentManagerBrower(maKhachHang,tinNhanCuoi.NoiDung, tinNhanCuoi.ThoiGianGui.ToString("d dddd-M-yyyy") ,nhanVien.Ten);
            }
        }

        #endregion


        #region Người dùng đăng nhập

        // Thêm connection Id khi người dùng hoặc nhân viên đăng nhập vào
        public async Task AddConnectionId(string maTaiKhoan, string connectionId)
        {
            TaiKhoan taiKhoan = await DataContext.TaiKhoans.FindAsync(maTaiKhoan);
            int count = connections.Where(c => c.MaTaiKhoan == maTaiKhoan).Count();
            if (taiKhoan != null)
            {
                taiKhoan.ConnectionId = connectionId;
                await DataContext.SaveChangesAsync();
                if (count > 0)
                {
                    if (taiKhoan.KhachHang != null)
                    {
                        user.Where(u => u.MaTaiKhoan == maTaiKhoan).First().ConnectionId = connectionId;
                    }
                    else
                    {
                        manager.Where(m => m.MaTaiKhoan == maTaiKhoan).First().ConnectionId = connectionId;
                    }
                    connections.Where(c => c.MaTaiKhoan == maTaiKhoan).First().ConnectionId = connectionId;
                }
                else
                {
                    if (taiKhoan.KhachHang != null)
                    {
                        user.Add(new ConnectionIdUser() { MaTaiKhoan = taiKhoan.MaTaiKhoan, ConnectionId = taiKhoan.ConnectionId });
                    }
                    else
                    {
                        manager.Add(new ConnectionIdUser() { MaTaiKhoan = taiKhoan.MaTaiKhoan, ConnectionId = taiKhoan.ConnectionId });
                    }
                    connections.Add(new ConnectionIdUser() { MaTaiKhoan = taiKhoan.MaTaiKhoan, ConnectionId = taiKhoan.ConnectionId }); 
                }
            }
        }

        // Xóa connection Id khi người dùng ngắt kết nối
        public async Task DeleteConnectionId(string maTaiKhoan)
        {
            TaiKhoan taiKhoan = await DataContext.TaiKhoans.FindAsync(maTaiKhoan);
            if (taiKhoan != null)
            {
                ConnectionIdUser currentConnection = connections.First(connection => connection.MaTaiKhoan == taiKhoan.MaTaiKhoan);
                if (currentConnection != null)
                {
                    connections.Remove(currentConnection);
                }
                ConnectionIdUser currentUser = user.First(user => user.MaTaiKhoan == taiKhoan.MaTaiKhoan);
                if (currentUser != null)
                {
                    user.Remove(currentUser);
                }

                ConnectionIdUser currentManager = manager.First(manager => manager.MaTaiKhoan == taiKhoan.MaTaiKhoan);
                if (currentUser != null)
                {
                    manager.Remove(currentManager);
                }
            }
        }

        #endregion
        
    }
}