namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSoLuongToLoaiVe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoaiVe", "SoLuong", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoaiVe", "SoLuong");
        }
    }
}
