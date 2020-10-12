namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDiaDiemAddDuongDanAnhProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DiaDiem", "DuongDanAnh", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DiaDiem", "DuongDanAnh");
        }
    }
}
