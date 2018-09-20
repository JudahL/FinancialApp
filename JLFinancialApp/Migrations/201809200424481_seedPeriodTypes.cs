namespace JLFinancialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedPeriodTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO PeriodTypes (Id, Name, FrequencyPerYear) VALUES (1, 'Yearly', 1)");
            Sql("INSERT INTO PeriodTypes (Id, Name, FrequencyPerYear) VALUES (2, 'Monthly', 12)");
            Sql("INSERT INTO PeriodTypes (Id, Name, FrequencyPerYear) VALUES (3, 'Weekly', 52)");
            Sql("INSERT INTO PeriodTypes (Id, Name, FrequencyPerYear) VALUES (4, 'Daily', 365)");
        }
        
        public override void Down()
        {
        }
    }
}
