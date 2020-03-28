using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Context.Migrations
{
    public partial class ConvertDateTimeToDateTimeOffset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "RefreshTokenExpiresDateTime",
                table: "UserTokens",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "AccessTokenExpiresDateTime",
                table: "UserTokens",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Roles",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 28, 15, 27, 7, 629, DateTimeKind.Local).AddTicks(831),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 3, 28, 3, 11, 8, 301, DateTimeKind.Local).AddTicks(2670));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2020, 3, 28, 15, 27, 7, 650, DateTimeKind.Local).AddTicks(875));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2020, 3, 28, 15, 27, 7, 650, DateTimeKind.Local).AddTicks(3910));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RefreshTokenExpiresDateTime",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AccessTokenExpiresDateTime",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 28, 3, 11, 8, 301, DateTimeKind.Local).AddTicks(2670),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 28, 15, 27, 7, 629, DateTimeKind.Local).AddTicks(831));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2020, 3, 28, 3, 11, 8, 321, DateTimeKind.Local).AddTicks(4747));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2020, 3, 28, 3, 11, 8, 321, DateTimeKind.Local).AddTicks(6547));
        }
    }
}
