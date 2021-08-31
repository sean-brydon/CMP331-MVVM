namespace CMP332.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inital : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Roles", new[] { "Name" });
            DropIndex("dbo.Users", new[] { "Username" });
            AlterColumn("dbo.Roles", "Name", c => c.String());
            AlterColumn("dbo.Users", "Username", c => c.String());
            DropColumn("dbo.Roles", "CreatedAt");
            DropColumn("dbo.Roles", "UpdatedAt");
            DropColumn("dbo.Users", "CreatedAt");
            DropColumn("dbo.Users", "UpdatedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UpdatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Users", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Roles", "UpdatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Roles", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Users", "Username", c => c.String(maxLength: 50));
            AlterColumn("dbo.Roles", "Name", c => c.String(maxLength: 50));
            CreateIndex("dbo.Users", "Username", unique: true);
            CreateIndex("dbo.Roles", "Name", unique: true);
        }
    }
}
