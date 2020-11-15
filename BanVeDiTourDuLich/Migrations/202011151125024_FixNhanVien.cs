namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixNhanVien : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TaiKhoan", new[] { "KhachHang_MaKhachHang" });
            DropIndex("dbo.TaiKhoan", new[] { "NhanVien_MaNhanVien" });
            RenameColumn(table: "dbo.KhachHang", name: "KhachHang_MaKhachHang", newName: "TaiKhoan_MaTaiKhoan");
            RenameColumn(table: "dbo.NhanVien", name: "NhanVien_MaNhanVien", newName: "TaiKhoan_MaTaiKhoan");
            CreateIndex("dbo.KhachHang", "TaiKhoan_MaTaiKhoan");
            CreateIndex("dbo.NhanVien", "TaiKhoan_MaTaiKhoan");
            DropColumn("dbo.TaiKhoan", "KhachHang_MaKhachHang");
            DropColumn("dbo.TaiKhoan", "NhanVien_MaNhanVien");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaiKhoan", "NhanVien_MaNhanVien", c => c.String(maxLength: 20, unicode: false));
            AddColumn("dbo.TaiKhoan", "KhachHang_MaKhachHang", c => c.String(maxLength: 20, unicode: false));
            DropIndex("dbo.NhanVien", new[] { "TaiKhoan_MaTaiKhoan" });
            DropIndex("dbo.KhachHang", new[] { "TaiKhoan_MaTaiKhoan" });
            RenameColumn(table: "dbo.NhanVien", name: "TaiKhoan_MaTaiKhoan", newName: "NhanVien_MaNhanVien");
            RenameColumn(table: "dbo.KhachHang", name: "TaiKhoan_MaTaiKhoan", newName: "KhachHang_MaKhachHang");
            CreateIndex("dbo.TaiKhoan", "NhanVien_MaNhanVien");
            CreateIndex("dbo.TaiKhoan", "KhachHang_MaKhachHang");
        }
    }
}
