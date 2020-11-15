namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixTaiKhoan : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TaiKhoan");
            AlterColumn("dbo.TaiKhoan", "MaTaiKhoan", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.TaiKhoan", "TaiKhoanDangNhap", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.TaiKhoan", "MatKhau", c => c.String(maxLength: 20, unicode: false));
            DropForeignKey("dbo.TaiKhoan", "KhachHang_MaKhachHang", "dbo.KhachHang");
            DropForeignKey("dbo.TaiKhoan", "NhanVien_MaNhanVien", "dbo.NhanVien");
            DropIndex("dbo.TaiKhoan", new[] { "KhachHang_MaKhachHang" });
            DropIndex("dbo.TaiKhoan", new[] { "NhanVien_MaNhanVien" });
            AddForeignKey("dbo.TaiKhoan", "MaTaiKhoan", "dbo.NhanVien", "MaNhanVien", cascadeDelete: true);
            AddForeignKey("dbo.TaiKhoan", "MaTaiKhoan", "dbo.KhachHang", "MaKhachHang", cascadeDelete: true);
            DropColumn("dbo.TaiKhoan", "KhachHang_MaKhachHang");
            DropColumn("dbo.TaiKhoan", "NhanVien_MaNhanVien");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaiKhoan", "NhanVien_MaNhanVien", c => c.String(maxLength: 20, unicode: false));
            AddColumn("dbo.TaiKhoan", "KhachHang_MaKhachHang", c => c.String(maxLength: 20, unicode: false));
            DropForeignKey("dbo.TinNhan", "MaNhanVien", "dbo.NhanVien");
            DropForeignKey("dbo.HoaDon", "MaNhanVien", "dbo.NhanVien");
            CreateIndex("dbo.TaiKhoan", "NhanVien_MaNhanVien");
            CreateIndex("dbo.TaiKhoan", "KhachHang_MaKhachHang");
            AddForeignKey("dbo.TaiKhoan", "NhanVien_MaNhanVien", "dbo.NhanVien", "MaNhanVien");
            AddForeignKey("dbo.TaiKhoan", "KhachHang_MaKhachHang", "dbo.KhachHang", "MaKhachHang");
            AlterColumn("dbo.TaiKhoan", "MatKhau", c => c.String(maxLength: 20));
            AlterColumn("dbo.TaiKhoan", "TaiKhoanDangNhap", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.TaiKhoan", "MaTaiKhoan", c => c.String(nullable: false, maxLength: 20));
            AddPrimaryKey("dbo.TaiKhoan" , "MaTaiKhoan");
        }
    }
}
