﻿namespace BanVeDiTourDuLich.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConnectionId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaiKhoan", "ConnectionId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaiKhoan", "ConnectionId");
        }
    }
}
