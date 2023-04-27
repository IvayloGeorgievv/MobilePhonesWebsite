namespace MobilePhonesWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class smartwatchUpdate1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Smartwatch", "AdditionalInformation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Smartwatch", "AdditionalInformation", c => c.String());
        }
    }
}
