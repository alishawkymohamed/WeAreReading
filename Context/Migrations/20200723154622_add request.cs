using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Context.Migrations
{
    public partial class addrequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 23, 17, 46, 21, 524, DateTimeKind.Local).AddTicks(427),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 22, 20, 37, 50, 334, DateTimeKind.Local).AddTicks(7602));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Governments",
                nullable: true,
                defaultValue: new DateTime(2020, 7, 23, 17, 46, 21, 563, DateTimeKind.Local).AddTicks(1747),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 7, 22, 20, 37, 50, 370, DateTimeKind.Local).AddTicks(8293));

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAccepted = table.Column<bool>(nullable: true),
                    BookId = table.Column<int>(nullable: false),
                    SenderId = table.Column<int>(nullable: false),
                    ReceiverId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 23, 17, 46, 21, 546, DateTimeKind.Local).AddTicks(7722));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 23, 17, 46, 21, 547, DateTimeKind.Local).AddTicks(214));

            migrationBuilder.CreateIndex(
                name: "IX_Requests_BookId",
                table: "Requests",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ReceiverId",
                table: "Requests",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_SenderId",
                table: "Requests",
                column: "SenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 22, 20, 37, 50, 334, DateTimeKind.Local).AddTicks(7602),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 23, 17, 46, 21, 524, DateTimeKind.Local).AddTicks(427));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Governments",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2020, 7, 22, 20, 37, 50, 370, DateTimeKind.Local).AddTicks(8293),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 7, 23, 17, 46, 21, 563, DateTimeKind.Local).AddTicks(1747));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 22, 20, 37, 50, 356, DateTimeKind.Local).AddTicks(7969));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 22, 20, 37, 50, 357, DateTimeKind.Local).AddTicks(267));
        }
    }
}
