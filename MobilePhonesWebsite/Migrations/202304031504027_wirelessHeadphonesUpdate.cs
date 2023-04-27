namespace MobilePhonesWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wirelessHeadphonesUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WirelessHeadphones", "BatteryLifeWithCase", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WirelessHeadphones", "BatteryLifeWithCase");
        }
    }
}
