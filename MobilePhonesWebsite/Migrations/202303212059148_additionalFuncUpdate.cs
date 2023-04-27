namespace MobilePhonesWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class additionalFuncUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AdditionalFunctions", "SecondSIM", c => c.Int(nullable: false));
            AlterColumn("dbo.AdditionalFunctions", "HeadphonesJack", c => c.Int(nullable: false));
            AlterColumn("dbo.AdditionalFunctions", "MemoryCard", c => c.Int(nullable: false));
            AlterColumn("dbo.AdditionalFunctions", "FingerPrintReader", c => c.Int(nullable: false));
            AlterColumn("dbo.AdditionalFunctions", "FacialRecognition", c => c.Int(nullable: false));
            AlterColumn("dbo.AdditionalFunctions", "NFC", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AdditionalFunctions", "NFC", c => c.String());
            AlterColumn("dbo.AdditionalFunctions", "FacialRecognition", c => c.String());
            AlterColumn("dbo.AdditionalFunctions", "FingerPrintReader", c => c.String());
            AlterColumn("dbo.AdditionalFunctions", "MemoryCard", c => c.String());
            AlterColumn("dbo.AdditionalFunctions", "HeadphonesJack", c => c.String());
            AlterColumn("dbo.AdditionalFunctions", "SecondSIM", c => c.String());
        }
    }
}
