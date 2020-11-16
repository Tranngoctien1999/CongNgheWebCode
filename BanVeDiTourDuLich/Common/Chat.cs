using System;
using System.Collections.Generic;
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
        public DataContext DataContext { get; set; }
        public Chat()
        {
            DataContext = new DataContext();
        }

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

        public void SendMessageFromClientToManager(string maNhanVien,string maKhachHang, string noiDung)
        {
            ConnectionIdUser connection = connections.Find(c => c.MaTaiKhoan == maNhanVien);
            if (connection != null)
            {
                Clients.Client(connection.ConnectionId).addNewMessageToManagerBrower(maKhachHang,noiDung);
            }
        }

        public void AddConnectionId(string maTaiKhoan, string connectionId)
        {
            TaiKhoan taiKhoan = DataContext.TaiKhoans.Find(maTaiKhoan);
            if (taiKhoan != null)
            {
                taiKhoan.ConnectionId = connectionId;
                DataContext.SaveChanges();
                connections.Add(new ConnectionIdUser(){MaTaiKhoan = taiKhoan.MaTaiKhoan , ConnectionId = taiKhoan.ConnectionId});
            }
        }

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