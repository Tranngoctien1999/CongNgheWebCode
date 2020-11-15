namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTinNhanNoiDungRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TinNhan", "NoiDung", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TinNhan", "NoiDung", c => c.String());
        }
    }
}
