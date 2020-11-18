using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ATM.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(unicode: false, maxLength: 16, nullable: false),
                    Pin = table.Column<int>(nullable: false),
                    Balance = table.Column<long>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    IsLoked = table.Column<bool>(nullable: false),
                    ValidDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_Cards_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    OperationId = table.Column<int>(nullable: false),
                    OperationType = table.Column<int>(nullable: false),
                    CardId = table.Column<int>(nullable: false),
                    DateOperation = table.Column<DateTime>(type: "date", nullable: false),
                    Amount = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.OperationId);
                    table.ForeignKey(
                        name: "FK_Operations_Cards",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Name" },
                values: new object[] { 1, "Pablo" });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardId", "Balance", "IsLoked", "Number", "Pin", "UserId", "ValidDate" },
                values: new object[] { 1, 35000L, false, "4525111122223333", 1234, 1, new DateTime(2021, 11, 17, 5, 47, 33, 829, DateTimeKind.Local).AddTicks(2245) });

            migrationBuilder.InsertData(
                table: "Operations",
                columns: new[] { "OperationId", "Amount", "CardId", "DateOperation", "OperationType" },
                values: new object[] { 1, 1000, 1, new DateTime(2020, 11, 17, 5, 47, 33, 829, DateTimeKind.Local).AddTicks(2245), 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_UserId",
                table: "Cards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_CardId",
                table: "Operations",
                column: "CardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
