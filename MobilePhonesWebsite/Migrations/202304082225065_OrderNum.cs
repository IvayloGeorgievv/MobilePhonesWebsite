namespace MobilePhonesWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderNum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderNum", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "OrderNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "OrderNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "OrderNum");
        }
    }
}
