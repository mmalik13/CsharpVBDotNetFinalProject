namespace Bank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestUserAccountSwitchPropertyToObject : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "UserAccount_AccountNumber", "dbo.Accounts");
            DropIndex("dbo.Users", new[] { "UserAccount_AccountNumber" });
            DropColumn("dbo.Users", "UserAccount_AccountNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UserAccount_AccountNumber", c => c.Int());
            CreateIndex("dbo.Users", "UserAccount_AccountNumber");
            AddForeignKey("dbo.Users", "UserAccount_AccountNumber", "dbo.Accounts", "AccountNumber");
        }
    }
}
