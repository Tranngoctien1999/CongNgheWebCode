namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentityTraceTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IdentityTrace",
                c => new
                    {
                        TaiKhoanIdentity = c.Int(nullable: false),
                        Key = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.TaiKhoanIdentity);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.IdentityTrace");
        }
    }
}
