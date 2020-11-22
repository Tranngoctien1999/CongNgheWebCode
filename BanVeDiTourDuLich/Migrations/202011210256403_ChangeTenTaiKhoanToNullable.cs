namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTenTaiKhoanToNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaiKhoan", "TaiKhoanDangNhap", c => c.String(nullable:true ,maxLength: 20, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaiKhoan", "TaiKhoanDangNhap", c => c.String(nullable: false, maxLength: 20, unicode: false));
        }
    }
}
