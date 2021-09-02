namespace CMP332.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EasierInspectionQuery : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Inspections", "InspectionDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Inspections", "InspectionDate", c => c.DateTime(nullable: false));
        }
    }
}
