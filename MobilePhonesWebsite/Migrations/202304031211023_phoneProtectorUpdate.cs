namespace MobilePhonesWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phoneProtectorUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Case", "FitFor", c => c.String());
            DropColumn("dbo.Protector", "Colour");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Protector", "Colour", c => c.Int(nullable: false));
            AlterColumn("dbo.Case", "FitFor", c => c.String(nullable: false));
        }
    }
}
