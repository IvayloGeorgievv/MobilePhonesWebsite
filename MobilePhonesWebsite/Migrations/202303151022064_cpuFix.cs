namespace MobilePhonesWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cpuFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MobilePhoneProcessor", "ProcessorType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MobilePhoneProcessor", "ProcessorType", c => c.String());
        }
    }
}
