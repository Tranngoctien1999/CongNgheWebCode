namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaiKhoan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaiKhoan",
                c => new
                    {
                        MaTaiKhoan = c.String(nullable: false, maxLength: 20, unicode: false),
                        TaiKhoanDangNhap = c.String(nullable: false, maxLength: 20),
                        MatKhau = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.MaTaiKhoan)
                .ForeignKey("dbo.KhachHang", t => t.MaTaiKhoan)
                .ForeignKey("dbo.NhanVien", t => t.MaTaiKhoan)
                .Index(t => t.MaTaiKhoan);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaiKhoan", "MaTaiKhoan", "dbo.NhanVien");
            DropForeignKey("dbo.TaiKhoan", "MaTaiKhoan", "dbo.KhachHang");
            DropIndex("dbo.TaiKhoan", new[] { "MaTaiKhoan" });
            DropTable("dbo.TaiKhoan");
        }
    }
}
