namespace MobilePhonesWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FirstAndLastName = c.String(),
                        PhoneNumber = c.String(),
                        DateOrdered = c.DateTime(nullable: false),
                        ProductsId = c.String(),
                        ProductsTypes = c.String(),
                        ProductsQuantities = c.String(),
                        OrderStatus = c.Int(nullable: false),
                        ShippingAddress = c.String(),
                        ShippingMethod = c.Int(nullable: false),
                        OrderTotalPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
        }
    }
}
