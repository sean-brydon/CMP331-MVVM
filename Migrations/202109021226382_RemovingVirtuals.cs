namespace CMP332.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingVirtuals : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Inspections", new[] { "Property_Id" });
            CreateIndex("dbo.Inspections", "property_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Inspections", new[] { "property_Id" });
            CreateIndex("dbo.Inspections", "Property_Id");
        }
    }
}
