using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Context.Migrations
{
    public partial class removepricefrombook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Books");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 22, 20, 37, 50, 334, DateTimeKind.Local).AddTicks(7602),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 2, 5, 12, 4, 390, DateTimeKind.Local).AddTicks(3079));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Governments",
                nullable: true,
                defaultValue: new DateTime(2020, 7, 22, 20, 37, 50, 370, DateTimeKind.Local).AddTicks(8293),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 7, 2, 5, 12, 4, 430, DateTimeKind.Local).AddTicks(7420));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2020, 7, 22, 20, 37, 50, 356, DateTimeKind.Local).AddTicks(7969), "Borrower" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2020, 7, 22, 20, 37, 50, 357, DateTimeKind.Local).AddTicks(267), "Library Owner" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 2, 5, 12, 4, 390, DateTimeKind.Local).AddTicks(3079),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 22, 20, 37, 50, 334, DateTimeKind.Local).AddTicks(7602));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Governments",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2020, 7, 2, 5, 12, 4, 430, DateTimeKind.Local).AddTicks(7420),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 7, 22, 20, 37, 50, 370, DateTimeKind.Local).AddTicks(8293));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Books",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2020, 7, 2, 5, 12, 4, 416, DateTimeKind.Local).AddTicks(735), "User" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2020, 7, 2, 5, 12, 4, 416, DateTimeKind.Local).AddTicks(3155), "Library" });
        }
    }
}
