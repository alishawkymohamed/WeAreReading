using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Context.Migrations
{
    public partial class AddStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 2, 5, 12, 4, 390, DateTimeKind.Local).AddTicks(3079),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 2, 2, 19, 23, 337, DateTimeKind.Local).AddTicks(6241));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Governments",
                nullable: true,
                defaultValue: new DateTime(2020, 7, 2, 5, 12, 4, 430, DateTimeKind.Local).AddTicks(7420),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 7, 2, 2, 19, 23, 375, DateTimeKind.Local).AddTicks(1941));

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Books",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 2, 5, 12, 4, 416, DateTimeKind.Local).AddTicks(735));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 2, 5, 12, 4, 416, DateTimeKind.Local).AddTicks(3155));

            migrationBuilder.CreateIndex(
                name: "IX_Books_StatusId",
                table: "Books",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Statuses_StatusId",
                table: "Books",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.InsertData(
            table: "Statuses",
            columns: new[] { "Id", "Name" },
            values: new object[] { 1, "New" });

            migrationBuilder.InsertData(
            table: "Statuses",
            columns: new[] { "Id", "Name" },
            values: new object[] { 2, "Used" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Statuses_StatusId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Books_StatusId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Books");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 2, 2, 19, 23, 337, DateTimeKind.Local).AddTicks(6241),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 2, 5, 12, 4, 390, DateTimeKind.Local).AddTicks(3079));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Governments",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2020, 7, 2, 2, 19, 23, 375, DateTimeKind.Local).AddTicks(1941),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 7, 2, 5, 12, 4, 430, DateTimeKind.Local).AddTicks(7420));

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
    }
}
