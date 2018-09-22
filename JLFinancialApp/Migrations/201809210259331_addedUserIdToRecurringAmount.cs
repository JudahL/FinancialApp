namespace JLFinancialApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUserIdToRecurringAmount : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Incomes", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Subscriptions", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Incomes", name: "IX_User_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.Subscriptions", name: "IX_User_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Subscriptions", name: "IX_UserId", newName: "IX_User_Id");
            RenameIndex(table: "dbo.Incomes", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Subscriptions", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Incomes", name: "UserId", newName: "User_Id");
        }
    }
}
