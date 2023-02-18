using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExchangeApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesClearence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencySales");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "BuyRateDeviation",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "BuyRateDeviationPercent",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "MiddleCourse",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "SellRateDeviation",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "SellRateDeviationPercent",
                table: "Currencies");

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "Donations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "Donations");

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                table: "Persons",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<float>(
                name: "BuyRateDeviation",
                table: "Currencies",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "BuyRateDeviationPercent",
                table: "Currencies",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MiddleCourse",
                table: "Currencies",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "SellRateDeviation",
                table: "Currencies",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SellRateDeviationPercent",
                table: "Currencies",
                type: "REAL",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CurrencySales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CurrencyCode = table.Column<string>(type: "TEXT", nullable: false),
                    ActiveAboutAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    Sale = table.Column<float>(type: "REAL", nullable: true),
                    SalePercent = table.Column<float>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencySales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencySales_Currencies_CurrencyCode",
                        column: x => x.CurrencyCode,
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CZK",
                columns: new[] { "BuyRateDeviation", "BuyRateDeviationPercent", "MiddleCourse", "SellRateDeviation", "SellRateDeviationPercent" },
                values: new object[] { null, null, 1f, null, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "EUR",
                columns: new[] { "BuyRateDeviation", "BuyRateDeviationPercent", "MiddleCourse", "SellRateDeviation", "SellRateDeviationPercent" },
                values: new object[] { null, null, 1f, null, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "PLN",
                columns: new[] { "BuyRateDeviation", "BuyRateDeviationPercent", "MiddleCourse", "SellRateDeviation", "SellRateDeviationPercent" },
                values: new object[] { null, null, 1f, null, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "USD",
                columns: new[] { "BuyRateDeviation", "BuyRateDeviationPercent", "MiddleCourse", "SellRateDeviation", "SellRateDeviationPercent" },
                values: new object[] { null, null, 1f, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencySales_CurrencyCode",
                table: "CurrencySales",
                column: "CurrencyCode");
        }
    }
}
