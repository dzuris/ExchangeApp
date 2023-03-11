using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExchangeApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FinalCurrencySeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Code", "AverageCourseRate", "BuyRate", "PhotoUrl", "Quantity", "SellRate", "Status" },
                values: new object[,]
                {
                    { "AUD", 1m, null, "aud.png", 0m, null, 0 },
                    { "BRL", 1m, null, "brl.png", 0m, null, 0 },
                    { "CNY", 1m, null, "cny.png", 0m, null, 0 },
                    { "DKK", 1m, null, "dkk.png", 0m, null, 0 },
                    { "HKD", 1m, null, "hkd.png", 0m, null, 0 },
                    { "IDR", 1m, null, "idr.png", 0m, null, 0 },
                    { "ILS", 1m, null, "ils.png", 0m, null, 0 },
                    { "INR", 1m, null, "inr.png", 0m, null, 0 },
                    { "KRW", 1m, null, "krw.png", 0m, null, 0 },
                    { "MXN", 1m, null, "mxn.png", 0m, null, 0 },
                    { "MYR", 1m, null, "myr.png", 0m, null, 0 },
                    { "NZD", 1m, null, "nzd.png", 0m, null, 0 },
                    { "PHP", 1m, null, "php.png", 0m, null, 0 },
                    { "RON", 1m, null, "ron.png", 0m, null, 0 },
                    { "SEK", 1m, null, "sek.png", 0m, null, 0 },
                    { "SGD", 1m, null, "sgd.png", 0m, null, 0 },
                    { "THB", 1m, null, "thb.png", 0m, null, 0 },
                    { "TRY", 1m, null, "try.png", 0m, null, 0 },
                    { "ZAR", 1m, null, "zar.png", 0m, null, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "AUD");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "BRL");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CNY");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "DKK");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "HKD");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "IDR");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "ILS");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "INR");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "KRW");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "MXN");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "MYR");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "NZD");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "PHP");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "RON");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "SEK");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "SGD");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "THB");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "TRY");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "ZAR");
        }
    }
}
