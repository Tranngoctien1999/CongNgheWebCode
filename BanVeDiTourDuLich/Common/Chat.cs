using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using BanVeDiTourDuLich.Models;
using Microsoft.AspNet.SignalR;

namespace BanVeDiTourDuLich.Hubs
{
    public class Chat : Hub
    {
        public DataContext DataContext { get; set; }

        public Chat()
        {
            DataContext = new DataContext();
        }

        public void Hello()
        {
            Clients.All.hello();
        }

        public void CreateNewMessage(string maNhanVien , string maKhachHang, string noiDung)
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

        }
    }
}