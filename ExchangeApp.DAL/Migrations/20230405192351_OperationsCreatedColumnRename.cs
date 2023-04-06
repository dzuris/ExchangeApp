using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExchangeApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class OperationsCreatedColumnRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Operations",
                newName: "Created");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "CurrenciesHistory",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "AUD",
                column: "PhotoUrl",
                value: "aud_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "BGN",
                column: "PhotoUrl",
                value: "bgn_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "BRL",
                column: "PhotoUrl",
                value: "brl_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CAD",
                column: "PhotoUrl",
                value: "cad_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CNY",
                column: "PhotoUrl",
                value: "cny_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CZK",
                column: "PhotoUrl",
                value: "czk_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "DKK",
                column: "PhotoUrl",
                value: "dkk_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "EUR",
                column: "PhotoUrl",
                value: "eur_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "GBP",
                column: "PhotoUrl",
                value: "gbp_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "HKD",
                column: "PhotoUrl",
                value: "hkd_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "HUF",
                column: "PhotoUrl",
                value: "huf_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CHF",
                column: "PhotoUrl",
                value: "chf_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "IDR",
                column: "PhotoUrl",
                value: "idr_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "ILS",
                column: "PhotoUrl",
                value: "ils_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "INR",
                column: "PhotoUrl",
                value: "inr_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "JPY",
                column: "PhotoUrl",
                value: "jpn_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "KRW",
                column: "PhotoUrl",
                value: "krw_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "MXN",
                column: "PhotoUrl",
                value: "mxn_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "MYR",
                column: "PhotoUrl",
                value: "myr_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "NOK",
                column: "PhotoUrl",
                value: "nok_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "NZD",
                column: "PhotoUrl",
                value: "nzd_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "PHP",
                column: "PhotoUrl",
                value: "php_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "PLN",
                column: "PhotoUrl",
                value: "pln_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "RON",
                column: "PhotoUrl",
                value: "ron_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "RUB",
                column: "PhotoUrl",
                value: "rub_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "SEK",
                column: "PhotoUrl",
                value: "sek_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "SGD",
                column: "PhotoUrl",
                value: "sgd_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "THB",
                column: "PhotoUrl",
                value: "thb_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "TRY",
                column: "PhotoUrl",
                value: "try_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "USD",
                column: "PhotoUrl",
                value: "usd_flag.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "ZAR",
                column: "PhotoUrl",
                value: "zar_flag.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Operations",
                newName: "Time");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CurrenciesHistory",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "AUD",
                column: "PhotoUrl",
                value: "aud.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "BGN",
                column: "PhotoUrl",
                value: "bgn.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "BRL",
                column: "PhotoUrl",
                value: "brl.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CAD",
                column: "PhotoUrl",
                value: "cad.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CNY",
                column: "PhotoUrl",
                value: "cny.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CZK",
                column: "PhotoUrl",
                value: "czk.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "DKK",
                column: "PhotoUrl",
                value: "dkk.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "EUR",
                column: "PhotoUrl",
                value: "eur.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "GBP",
                column: "PhotoUrl",
                value: "gbp.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "HKD",
                column: "PhotoUrl",
                value: "hkd.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "HUF",
                column: "PhotoUrl",
                value: "huf.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "CHF",
                column: "PhotoUrl",
                value: "chf.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "IDR",
                column: "PhotoUrl",
                value: "idr.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "ILS",
                column: "PhotoUrl",
                value: "ils.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "INR",
                column: "PhotoUrl",
                value: "inr.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "JPY",
                column: "PhotoUrl",
                value: "jpn.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "KRW",
                column: "PhotoUrl",
                value: "krw.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "MXN",
                column: "PhotoUrl",
                value: "mxn.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "MYR",
                column: "PhotoUrl",
                value: "myr.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "NOK",
                column: "PhotoUrl",
                value: "nok.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "NZD",
                column: "PhotoUrl",
                value: "nzd.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "PHP",
                column: "PhotoUrl",
                value: "php.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "PLN",
                column: "PhotoUrl",
                value: "pln.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "RON",
                column: "PhotoUrl",
                value: "ron.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "RUB",
                column: "PhotoUrl",
                value: "rub.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "SEK",
                column: "PhotoUrl",
                value: "sek.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "SGD",
                column: "PhotoUrl",
                value: "sgd.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "THB",
                column: "PhotoUrl",
                value: "thb.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "TRY",
                column: "PhotoUrl",
                value: "try.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "USD",
                column: "PhotoUrl",
                value: "usd.png");

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code",
                keyValue: "ZAR",
                column: "PhotoUrl",
                value: "zar.png");
        }
    }
}
