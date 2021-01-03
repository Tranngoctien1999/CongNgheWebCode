namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDuongDanAnhvaoLichTrinh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LichTrinh", "DuongDanAnh", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LichTrinh", "DuongDanAnh");
        }
    }
}
