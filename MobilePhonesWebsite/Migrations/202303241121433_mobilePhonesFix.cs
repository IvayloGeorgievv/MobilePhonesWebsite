namespace MobilePhonesWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mobilePhonesFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MobilePhones", "Image1", c => c.String());
            AddColumn("dbo.MobilePhones", "Image2", c => c.String());
            DropColumn("dbo.MobilePhones", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MobilePhones", "Image", c => c.String());
            DropColumn("dbo.MobilePhones", "Image2");
            DropColumn("dbo.MobilePhones", "Image1");
        }
    }
}
