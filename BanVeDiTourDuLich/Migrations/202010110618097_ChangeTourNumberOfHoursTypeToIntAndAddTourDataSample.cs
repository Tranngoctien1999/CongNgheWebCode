namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTourNumberOfHoursTypeToIntAndAddTourDataSample : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tour" , "SoGio");
            AddColumn("dbo.Tour", "SoGio" , e => e.Int() );
        }
        
        public override void Down()
        {
            
        }
    }
}
