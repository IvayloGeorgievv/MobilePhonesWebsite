namespace MobilePhonesWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cartUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cart", "PriceForOne", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cart", "PriceForOne");
        }
    }
}
