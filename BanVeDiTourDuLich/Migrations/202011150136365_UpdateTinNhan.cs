namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTinNhan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TinNhan",
                c => new
                    {
                        MaTinNhan = c.Int(nullable: false, identity: true),
                        MaNhanVien = c.String(nullable: false, maxLength: 20, unicode: false),
                        MaKhachHang = c.String(nullable: false, maxLength: 20, unicode: false),
                        NoiDung = c.String(),
                    })
                .PrimaryKey(t => t.MaTinNhan)
                .ForeignKey("dbo.NhanVien", t => t.MaNhanVien, cascadeDelete: true)
                .ForeignKey("dbo.KhachHang", t => t.MaKhachHang, cascadeDelete: true)
                .Index(t => t.MaNhanVien)
                .Index(t => t.MaKhachHang);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TinNhan", "MaKhachHang", "dbo.KhachHang");
            DropForeignKey("dbo.TinNhan", "MaNhanVien", "dbo.NhanVien");
            DropIndex("dbo.TinNhan", new[] { "MaKhachHang" });
            DropIndex("dbo.TinNhan", new[] { "MaNhanVien" });
            DropTable("dbo.TinNhan");
        }
    }
}
