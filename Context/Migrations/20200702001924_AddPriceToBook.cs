using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Context.Migrations
{
    public partial class AddPriceToBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 2, 2, 19, 23, 337, DateTimeKind.Local).AddTicks(6241),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 4, 4, 17, 24, 34, 424, DateTimeKind.Local).AddTicks(5743));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Governments",
                nullable: true,
                defaultValue: new DateTime(2020, 7, 2, 2, 19, 23, 375, DateTimeKind.Local).AddTicks(1941),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 4, 4, 17, 24, 34, 456, DateTimeKind.Local).AddTicks(1032));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Books",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 2, 2, 19, 23, 360, DateTimeKind.Local).AddTicks(4554));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 2, 2, 19, 23, 360, DateTimeKind.Local).AddTicks(6854));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Books");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 4, 4, 17, 24, 34, 424, DateTimeKind.Local).AddTicks(5743),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 2, 2, 19, 23, 337, DateTimeKind.Local).AddTicks(6241));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Governments",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2020, 4, 4, 17, 24, 34, 456, DateTimeKind.Local).AddTicks(1032),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 7, 2, 2, 19, 23, 375, DateTimeKind.Local).AddTicks(1941));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 4, 4, 17, 24, 34, 444, DateTimeKind.Local).AddTicks(9645));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 4, 4, 17, 24, 34, 445, DateTimeKind.Local).AddTicks(1355));
        }
    }
}
