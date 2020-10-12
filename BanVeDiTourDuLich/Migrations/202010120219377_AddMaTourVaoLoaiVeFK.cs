namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMaTourVaoLoaiVeFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoaiVe", "MaTour", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AddColumn("dbo.LoaiVe", "GiaTien", c => c.Double(nullable: false));
            CreateIndex("dbo.LoaiVe", "MaTour");
            AddForeignKey("dbo.LoaiVe", "MaTour", "dbo.Tour", "MaTour", cascadeDelete: true);
            DropColumn("dbo.Ve", "GiaTien");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ve", "GiaTien", c => c.Double(nullable: false));
            DropForeignKey("dbo.LoaiVe", "MaTour", "dbo.Tour");
            DropIndex("dbo.LoaiVe", new[] { "MaTour" });
            DropColumn("dbo.LoaiVe", "GiaTien");
            DropColumn("dbo.LoaiVe", "MaTour");
        }
    }
}
