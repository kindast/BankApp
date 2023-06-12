namespace ProgressBankApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DepositContract : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DepositContracts", "Balance");
            DropColumn("dbo.DepositContracts", "DateOpen");
        }

        public override void Down()
        {
            AddColumn("dbo.DepositContracts", "DateOpen", c => c.DateTime(nullable: false));
            AddColumn("dbo.DepositContracts", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
