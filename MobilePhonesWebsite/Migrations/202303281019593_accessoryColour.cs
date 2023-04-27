namespace MobilePhonesWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accessoryColour : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Case", "Colour", c => c.Int(nullable: false));
            AddColumn("dbo.Protector", "Colour", c => c.Int(nullable: false));
            AddColumn("dbo.Smartwatch", "Colour", c => c.Int(nullable: false));
            AddColumn("dbo.WiredHeadphones", "Colour", c => c.Int(nullable: false));
            AddColumn("dbo.WirelessHeadphones", "Colour", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WirelessHeadphones", "Colour");
            DropColumn("dbo.WiredHeadphones", "Colour");
            DropColumn("dbo.Smartwatch", "Colour");
            DropColumn("dbo.Protector", "Colour");
            DropColumn("dbo.Case", "Colour");
        }
    }
}
