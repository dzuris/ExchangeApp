using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExchangeApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Quantity = table.Column<decimal>(type: "TEXT", nullable: false),
                    PhotoUrl = table.Column<string>(type: "TEXT", nullable: false),
                    AverageCourseRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    BuyRate = table.Column<decimal>(type: "TEXT", nullable: true),
                    SellRate = table.Column<decimal>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    IdentificationNumber = table.Column<string>(type: "TEXT", nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    EvidenceType = table.Column<int>(type: "INTEGER", nullable: false),
                    EvidenceNumber = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TotalBalances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastTotalBalance = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalBalances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrenciesHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Quantity = table.Column<decimal>(type: "TEXT", nullable: false),
                    AverageCourseRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CurrencyEntityCode = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrenciesHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrenciesHistory_Currencies_CurrencyEntityCode",
                        column: x => x.CurrencyEntityCode,
                        principalTable: "Currencies",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "BusinessCustomers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TradeNameOfTheOwner = table.Column<string>(type: "TEXT", nullable: false),
                    TradeAddress = table.Column<string>(type: "TEXT", nullable: false),
                    ICO = table.Column<string>(type: "TEXT", nullable: false),
                    Nationality = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessCustomers_Customers_Id",
                        column: x => x.Id,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualCustomers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nationality = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualCustomers_Customers_Id",
                        column: x => x.Id,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MinorCustomers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinorCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MinorCustomers_Customers_Id",
                        column: x => x.Id,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Quantity = table.Column<decimal>(type: "TEXT", nullable: false),
                    CurrencyQuantityBefore = table.Column<decimal>(type: "TEXT", nullable: false),
                    CourseRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    AverageCourseRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsCanceled = table.Column<bool>(type: "INTEGER", nullable: false),
                    CurrencyCode = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    TransactionType = table.Column<int>(type: "INTEGER", nullable: true),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operations_Currencies_CurrencyCode",
                        column: x => x.CurrencyCode,
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Code", "AverageCourseRate", "BuyRate", "PhotoUrl", "Quantity", "SellRate", "Status" },
                values: new object[,]
                {
                    { "AUD", 0m, null, "aud_flag.png", 0m, null, 0 },
                    { "BGN", 0m, null, "bgn_flag.png", 0m, null, 0 },
                    { "BRL", 0m, null, "brl_flag.png", 0m, null, 0 },
                    { "CAD", 0m, null, "cad_flag.png", 0m, null, 0 },
                    { "CNY", 0m, null, "cny_flag.png", 0m, null, 0 },
                    { "CZK", 0m, null, "czk_flag.png", 0m, null, 0 },
                    { "DKK", 0m, null, "dkk_flag.png", 0m, null, 0 },
                    { "EUR", 1m, null, "eur_flag.png", 0m, null, 1 },
                    { "GBP", 0m, null, "gbp_flag.png", 0m, null, 0 },
                    { "HKD", 0m, null, "hkd_flag.png", 0m, null, 0 },
                    { "HUF", 0m, null, "huf_flag.png", 0m, null, 0 },
                    { "CHF", 0m, null, "chf_flag.png", 0m, null, 0 },
                    { "IDR", 0m, null, "idr_flag.png", 0m, null, 0 },
                    { "ILS", 0m, null, "ils_flag.png", 0m, null, 0 },
                    { "INR", 0m, null, "inr_flag.png", 0m, null, 0 },
                    { "JPY", 0m, null, "jpn_flag.png", 0m, null, 0 },
                    { "KRW", 0m, null, "krw_flag.png", 0m, null, 0 },
                    { "MXN", 0m, null, "mxn_flag.png", 0m, null, 0 },
                    { "MYR", 0m, null, "myr_flag.png", 0m, null, 0 },
                    { "NOK", 0m, null, "nok_flag.png", 0m, null, 0 },
                    { "NZD", 0m, null, "nzd_flag.png", 0m, null, 0 },
                    { "PHP", 0m, null, "php_flag.png", 0m, null, 0 },
                    { "PLN", 0m, null, "pln_flag.png", 0m, null, 0 },
                    { "RON", 0m, null, "ron_flag.png", 0m, null, 0 },
                    { "RUB", 0m, null, "rub_flag.png", 0m, null, 0 },
                    { "SEK", 0m, null, "sek_flag.png", 0m, null, 0 },
                    { "SGD", 0m, null, "sgd_flag.png", 0m, null, 0 },
                    { "THB", 0m, null, "thb_flag.png", 0m, null, 0 },
                    { "TRY", 0m, null, "try_flag.png", 0m, null, 0 },
                    { "USD", 0m, null, "usd_flag.png", 0m, null, 0 },
                    { "ZAR", 0m, null, "zar_flag.png", 0m, null, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrenciesHistory_CurrencyEntityCode",
                table: "CurrenciesHistory",
                column: "CurrencyEntityCode");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_CurrencyCode",
                table: "Operations",
                column: "CurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_CustomerId",
                table: "Operations",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TotalBalances_Type_Created",
                table: "TotalBalances",
                columns: new[] { "Type", "Created" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessCustomers");

            migrationBuilder.DropTable(
                name: "CurrenciesHistory");

            migrationBuilder.DropTable(
                name: "IndividualCustomers");

            migrationBuilder.DropTable(
                name: "MinorCustomers");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "TotalBalances");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
