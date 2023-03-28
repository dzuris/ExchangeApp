﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExchangeApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CurrenciesHistoryTableRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyHistory");

            migrationBuilder.CreateTable(
                name: "CurrenciesHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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

            migrationBuilder.CreateIndex(
                name: "IX_CurrenciesHistory_CurrencyEntityCode",
                table: "CurrenciesHistory",
                column: "CurrencyEntityCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrenciesHistory");

            migrationBuilder.CreateTable(
                name: "CurrencyHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AverageCourseRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    CurrencyEntityCode = table.Column<string>(type: "TEXT", nullable: true),
                    Quantity = table.Column<decimal>(type: "TEXT", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyHistory_Currencies_CurrencyEntityCode",
                        column: x => x.CurrencyEntityCode,
                        principalTable: "Currencies",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyHistory_CurrencyEntityCode",
                table: "CurrencyHistory",
                column: "CurrencyEntityCode");
        }
    }
}
