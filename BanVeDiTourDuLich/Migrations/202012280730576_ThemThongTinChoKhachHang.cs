namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThemThongTinChoKhachHang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KhachHang", "DiaChi", c => c.String());
            AddColumn("dbo.KhachHang", "GioiTinh", c => c.Boolean(nullable: false));
            AddColumn("dbo.KhachHang", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.KhachHang", "Email");
            DropColumn("dbo.KhachHang", "GioiTinh");
            DropColumn("dbo.KhachHang", "DiaChi");
        }
    }
}
