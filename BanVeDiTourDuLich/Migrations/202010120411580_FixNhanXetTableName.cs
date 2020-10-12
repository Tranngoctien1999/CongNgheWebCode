namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixNhanXetTableName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.NhanXets", newName: "NhanXet");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.NhanXet", newName: "NhanXets");
        }
    }
}
