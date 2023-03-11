using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExchangeApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AverageCourseOperation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AverageCourseRate",
                table: "Operations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "AUD",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "BGN",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "BRL",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CAD",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CNY",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CZK",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "DKK",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "EUR",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "GBP",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "HKD",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "HUF",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CHF",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "IDR",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "ILS",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "INR",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "JPY",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "KRW",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "MXN",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "MYR",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "NOK",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "NZD",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "PHP",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "PLN",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "RON",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "RUB",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "SEK",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "SGD",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "THB",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "TRY",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "USD",
                column: "AverageCourseRate",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "ZAR",
                column: "AverageCourseRate",
                value: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageCourseRate",
                table: "Operations");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "AUD",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "BGN",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "BRL",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CAD",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CNY",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CZK",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "DKK",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "EUR",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "GBP",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "HKD",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "HUF",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CHF",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "IDR",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "ILS",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "INR",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "JPY",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "KRW",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "MXN",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "MYR",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "NOK",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "NZD",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "PHP",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "PLN",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "RON",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "RUB",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "SEK",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "SGD",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "THB",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "TRY",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "USD",
                column: "AverageCourseRate",
                value: 1m);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "ZAR",
                column: "AverageCourseRate",
                value: 1m);
        }
    }
}
