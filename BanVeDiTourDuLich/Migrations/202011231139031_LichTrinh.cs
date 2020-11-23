namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LichTrinh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LichTrinh",
                c => new
                    {
                        MaTinhNang = c.Int(nullable: false, identity: true),
                        MaTour = c.String(),
                        Ngay = c.Int(nullable: false),
                        MoTa = c.String(),
                        ChiTiet = c.String(),
                    })
                .PrimaryKey(t => t.MaTinhNang);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LichTrinh");
        }
    }
}
