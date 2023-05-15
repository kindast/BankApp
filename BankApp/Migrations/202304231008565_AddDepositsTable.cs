namespace BankApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDepositsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Deposits",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Percet = c.Double(nullable: false),
                    IsCapitalization = c.Boolean(nullable: false),
                    IsReplenishment = c.Boolean(nullable: false),
                    IsWithdrawal = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.DepositContracts", "Balance", c => c.Double(nullable: false));
            AddColumn("dbo.DepositContracts", "Deposit_Id", c => c.Int());
            CreateIndex("dbo.DepositContracts", "Deposit_Id");
            AddForeignKey("dbo.DepositContracts", "Deposit_Id", "dbo.Deposits", "Id");
            DropColumn("dbo.DepositContracts", "Amount");
            DropColumn("dbo.DepositContracts", "Percet");
        }

        public override void Down()
        {
            AddColumn("dbo.DepositContracts", "Percet", c => c.Double(nullable: false));
            AddColumn("dbo.DepositContracts", "Amount", c => c.Double(nullable: false));
            DropForeignKey("dbo.DepositContracts", "Deposit_Id", "dbo.Deposits");
            DropIndex("dbo.DepositContracts", new[] { "Deposit_Id" });
            DropColumn("dbo.DepositContracts", "Deposit_Id");
            DropColumn("dbo.DepositContracts", "Balance");
            DropTable("dbo.Deposits");
        }
    }
}
