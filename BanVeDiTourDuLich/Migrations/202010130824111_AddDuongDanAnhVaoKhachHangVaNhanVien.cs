namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDuongDanAnhVaoKhachHangVaNhanVien : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KhachHang", "DuongDanAnh", c => c.String());
            AddColumn("dbo.NhanVien", "DuongDanAnh", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NhanVien", "DuongDanAnh");
            DropColumn("dbo.KhachHang", "DuongDanAnh");
        }
    }
}
