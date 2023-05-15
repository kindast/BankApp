namespace BankApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Fixhistory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Histories", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }

        public override void Down()
        {
            AlterColumn("dbo.Histories", "Amount", c => c.Double(nullable: false));
        }
    }
}
