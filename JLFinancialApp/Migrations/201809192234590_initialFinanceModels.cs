namespace JLFinancialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialFinanceModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Incomes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Amount = c.Int(nullable: false),
                        PeriodType_Id = c.Byte(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PeriodTypes", t => t.PeriodType_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.PeriodType_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.PeriodTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false),
                        FrequencyPerYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Amount = c.Int(nullable: false),
                        PeriodType_Id = c.Byte(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PeriodTypes", t => t.PeriodType_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.PeriodType_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscriptions", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Subscriptions", "PeriodType_Id", "dbo.PeriodTypes");
            DropForeignKey("dbo.Incomes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Incomes", "PeriodType_Id", "dbo.PeriodTypes");
            DropIndex("dbo.Subscriptions", new[] { "User_Id" });
            DropIndex("dbo.Subscriptions", new[] { "PeriodType_Id" });
            DropIndex("dbo.Incomes", new[] { "User_Id" });
            DropIndex("dbo.Incomes", new[] { "PeriodType_Id" });
            DropTable("dbo.Subscriptions");
            DropTable("dbo.PeriodTypes");
            DropTable("dbo.Incomes");
        }
    }
}
