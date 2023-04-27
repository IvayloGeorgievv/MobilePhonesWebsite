namespace MobilePhonesWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class likedProductsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Liked",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductType = c.String(),
                        ProductId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ProductName = c.String(),
                        Image = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Liked");
        }
    }
}
