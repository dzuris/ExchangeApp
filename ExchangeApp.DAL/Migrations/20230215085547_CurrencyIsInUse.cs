using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExchangeApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CurrencyIsInUse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInUse",
                table: "Currencies",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CZK",
                column: "IsInUse",
                value: false);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "EUR",
                column: "IsInUse",
                value: false);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "PLN",
                column: "IsInUse",
                value: false);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "USD",
                column: "IsInUse",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInUse",
                table: "Currencies");
        }
    }
}
