namespace ProgressBankApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Fixtypeofbalance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DepositContracts", "DateOpen", c => c.DateTime(nullable: false));
            AddColumn("dbo.DepositContracts", "DateExpiration", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BankAccounts", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DepositContracts", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.DepositContracts", "ExpirationDate");
        }

        public override void Down()
        {
            AddColumn("dbo.DepositContracts", "ExpirationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DepositContracts", "Balance", c => c.Double(nullable: false));
            AlterColumn("dbo.BankAccounts", "Balance", c => c.Double(nullable: false));
            DropColumn("dbo.DepositContracts", "DateExpiration");
            DropColumn("dbo.DepositContracts", "DateOpen");
        }
    }
}
