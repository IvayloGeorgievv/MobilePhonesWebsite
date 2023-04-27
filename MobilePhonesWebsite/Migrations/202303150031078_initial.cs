namespace MobilePhonesWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdditionalFunctions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SecondSIM = c.String(),
                        HeadphonesJack = c.String(),
                        MemoryCard = c.String(),
                        FingerPrintReader = c.String(),
                        FacialRecognition = c.String(),
                        NFC = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MobilePhoneCamera",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MainRearCamera = c.String(),
                        RearCamera = c.String(),
                        FrontCamera = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MobilePhoneDisplay",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplaySize = c.Double(nullable: false),
                        Technology = c.String(),
                        Resolution = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MobilePhoneProcessor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessorType = c.String(),
                        ProcessorFrequency = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MobilePhones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Brand = c.String(),
                        Model = c.String(),
                        Size = c.String(),
                        BatteryLife = c.Int(nullable: false),
                        Weight = c.Double(nullable: false),
                        Colour = c.Int(nullable: false),
                        SIMCard = c.Int(nullable: false),
                        MobilePhoneCameraId = c.Int(nullable: false),
                        MobilePhoneDisplayId = c.Int(nullable: false),
                        MobilePhoneProcessorId = c.Int(nullable: false),
                        AdditionalFunctions = c.Int(nullable: false),
                        Image = c.String(),
                        OperatingMemory = c.Int(nullable: false),
                        StorageSpace = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Case",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Brand = c.String(nullable: false),
                        FitFor = c.String(nullable: false),
                        Image = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Protector",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Brand = c.String(nullable: false),
                        FitFor = c.String(nullable: false),
                        Image = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Smartwatch",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Brand = c.String(nullable: false),
                        Model = c.String(),
                        Size = c.String(),
                        DisplayTechnology = c.String(),
                        BatteryLife = c.Double(nullable: false),
                        Description = c.String(),
                        Image = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WiredHeadphones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Brand = c.String(nullable: false),
                        Model = c.String(),
                        Connectivity = c.String(),
                        Type = c.Int(nullable: false),
                        Image = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WirelessHeadphones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Brand = c.String(nullable: false),
                        Model = c.String(),
                        Connectivity = c.String(),
                        Type = c.Int(nullable: false),
                        BatteryLife = c.Double(nullable: false),
                        Image = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WirelessHeadphones");
            DropTable("dbo.WiredHeadphones");
            DropTable("dbo.Users");
            DropTable("dbo.Smartwatch");
            DropTable("dbo.Protector");
            DropTable("dbo.Case");
            DropTable("dbo.MobilePhones");
            DropTable("dbo.MobilePhoneProcessor");
            DropTable("dbo.MobilePhoneDisplay");
            DropTable("dbo.MobilePhoneCamera");
            DropTable("dbo.AdditionalFunctions");
        }
    }
}
