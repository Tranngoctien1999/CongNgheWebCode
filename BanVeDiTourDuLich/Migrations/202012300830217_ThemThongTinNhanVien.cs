namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThemThongTinNhanVien : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NhanVien", "DiaChi", c => c.String());
            AddColumn("dbo.NhanVien", "GioiTinh", c => c.Boolean(nullable: false));
            AddColumn("dbo.NhanVien", "Email", c => c.String());
            AddColumn("dbo.NhanVien", "SoDienThoai", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NhanVien", "SoDienThoai");
            DropColumn("dbo.NhanVien", "Email");
            DropColumn("dbo.NhanVien", "GioiTinh");
            DropColumn("dbo.NhanVien", "DiaChi");
        }
    }
}
