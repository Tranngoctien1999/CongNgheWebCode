namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncreseLengthConstrainOfAccountAndClient : DbMigration
    {
        public override void Up()
        {
            Sql("IF object_id(N'[dbo].[FK_HoaDon_KhachHang]', N'F') IS NOT NULL\r\n    ALTER TABLE [dbo].[HoaDon] DROP CONSTRAINT [FK_HoaDon_KhachHang]");
            Sql("IF object_id(N'[dbo].[FK_dbo.NhanXets_dbo.KhachHang_MaKhachHang]', N'F') IS NOT NULL\r\n    ALTER TABLE [dbo].[NhanXet] DROP CONSTRAINT [FK_dbo.NhanXets_dbo.KhachHang_MaKhachHang]");
            DropForeignKey("dbo.TinNhan", "MaKhachHang", "dbo.KhachHang");
            Sql("IF object_id(N'[dbo].[FK_HoaDon_NhanVien]', N'F') IS NOT NULL\r\n    ALTER TABLE [dbo].[HoaDon] DROP CONSTRAINT [FK_HoaDon_NhanVien]");
            DropForeignKey("dbo.TinNhan", "MaNhanVien", "dbo.NhanVien");
            DropForeignKey("dbo.TaiKhoan", "MaTaiKhoan", "dbo.KhachHang");
            DropForeignKey("dbo.TaiKhoan", "MaTaiKhoan", "dbo.NhanVien");
            DropIndex("dbo.NhanXet", new[] { "MaKhachHang" });
            DropIndex("dbo.TaiKhoan", new[] { "MaTaiKhoan" });
            DropIndex("dbo.HoaDon", new[] { "MaKhachHang" });
            DropIndex("dbo.HoaDon", new[] { "MaNhanVien" });
            DropIndex("dbo.TinNhan", new[] { "MaNhanVien" });
            DropIndex("dbo.TinNhan", new[] { "MaKhachHang" });
            DropPrimaryKey("KhachHang");
            DropPrimaryKey("NhanVien");
            AlterColumn("dbo.NhanXet", "MaKhachHang", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.KhachHang", "MaKhachHang", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.HoaDon", "MaKhachHang", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.HoaDon", "MaNhanVien", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.NhanVien", "MaNhanVien", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.TaiKhoan", "MaTaiKhoan", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.TinNhan", "MaNhanVien", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.TinNhan", "MaKhachHang", c => c.String(maxLength: 50, unicode: false));
            AddPrimaryKey("dbo.KhachHang", "MaKhachHang");
            AddPrimaryKey("dbo.NhanVien", "MaNhanVien");
            AddPrimaryKey("dbo.TaiKhoan", "MaTaiKhoan");
            CreateIndex("dbo.NhanXet", "MaKhachHang");
            CreateIndex("dbo.TaiKhoan", "MaTaiKhoan");
            CreateIndex("dbo.HoaDon", "MaKhachHang");
            CreateIndex("dbo.HoaDon", "MaNhanVien");
            CreateIndex("dbo.TinNhan", "MaNhanVien");
            CreateIndex("dbo.TinNhan", "MaKhachHang");
            AddForeignKey("dbo.HoaDon", "MaKhachHang", "dbo.KhachHang", "MaKhachHang");
            AddForeignKey("dbo.NhanXet", "MaKhachHang", "dbo.KhachHang", "MaKhachHang", cascadeDelete: true);
            AddForeignKey("dbo.TinNhan", "MaKhachHang", "dbo.KhachHang", "MaKhachHang");
            AddForeignKey("dbo.HoaDon", "MaNhanVien", "dbo.NhanVien", "MaNhanVien");
            AddForeignKey("dbo.TinNhan", "MaNhanVien", "dbo.NhanVien", "MaNhanVien");
            AddForeignKey("dbo.TaiKhoan", "MaTaiKhoan", "dbo.NhanVien", "MaNhanVien");
            Sql("ALTER TABLE dbo.TaiKhoan NOCHECK CONSTRAINT [FK_dbo.TaiKhoan_dbo.NhanVien_MaTaiKhoan]");
            AddForeignKey("dbo.TaiKhoan", "MaTaiKhoan", "dbo.KhachHang", "MaKhachHang");
            Sql("ALTER TABLE dbo.TaiKhoan NOCHECK CONSTRAINT [FK_dbo.TaiKhoan_dbo.KhachHang_MaTaiKhoan]");
        }
        
        public override void Down()
        {
            // This down method is not true : restore database to update
            DropForeignKey("dbo.TaiKhoan", "MaTaiKhoan", "dbo.NhanVien");
            DropForeignKey("dbo.TaiKhoan", "MaTaiKhoan", "dbo.KhachHang");
            DropForeignKey("dbo.TinNhan", "MaNhanVien", "dbo.NhanVien");
            DropForeignKey("dbo.HoaDon", "MaNhanVien", "dbo.NhanVien");
            DropForeignKey("dbo.TinNhan", "MaKhachHang", "dbo.KhachHang");
            DropForeignKey("dbo.NhanXet", "MaKhachHang", "dbo.KhachHang");
            DropForeignKey("dbo.HoaDon", "MaKhachHang", "dbo.KhachHang");
            DropIndex("dbo.TinNhan", new[] { "MaKhachHang" });
            DropIndex("dbo.TinNhan", new[] { "MaNhanVien" });
            DropIndex("dbo.TaiKhoan", new[] { "MaTaiKhoan" });
            DropIndex("dbo.HoaDon", new[] { "MaNhanVien" });
            DropIndex("dbo.HoaDon", new[] { "MaKhachHang" });
            DropIndex("dbo.NhanXet", new[] { "MaKhachHang" });
            DropPrimaryKey("dbo.TaiKhoan");
            DropPrimaryKey("dbo.NhanVien");
            DropPrimaryKey("dbo.KhachHang");
            AlterColumn("dbo.TinNhan", "MaKhachHang", c => c.String(maxLength: 20, unicode: false));
            AlterColumn("dbo.TinNhan", "MaNhanVien", c => c.String(maxLength: 20, unicode: false));
            AlterColumn("dbo.TaiKhoan", "MaTaiKhoan", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.NhanVien", "MaNhanVien", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.HoaDon", "MaNhanVien", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.HoaDon", "MaKhachHang", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.KhachHang", "MaKhachHang", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.NhanXet", "MaKhachHang", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AddPrimaryKey("dbo.NhanVien", "MaNhanVien");
            AddPrimaryKey("dbo.KhachHang", "MaKhachHang");
            CreateIndex("dbo.TinNhan", "MaKhachHang");
            CreateIndex("dbo.TinNhan", "MaNhanVien");
            CreateIndex("dbo.HoaDon", "MaNhanVien");
            CreateIndex("dbo.HoaDon", "MaKhachHang");
            CreateIndex("dbo.TaiKhoan", "MaTaiKhoan");
            CreateIndex("dbo.NhanXet", "MaKhachHang");
            AddForeignKey("dbo.TaiKhoan", "MaTaiKhoan", "dbo.KhachHang", "MaKhachHang");
            AddForeignKey("dbo.TaiKhoan", "MaTaiKhoan", "dbo.NhanVien", "MaNhanVien");
            AddForeignKey("dbo.TinNhan", "MaNhanVien", "dbo.NhanVien", "MaNhanVien");
            AddForeignKey("dbo.HoaDon", "MaNhanVien", "dbo.NhanVien", "MaNhanVien");
            AddForeignKey("dbo.TinNhan", "MaKhachHang", "dbo.KhachHang", "MaKhachHang");
            AddForeignKey("dbo.NhanXet", "MaKhachHang", "dbo.KhachHang", "MaKhachHang", cascadeDelete: true);
            AddForeignKey("dbo.HoaDon", "MaKhachHang", "dbo.KhachHang", "MaKhachHang");
        }
    }
}
