using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExchangeApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NewSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Code", "AverageCourseRate", "BuyRate", "PhotoUrl", "Quantity", "SellRate", "Status" },
                values: new object[,]
                {
                    { "BGN", 1f, null, "bgn.png", 0f, null, 0 },
                    { "CAD", 1f, null, "cad.png", 0f, null, 0 },
                    { "HUF", 1f, null, "huf.png", 0f, null, 0 },
                    { "CHF", 1f, null, "chf.png", 0f, null, 0 },
                    { "NOK", 1f, null, "nok.png", 0f, null, 0 },
                    { "RUB", 1f, null, "rub.png", 0f, null, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "BGN");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CAD");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "HUF");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CHF");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "NOK");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "RUB");
        }
    }
}
