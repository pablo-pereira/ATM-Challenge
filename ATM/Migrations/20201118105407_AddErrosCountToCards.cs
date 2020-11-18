using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ATM.Migrations
{
    public partial class AddErrosCountToCards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ErrorsCount",
                table: "Cards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 1,
                column: "ValidDate",
                value: new DateTime(2021, 11, 18, 7, 54, 7, 38, DateTimeKind.Local).AddTicks(4061));

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 1,
                column: "DateOperation",
                value: new DateTime(2020, 11, 17, 7, 54, 7, 39, DateTimeKind.Local).AddTicks(7157));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrorsCount",
                table: "Cards");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 1,
                column: "ValidDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "OperationId",
                keyValue: 1,
                column: "DateOperation",
                value: new DateTime(2020, 11, 17, 5, 47, 33, 829, DateTimeKind.Local).AddTicks(2245));
        }
    }
}
