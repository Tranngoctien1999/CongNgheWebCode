namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangenameChiTietandTinhNang : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ChiTietTours", newName: "ChiTietTour");
            RenameTable(name: "dbo.TinhNangs", newName: "TinhNang");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TinhNang", newName: "TinhNangs");
            RenameTable(name: "dbo.ChiTietTour", newName: "ChiTietTours");
        }
    }
}
