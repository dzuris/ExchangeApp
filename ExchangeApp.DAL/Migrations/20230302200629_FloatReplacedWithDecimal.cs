using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExchangeApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FloatReplacedWithDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AlterColumn<decimal>(
                name: "CourseRate",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "Donations",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AlterColumn<decimal>(
                name: "CourseRate",
                table: "Donations",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AlterColumn<decimal>(
                name: "SellRate",
                table: "Currencies",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "Currencies",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AlterColumn<decimal>(
                name: "BuyRate",
                table: "Currencies",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AverageCourseRate",
                table: "Currencies",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "BGN",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1m, null, 0m, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CAD",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1m, null, 0m, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CZK",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1m, null, 0m, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "EUR",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1m, null, 0m, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "GBP",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1m, null, 0m, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "HUF",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1m, null, 0m, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CHF",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1m, null, 0m, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "JPY",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1m, null, 0m, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "NOK",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1m, null, 0m, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "PLN",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1m, null, 0m, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "RUB",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1m, null, 0m, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "USD",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1m, null, 0m, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Quantity",
                table: "Transactions",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<float>(
                name: "CourseRate",
                table: "Transactions",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<float>(
                name: "Quantity",
                table: "Donations",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<float>(
                name: "CourseRate",
                table: "Donations",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<float>(
                name: "SellRate",
                table: "Currencies",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Quantity",
                table: "Currencies",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<float>(
                name: "BuyRate",
                table: "Currencies",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "AverageCourseRate",
                table: "Currencies",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "BGN",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1f, null, 0f, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CAD",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1f, null, 0f, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CZK",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1f, null, 0f, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "EUR",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1f, null, 0f, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "GBP",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1f, null, 0f, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "HUF",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1f, null, 0f, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CHF",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1f, null, 0f, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "JPY",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1f, null, 0f, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "NOK",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1f, null, 0f, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "PLN",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1f, null, 0f, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "RUB",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1f, null, 0f, null });

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "USD",
                columns: new[] { "AverageCourseRate", "BuyRate", "Quantity", "SellRate" },
                values: new object[] { 1f, null, 0f, null });
        }
    }
}
