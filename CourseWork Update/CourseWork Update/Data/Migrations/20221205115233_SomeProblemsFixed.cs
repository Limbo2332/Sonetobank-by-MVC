using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseWork_Update.Migrations
{
    public partial class SomeProblemsFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PhotoInfos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Мінімальна сума вкладу: ");

            migrationBuilder.UpdateData(
                table: "PhotoInfos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "Умови сплати процентів: у кінці строку розміщення");

            migrationBuilder.UpdateData(
                table: "PhotoInfos",
                keyColumn: "Id",
                keyValue: 9,
                column: "Description",
                value: "З можливістю часткового зняття коштів");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PhotoInfos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Мінімальна сума вкладу:");

            migrationBuilder.UpdateData(
                table: "PhotoInfos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "Умови сплати процентів: у кінці <br />строку розміщення");

            migrationBuilder.UpdateData(
                table: "PhotoInfos",
                keyColumn: "Id",
                keyValue: 9,
                column: "Description",
                value: "З можливістю часткового зняття <br />коштів");
        }
    }
}
