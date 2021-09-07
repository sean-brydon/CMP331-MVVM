namespace CMP332.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingInvoices : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AmountOwed = c.Single(nullable: false),
                        PaymentDueDate = c.DateTime(nullable: false),
                        Lettor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lettors", t => t.Lettor_Id)
                .Index(t => t.Lettor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "Lettor_Id", "dbo.Lettors");
            DropIndex("dbo.Invoices", new[] { "Lettor_Id" });
            DropTable("dbo.Invoices");
        }
    }
}
