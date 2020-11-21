namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixTinNhan : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TinNhan", "MaKhachHang", "dbo.KhachHang");
            DropForeignKey("dbo.TinNhan", "MaNhanVien", "dbo.NhanVien");
            DropIndex("dbo.TinNhan", new[] { "MaNhanVien" });
            DropIndex("dbo.TinNhan", new[] { "MaKhachHang" });
            AlterColumn("dbo.TinNhan", "MaNhanVien", c => c.String(maxLength: 20, unicode: false));
            AlterColumn("dbo.TinNhan", "MaKhachHang", c => c.String(maxLength: 20, unicode: false));
            CreateIndex("dbo.TinNhan", "MaNhanVien");
            CreateIndex("dbo.TinNhan", "MaKhachHang");
            AddForeignKey("dbo.TinNhan", "MaKhachHang", "dbo.KhachHang", "MaKhachHang");
            AddForeignKey("dbo.TinNhan", "MaNhanVien", "dbo.NhanVien", "MaNhanVien");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TinNhan", "MaNhanVien", "dbo.NhanVien");
            DropForeignKey("dbo.TinNhan", "MaKhachHang", "dbo.KhachHang");
            DropIndex("dbo.TinNhan", new[] { "MaKhachHang" });
            DropIndex("dbo.TinNhan", new[] { "MaNhanVien" });
            AlterColumn("dbo.TinNhan", "MaKhachHang", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.TinNhan", "MaNhanVien", c => c.String(nullable: false, maxLength: 20, unicode: false));
            CreateIndex("dbo.TinNhan", "MaKhachHang");
            CreateIndex("dbo.TinNhan", "MaNhanVien");
            AddForeignKey("dbo.TinNhan", "MaNhanVien", "dbo.NhanVien", "MaNhanVien", cascadeDelete: true);
            AddForeignKey("dbo.TinNhan", "MaKhachHang", "dbo.KhachHang", "MaKhachHang", cascadeDelete: true);
        }
    }
}
