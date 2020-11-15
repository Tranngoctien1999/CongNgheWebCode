namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTinNhanThemThoiGianGui : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TinNhan", "ThoiGianGui", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TinNhan", "ThoiGianGui");
        }
    }
}
