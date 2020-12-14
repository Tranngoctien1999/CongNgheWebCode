namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixMaDiemDenDi : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Tour", name: "MaDiemDi", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Tour", name: "MaDiemDen", newName: "MaDiemDi");
            RenameColumn(table: "dbo.Tour", name: "__mig_tmp__0", newName: "MaDiemDen");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Tour", name: "MaDiemDen", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Tour", name: "MaDiemDi", newName: "MaDiemDen");
            RenameColumn(table: "dbo.Tour", name: "__mig_tmp__0", newName: "MaDiemDi");
        }
    }
}
