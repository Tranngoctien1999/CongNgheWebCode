namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNhanVienIdentity : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.IdentityTrace");
            AddColumn("dbo.IdentityTrace", "KhachHangIdentity", c => c.Int(nullable: false));
            AddColumn("dbo.IdentityTrace", "NhanVienIdentity", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.IdentityTrace", "Key");
            DropColumn("dbo.IdentityTrace", "TaiKhoanIdentity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IdentityTrace", "TaiKhoanIdentity", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.IdentityTrace");
            DropColumn("dbo.IdentityTrace", "NhanVienIdentity");
            DropColumn("dbo.IdentityTrace", "KhachHangIdentity");
            AddPrimaryKey("dbo.IdentityTrace", "TaiKhoanIdentity");
        }
    }
}
