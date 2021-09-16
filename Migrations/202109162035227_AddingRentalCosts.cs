namespace CMP332.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingRentalCosts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Properties", "CostPerMonth", c => c.Single());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Properties", "CostPerMonth");
        }
    }
}
