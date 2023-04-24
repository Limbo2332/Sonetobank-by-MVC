using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseWork_Update.Migrations
{
    public partial class InitialValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoDescriptionsId",
                table: "DepositsInfoModels");

            migrationBuilder.AlterColumn<double>(
                name: "PercentBeforeTax",
                table: "DepositsInfoModels",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "PercentAfterTax",
                table: "DepositsInfoModels",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "MaxSumOfDeposit",
                table: "DepositsInfoModels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinSumOfDeposit",
                table: "DepositsInfoModels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "DepositsInfoModels",
                columns: new[] { "Title", "Description", "MaxSumOfDeposit", "MinSumOfDeposit", "PercentAfterTax", "PercentBeforeTax", "PhotoNumbersId", "Term" },
                values: new object[,]
                {
                    { "Дитячий", "Накопичуйте кошти до повноліття дитини та створюйте матеріальну основу для отримання бажаної освіти, придбання омріяного житла <br />та всього необхідного.", 1000000.0, 500.0, 4.0300000000000002, 5.0, "1 5 2 6 3 10 4 8", 36 },
                    { "Мобільні заощадження", "Додатковий дохід та швидке управління тимчасово вільними коштами за допомогою картки та мобільного додатку.", 1000000.0, 500.0, 2.8199999999999998, 3.5, "1 5 2 6 3 10 4 8", 36 },
                    { "Накопичувальний", "Поповнюйте свій депозит будь-коли та отримуйте максимальний дохід у кінці сроку.", 1000000.0, 5000.0, 4.0300000000000002, 5.0, "1 5 2 6 3 10 4 8", 6 },
                    { "Ощадний", "Поповнюйте депозит в будь-який час або частково знімайте кошти без штрафних санкцій.", 1000000.0, 5000.0, 1.6100000000000001, 2.0, "1 5 2 9 3 7 4 8", 9 },
                    { "Перспективний", "Депозит «Перспективний» зі ставкою, що зростає, та можливістю повного дострокового повернення вкладу без втрати процентів.", 1000000.0, 5000.0, 4.8300000000000001, 6.0, "1 5 2 6 3 7 4 8", 6 },
                    { "Строковий", "Розміщуйте кошти та гарантовано отримайте максимально можливий дохід навіть протягом короткого строку.", 1000000.0, 5000.0, 5.6299999999999999, 7.0, "1 5 2 6 3 7 4 11", 12 }
                });

            migrationBuilder.InsertData(
                table: "PhotoInfos",
                columns: new[] { "Id", "Description", "PhotoNumber" },
                values: new object[,]
                {
                    { 1, "Строк розміщення: ", "1-1" },
                    { 2, "Мінімальна сума вкладу:", "1-2" },
                    { 3, "Відсоткова ставка: ", "1-3" },
                    { 4, "Максимальна сума: ", "1-4" },
                    { 5, "Валюта: гривня", "2-1" },
                    { 6, "Умови сплати процентів: у кінці <br />строку розміщення", "2-2" },
                    { 7, "Без можливості поповнення", "2-3" },
                    { 8, "Без можливості дострокового розірвання", "2-4" },
                    { 9, "З можливістю часткового зняття <br />коштів", "2-2-1" },
                    { 10, "З можливостю поповнення", "2-3-1" },
                    { 11, "Програма лояльності", "2-4-1" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DepositsInfoModels",
                keyColumn: "Title",
                keyValue: "Дитячий");

            migrationBuilder.DeleteData(
                table: "DepositsInfoModels",
                keyColumn: "Title",
                keyValue: "Мобільні заощадження");

            migrationBuilder.DeleteData(
                table: "DepositsInfoModels",
                keyColumn: "Title",
                keyValue: "Накопичувальний");

            migrationBuilder.DeleteData(
                table: "DepositsInfoModels",
                keyColumn: "Title",
                keyValue: "Ощадний");

            migrationBuilder.DeleteData(
                table: "DepositsInfoModels",
                keyColumn: "Title",
                keyValue: "Перспективний");

            migrationBuilder.DeleteData(
                table: "DepositsInfoModels",
                keyColumn: "Title",
                keyValue: "Строковий");

            migrationBuilder.DeleteData(
                table: "PhotoInfos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PhotoInfos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PhotoInfos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PhotoInfos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PhotoInfos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PhotoInfos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PhotoInfos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PhotoInfos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PhotoInfos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PhotoInfos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PhotoInfos",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DropColumn(
                name: "MaxSumOfDeposit",
                table: "DepositsInfoModels");

            migrationBuilder.DropColumn(
                name: "MinSumOfDeposit",
                table: "DepositsInfoModels");

            migrationBuilder.AlterColumn<int>(
                name: "PercentBeforeTax",
                table: "DepositsInfoModels",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "PercentAfterTax",
                table: "DepositsInfoModels",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "PhotoDescriptionsId",
                table: "DepositsInfoModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
