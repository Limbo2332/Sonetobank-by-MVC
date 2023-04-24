using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseWork_Update.Migrations
{
    public partial class AddDeposits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deposits",
                columns: table => new
                {
                    DepositId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepositName = table.Column<int>(type: "int", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepositSum = table.Column<double>(type: "float", nullable: false),
                    Term = table.Column<int>(type: "int", nullable: false),
                    PercentBeforeTax = table.Column<double>(type: "float", nullable: false),
                    PercentAfterTax = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "DateTime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposits", x => x.DepositId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deposits");
        }
    }
}
