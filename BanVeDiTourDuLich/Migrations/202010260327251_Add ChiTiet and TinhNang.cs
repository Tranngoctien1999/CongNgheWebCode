namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChiTietandTinhNang : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChiTietTours",
                c => new
                    {
                        MaTour = c.String(nullable: false, maxLength: 20, unicode: false),
                        ChiTiet = c.String(),
                    })
                .PrimaryKey(t => t.MaTour)
                .ForeignKey("dbo.Tour", t => t.MaTour)
                .Index(t => t.MaTour);
            
            CreateTable(
                "dbo.TinhNangs",
                c => new
                    {
                        MaTinhNang = c.Int(nullable: false, identity: true),
                        MaTour = c.String(nullable: false, maxLength: 20, unicode: false),
                        TenTinhNang = c.String(),
                        ThongTinTinhNang = c.String(),
                    })
                .PrimaryKey(t => t.MaTinhNang)
                .ForeignKey("dbo.ChiTietTours", t => t.MaTour, cascadeDelete: true)
                .Index(t => t.MaTour);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChiTietTours", "MaTour", "dbo.Tour");
            DropForeignKey("dbo.TinhNangs", "MaTour", "dbo.ChiTietTours");
            DropIndex("dbo.TinhNangs", new[] { "MaTour" });
            DropIndex("dbo.ChiTietTours", new[] { "MaTour" });
            DropTable("dbo.TinhNangs");
            DropTable("dbo.ChiTietTours");
        }
    }
}
