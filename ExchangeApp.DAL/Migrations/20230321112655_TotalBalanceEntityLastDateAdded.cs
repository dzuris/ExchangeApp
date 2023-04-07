using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExchangeApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TotalBalanceEntityLastDateAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TotalBalances_Type_Date",
                table: "TotalBalances");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "TotalBalances",
                newName: "LastTotalBalance");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TotalBalances",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "TotalBalances",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_TotalBalances_Type_Created",
                table: "TotalBalances",
                columns: new[] { "Type", "Created" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TotalBalances_Type_Created",
                table: "TotalBalances");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "TotalBalances");

            migrationBuilder.RenameColumn(
                name: "LastTotalBalance",
                table: "TotalBalances",
                newName: "Date");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "TotalBalances",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_TotalBalances_Type_Date",
                table: "TotalBalances",
                columns: new[] { "Type", "Date" },
                unique: true);
        }
    }
}
