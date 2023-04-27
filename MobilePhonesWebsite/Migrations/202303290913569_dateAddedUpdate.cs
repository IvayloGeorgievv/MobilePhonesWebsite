namespace MobilePhonesWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateAddedUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductType = c.String(),
                        ProductId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ProductName = c.String(),
                        Quantity = c.Int(nullable: false),
                        Image = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.MobilePhones", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Case", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Protector", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Smartwatch", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.WiredHeadphones", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.WirelessHeadphones", "DateAdded", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WirelessHeadphones", "DateAdded");
            DropColumn("dbo.WiredHeadphones", "DateAdded");
            DropColumn("dbo.Smartwatch", "DateAdded");
            DropColumn("dbo.Protector", "DateAdded");
            DropColumn("dbo.Case", "DateAdded");
            DropColumn("dbo.MobilePhones", "DateAdded");
            DropTable("dbo.Cart");
        }
    }
}
