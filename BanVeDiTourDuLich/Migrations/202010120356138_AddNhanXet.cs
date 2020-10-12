namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNhanXet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NhanXets",
                c => new
                    {
                        MaNhanXet = c.Int(nullable: false, identity: true),
                        MaKhachHang = c.String(nullable: false, maxLength: 20, unicode: false),
                        MaTour = c.String(nullable: false, maxLength: 20, unicode: false),
                        NoiDung = c.String(),
                    })
                .PrimaryKey(t => t.MaNhanXet)
                .ForeignKey("dbo.KhachHang", t => t.MaKhachHang, cascadeDelete: true)
                .ForeignKey("dbo.Tour", t => t.MaTour, cascadeDelete: true)
                .Index(t => t.MaKhachHang)
                .Index(t => t.MaTour);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NhanXets", "MaTour", "dbo.Tour");
            DropForeignKey("dbo.NhanXets", "MaKhachHang", "dbo.KhachHang");
            DropIndex("dbo.NhanXets", new[] { "MaTour" });
            DropIndex("dbo.NhanXets", new[] { "MaKhachHang" });
            DropTable("dbo.NhanXets");
        }
    }
}
