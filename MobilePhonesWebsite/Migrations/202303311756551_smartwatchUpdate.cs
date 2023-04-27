namespace MobilePhonesWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class smartwatchUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Smartwatch", "DisplaySize", c => c.Double(nullable: false));
            AddColumn("dbo.Smartwatch", "Weight", c => c.Double(nullable: false));
            AddColumn("dbo.Smartwatch", "AdditionalInformation", c => c.String());
            DropColumn("dbo.Smartwatch", "Size");
            DropColumn("dbo.Smartwatch", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Smartwatch", "Description", c => c.String());
            AddColumn("dbo.Smartwatch", "Size", c => c.String());
            DropColumn("dbo.Smartwatch", "AdditionalInformation");
            DropColumn("dbo.Smartwatch", "Weight");
            DropColumn("dbo.Smartwatch", "DisplaySize");
        }
    }
}
