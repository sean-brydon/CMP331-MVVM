namespace CMP332.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inspections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InspectionDate = c.DateTime(nullable: false),
                        InspectionType = c.String(),
                        InspectionCompleted = c.Boolean(nullable: false),
                        Property_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Properties", t => t.Property_Id)
                .Index(t => t.Property_Id);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        JobCompleted = c.Boolean(nullable: false),
                        Property_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Properties", t => t.Property_Id)
                .Index(t => t.Property_Id);
            
            CreateTable(
                "dbo.Lettors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        ContractStartDate = c.DateTime(nullable: false),
                        ContractEndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        NumberOfRooms = c.Int(nullable: false),
                        CurrentLettor_Id = c.Int(),
                        LettingAgent_Id = c.Int(),
                        MaintanceStaff_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lettors", t => t.CurrentLettor_Id)
                .ForeignKey("dbo.Users", t => t.LettingAgent_Id)
                .ForeignKey("dbo.Users", t => t.MaintanceStaff_Id)
                .Index(t => t.CurrentLettor_Id)
                .Index(t => t.LettingAgent_Id)
                .Index(t => t.MaintanceStaff_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Properties", "MaintanceStaff_Id", "dbo.Users");
            DropForeignKey("dbo.Jobs", "Property_Id", "dbo.Properties");
            DropForeignKey("dbo.Properties", "LettingAgent_Id", "dbo.Users");
            DropForeignKey("dbo.Inspections", "Property_Id", "dbo.Properties");
            DropForeignKey("dbo.Properties", "CurrentLettor_Id", "dbo.Lettors");
            DropIndex("dbo.Properties", new[] { "MaintanceStaff_Id" });
            DropIndex("dbo.Properties", new[] { "LettingAgent_Id" });
            DropIndex("dbo.Properties", new[] { "CurrentLettor_Id" });
            DropIndex("dbo.Jobs", new[] { "Property_Id" });
            DropIndex("dbo.Inspections", new[] { "Property_Id" });
            DropTable("dbo.Properties");
            DropTable("dbo.Lettors");
            DropTable("dbo.Jobs");
            DropTable("dbo.Inspections");
        }
    }
}
