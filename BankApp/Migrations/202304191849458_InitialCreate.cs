namespace BankApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccounts",
                c => new
                {
                    Number = c.String(nullable: false, maxLength: 128),
                    Name = c.String(),
                    DateOpen = c.DateTime(nullable: false),
                    Balance = c.Double(nullable: false),
                    Type_Id = c.Int(),
                    User_Id = c.Int(),
                })
                .PrimaryKey(t => t.Number)
                .ForeignKey("dbo.BankAccountTypes", t => t.Type_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Type_Id)
                .Index(t => t.User_Id);

            CreateTable(
                "dbo.BankAccountTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Login = c.String(),
                    Password = c.String(),
                    Name = c.String(),
                    Surname = c.String(),
                    Patronymic = c.String(),
                    PassportSeries = c.String(),
                    PassportNumber = c.String(),
                    Address = c.String(),
                    Phone = c.String(),
                    Email = c.String(),
                    DateOfBirth = c.DateTime(nullable: false),
                    PlaceOfBirth = c.String(),
                    Role_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .Index(t => t.Role_Id);

            CreateTable(
                "dbo.Roles",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.DepositContracts",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Amount = c.Double(nullable: false),
                    Period = c.Int(nullable: false),
                    ExpirationDate = c.DateTime(nullable: false),
                    Percet = c.Double(nullable: false),
                    Account_Number = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccounts", t => t.Account_Number)
                .Index(t => t.Account_Number);

            CreateTable(
                "dbo.Histories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    DateTime = c.DateTime(nullable: false),
                    Amount = c.Double(nullable: false),
                    Account_Number = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccounts", t => t.Account_Number)
                .Index(t => t.Account_Number);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Histories", "Account_Number", "dbo.BankAccounts");
            DropForeignKey("dbo.DepositContracts", "Account_Number", "dbo.BankAccounts");
            DropForeignKey("dbo.BankAccounts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.BankAccounts", "Type_Id", "dbo.BankAccountTypes");
            DropIndex("dbo.Histories", new[] { "Account_Number" });
            DropIndex("dbo.DepositContracts", new[] { "Account_Number" });
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropIndex("dbo.BankAccounts", new[] { "User_Id" });
            DropIndex("dbo.BankAccounts", new[] { "Type_Id" });
            DropTable("dbo.Histories");
            DropTable("dbo.DepositContracts");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.BankAccountTypes");
            DropTable("dbo.BankAccounts");
        }
    }
}
