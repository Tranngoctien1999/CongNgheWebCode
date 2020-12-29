namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThemSoDienThoaiChoKhachHang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KhachHang", "SoDienThoai", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.KhachHang", "SoDienThoai");
        }
    }
}
