using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseWork_Update.Migrations
{
    public partial class ChangeSomeAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Deposits",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DateTime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Credits",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DateTime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "AspNetUsers",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "DateTimeOffset",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Deposits",
                type: "DateTime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Credits",
                type: "DateTime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "AspNetUsers",
                type: "DateTimeOffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);
        }
    }
}
