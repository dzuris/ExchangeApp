﻿// <auto-generated />
using System;
using ExchangeApp.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExchangeApp.DAL.Migrations
{
    [DbContext(typeof(ExchangeAppDbContext))]
    [Migration("20230305211932_TransactionIdRemovedFromCustomer")]
    partial class TransactionIdRemovedFromCustomer
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("ExchangeApp.DAL.Entities.CurrencyEntity", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("AverageCourseRate")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("BuyRate")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("SellRate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Code");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Code = "EUR",
                            AverageCourseRate = 1m,
                            PhotoUrl = "eur.png",
                            Quantity = 0m,
                            Status = 1
                        },
                        new
                        {
                            Code = "CZK",
                            AverageCourseRate = 1m,
                            PhotoUrl = "czk.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "USD",
                            AverageCourseRate = 1m,
                            PhotoUrl = "usd.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "PLN",
                            AverageCourseRate = 1m,
                            PhotoUrl = "pln.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "JPY",
                            AverageCourseRate = 1m,
                            PhotoUrl = "jpn.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "GBP",
                            AverageCourseRate = 1m,
                            PhotoUrl = "gbp.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "CHF",
                            AverageCourseRate = 1m,
                            PhotoUrl = "chf.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "HUF",
                            AverageCourseRate = 1m,
                            PhotoUrl = "huf.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "CAD",
                            AverageCourseRate = 1m,
                            PhotoUrl = "cad.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "NOK",
                            AverageCourseRate = 1m,
                            PhotoUrl = "nok.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "BGN",
                            AverageCourseRate = 1m,
                            PhotoUrl = "bgn.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "RUB",
                            AverageCourseRate = 1m,
                            PhotoUrl = "rub.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "DKK",
                            AverageCourseRate = 1m,
                            PhotoUrl = "dkk.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "RON",
                            AverageCourseRate = 1m,
                            PhotoUrl = "ron.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "SEK",
                            AverageCourseRate = 1m,
                            PhotoUrl = "sek.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "TRY",
                            AverageCourseRate = 1m,
                            PhotoUrl = "try.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "AUD",
                            AverageCourseRate = 1m,
                            PhotoUrl = "aud.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "BRL",
                            AverageCourseRate = 1m,
                            PhotoUrl = "brl.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "CNY",
                            AverageCourseRate = 1m,
                            PhotoUrl = "cny.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "HKD",
                            AverageCourseRate = 1m,
                            PhotoUrl = "hkd.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "IDR",
                            AverageCourseRate = 1m,
                            PhotoUrl = "idr.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "ILS",
                            AverageCourseRate = 1m,
                            PhotoUrl = "ils.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "INR",
                            AverageCourseRate = 1m,
                            PhotoUrl = "inr.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "KRW",
                            AverageCourseRate = 1m,
                            PhotoUrl = "krw.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "MXN",
                            AverageCourseRate = 1m,
                            PhotoUrl = "mxn.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "MYR",
                            AverageCourseRate = 1m,
                            PhotoUrl = "myr.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "NZD",
                            AverageCourseRate = 1m,
                            PhotoUrl = "nzd.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "PHP",
                            AverageCourseRate = 1m,
                            PhotoUrl = "php.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "SGD",
                            AverageCourseRate = 1m,
                            PhotoUrl = "sgd.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "THB",
                            AverageCourseRate = 1m,
                            PhotoUrl = "thb.png",
                            Quantity = 0m,
                            Status = 0
                        },
                        new
                        {
                            Code = "ZAR",
                            AverageCourseRate = 1m,
                            PhotoUrl = "zar.png",
                            Quantity = 0m,
                            Status = 0
                        });
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Customers.CustomerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly?>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("EvidenceNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("EvidenceType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("IdentificationNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.DonationEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("CourseRate")
                        .HasColumnType("TEXT");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsCanceled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyCode");

                    b.ToTable("Donations");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.TotalBalanceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("TotalBalances");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.TransactionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("CourseRate")
                        .HasColumnType("TEXT");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsCanceled")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("QuantityForeignCurrency")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.Property<int>("TransactionType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyCode");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Customers.BusinessCustomerEntity", b =>
                {
                    b.HasBaseType("ExchangeApp.DAL.Entities.Customers.CustomerEntity");

                    b.Property<string>("ICO")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TradeAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TradeNameOfTheOwner")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("BusinessCustomers");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Customers.IndividualCustomerEntity", b =>
                {
                    b.HasBaseType("ExchangeApp.DAL.Entities.Customers.CustomerEntity");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("IndividualCustomers");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Customers.MinorCustomerEntity", b =>
                {
                    b.HasBaseType("ExchangeApp.DAL.Entities.Customers.CustomerEntity");

                    b.ToTable("MinorCustomers");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.DonationEntity", b =>
                {
                    b.HasOne("ExchangeApp.DAL.Entities.CurrencyEntity", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.TransactionEntity", b =>
                {
                    b.HasOne("ExchangeApp.DAL.Entities.CurrencyEntity", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ExchangeApp.DAL.Entities.Customers.CustomerEntity", "Customer")
                        .WithOne()
                        .HasForeignKey("ExchangeApp.DAL.Entities.TransactionEntity", "CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Currency");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Customers.BusinessCustomerEntity", b =>
                {
                    b.HasOne("ExchangeApp.DAL.Entities.Customers.CustomerEntity", null)
                        .WithOne()
                        .HasForeignKey("ExchangeApp.DAL.Entities.Customers.BusinessCustomerEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Customers.IndividualCustomerEntity", b =>
                {
                    b.HasOne("ExchangeApp.DAL.Entities.Customers.CustomerEntity", null)
                        .WithOne()
                        .HasForeignKey("ExchangeApp.DAL.Entities.Customers.IndividualCustomerEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Customers.MinorCustomerEntity", b =>
                {
                    b.HasOne("ExchangeApp.DAL.Entities.Customers.CustomerEntity", null)
                        .WithOne()
                        .HasForeignKey("ExchangeApp.DAL.Entities.Customers.MinorCustomerEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
