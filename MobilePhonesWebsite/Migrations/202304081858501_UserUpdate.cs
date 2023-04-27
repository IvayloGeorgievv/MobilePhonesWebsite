namespace MobilePhonesWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FirstName", c => c.String());
            AddColumn("dbo.Users", "LastName", c => c.String());
            DropColumn("dbo.Users", "Username");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Username", c => c.String());
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "FirstName");
        }
    }
}
