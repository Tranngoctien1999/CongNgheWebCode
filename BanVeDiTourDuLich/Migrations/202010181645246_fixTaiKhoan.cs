namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixTaiKhoan : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaiKhoan", "MaTaiKhoan", "dbo.KhachHang");
            DropForeignKey("dbo.TaiKhoan", "MaTaiKhoan", "dbo.NhanVien");
            DropIndex("dbo.TaiKhoan", new[] { "MaTaiKhoan" });
            DropPrimaryKey("dbo.TaiKhoan");
            AddColumn("dbo.TaiKhoan", "KhachHang_MaKhachHang", c => c.String(maxLength: 20, unicode: false));
            AddColumn("dbo.TaiKhoan", "NhanVien_MaNhanVien", c => c.String(maxLength: 20, unicode: false));
            AlterColumn("dbo.TaiKhoan", "MaTaiKhoan", c => c.String(nullable: false, maxLength: 20));
            AddPrimaryKey("dbo.TaiKhoan", "MaTaiKhoan");
            CreateIndex("dbo.TaiKhoan", "KhachHang_MaKhachHang");
            CreateIndex("dbo.TaiKhoan", "NhanVien_MaNhanVien");
            AddForeignKey("dbo.TaiKhoan", "KhachHang_MaKhachHang", "dbo.KhachHang", "MaKhachHang");
            AddForeignKey("dbo.TaiKhoan", "NhanVien_MaNhanVien", "dbo.NhanVien", "MaNhanVien");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaiKhoan", "NhanVien_MaNhanVien", "dbo.NhanVien");
            DropForeignKey("dbo.TaiKhoan", "KhachHang_MaKhachHang", "dbo.KhachHang");
            DropIndex("dbo.TaiKhoan", new[] { "NhanVien_MaNhanVien" });
            DropIndex("dbo.TaiKhoan", new[] { "KhachHang_MaKhachHang" });
            DropPrimaryKey("dbo.TaiKhoan");
            AlterColumn("dbo.TaiKhoan", "MaTaiKhoan", c => c.String(nullable: false, maxLength: 20, unicode: false));
            DropColumn("dbo.TaiKhoan", "NhanVien_MaNhanVien");
            DropColumn("dbo.TaiKhoan", "KhachHang_MaKhachHang");
            AddPrimaryKey("dbo.TaiKhoan", "MaTaiKhoan");
            CreateIndex("dbo.TaiKhoan", "MaTaiKhoan");
            AddForeignKey("dbo.TaiKhoan", "MaTaiKhoan", "dbo.NhanVien", "MaNhanVien");
            AddForeignKey("dbo.TaiKhoan", "MaTaiKhoan", "dbo.KhachHang", "MaKhachHang");
        }
    }
}
