using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseWork_Update.Migrations
{
    public partial class AddNewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepositsInfoModels",
                columns: table => new
                {
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoNumbersId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoDescriptionsId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Term = table.Column<int>(type: "int", nullable: false),
                    PercentBeforeTax = table.Column<int>(type: "int", nullable: false),
                    PercentAfterTax = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositsInfoModels", x => x.Title);
                });

            migrationBuilder.CreateTable(
                name: "PhotoInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoInfos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepositsInfoModels");

            migrationBuilder.DropTable(
                name: "PhotoInfos");
        }
    }
}
