namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdentityOptionalCustomerTrace : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IdentityTrace", "KhachHangOptionalIdentity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IdentityTrace", "KhachHangOptionalIdentity");
        }
    }
}
