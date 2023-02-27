using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExchangeApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class PlnCurrencyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Code", "AverageCourseRate", "BuyRate", "BuyRateDeviation", "BuyRateDeviationPercent", "MiddleCourse", "PhotoUrl", "Quantity", "SellRate", "SellRateDeviation", "SellRateDeviationPercent", "State" },
                values: new object[] { "PLN", 1f, null, null, null, 1f, "pln.png", 0f, null, null, null, "Poľsko" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "PLN");
        }
    }
}
