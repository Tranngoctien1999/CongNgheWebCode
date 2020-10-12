namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeChiTietInLoaiKhachHangToNvarcharmax : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LoaiKhachHang", "ChiTiet", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LoaiKhachHang", "ChiTiet", c => c.String(maxLength: 10, fixedLength: true));
        }
    }
}
