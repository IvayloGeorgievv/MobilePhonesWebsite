namespace MobilePhonesWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userUpdateAgain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Username", c => c.String());
            DropColumn("dbo.Users", "FirstName");
            DropColumn("dbo.Users", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "LastName", c => c.String());
            AddColumn("dbo.Users", "FirstName", c => c.String());
            DropColumn("dbo.Users", "Username");
        }
    }
}
