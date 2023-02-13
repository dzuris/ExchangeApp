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
    [Migration("20230212200700_CurrencyAmountToQuantityRename")]
    partial class CurrencyAmountToQuantityRename
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

                    b.Property<float>("AverageCourseRate")
                        .HasColumnType("REAL");

                    b.Property<float?>("BuyRate")
                        .HasColumnType("REAL");

                    b.Property<float?>("BuyRateDeviation")
                        .HasColumnType("REAL");

                    b.Property<float?>("BuyRateDeviationPercent")
                        .HasColumnType("REAL");

                    b.Property<float>("MiddleCourse")
                        .HasColumnType("REAL");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Quantity")
                        .HasColumnType("REAL");

                    b.Property<float?>("SellRate")
                        .HasColumnType("REAL");

                    b.Property<float?>("SellRateDeviation")
                        .HasColumnType("REAL");

                    b.Property<float?>("SellRateDeviationPercent")
                        .HasColumnType("REAL");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Code");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Code = "EUR",
                            AverageCourseRate = 1f,
                            MiddleCourse = 1f,
                            PhotoUrl = "eur.png",
                            Quantity = 0f,
                            State = "Európska menová únia"
                        },
                        new
                        {
                            Code = "CZK",
                            AverageCourseRate = 1f,
                            MiddleCourse = 1f,
                            PhotoUrl = "czk.png",
                            Quantity = 0f,
                            State = "Česko"
                        },
                        new
                        {
                            Code = "USD",
                            AverageCourseRate = 1f,
                            MiddleCourse = 1f,
                            PhotoUrl = "usd.png",
                            Quantity = 0f,
                            State = "Spojené štáty americké"
                        },
                        new
                        {
                            Code = "PLN",
                            AverageCourseRate = 1f,
                            MiddleCourse = 1f,
                            PhotoUrl = "pln.png",
                            Quantity = 0f,
                            State = "Poľsko"
                        });
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.CurrencySaleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("ActiveAboutAmount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float?>("Sale")
                        .HasColumnType("REAL");

                    b.Property<float?>("SalePercent")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyCode");

                    b.ToTable("CurrencySales");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.DonationEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("CourseRate")
                        .HasColumnType("REAL");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Quantity")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyCode");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Donations");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Persons.PersonEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Persons");

                    b.UseTptMappingStrategy();
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

                    b.HasIndex("EmployeeId");

                    b.ToTable("TotalBalances");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.TransactionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("CourseRate")
                        .HasColumnType("REAL");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("TEXT");

                    b.Property<float>("Quantity")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.Property<int>("TransactionType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyCode");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Persons.Customers.CustomerEntity", b =>
                {
                    b.HasBaseType("ExchangeApp.DAL.Entities.Persons.PersonEntity");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly?>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("EvidenceNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("EvidenceType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("IdentificationNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("TransactionId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("TransactionId")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Persons.EmployeeEntity", b =>
                {
                    b.HasBaseType("ExchangeApp.DAL.Entities.Persons.PersonEntity");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Persons.Customers.BusinessCustomerEntity", b =>
                {
                    b.HasBaseType("ExchangeApp.DAL.Entities.Persons.Customers.CustomerEntity");

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

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Persons.Customers.IndividualCustomerEntity", b =>
                {
                    b.HasBaseType("ExchangeApp.DAL.Entities.Persons.Customers.CustomerEntity");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("IndividualCustomers");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Persons.Customers.MinorCustomerEntity", b =>
                {
                    b.HasBaseType("ExchangeApp.DAL.Entities.Persons.Customers.CustomerEntity");

                    b.ToTable("MinorCustomers");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.CurrencySaleEntity", b =>
                {
                    b.HasOne("ExchangeApp.DAL.Entities.CurrencyEntity", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.DonationEntity", b =>
                {
                    b.HasOne("ExchangeApp.DAL.Entities.CurrencyEntity", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ExchangeApp.DAL.Entities.Persons.EmployeeEntity", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.TotalBalanceEntity", b =>
                {
                    b.HasOne("ExchangeApp.DAL.Entities.Persons.EmployeeEntity", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.TransactionEntity", b =>
                {
                    b.HasOne("ExchangeApp.DAL.Entities.CurrencyEntity", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ExchangeApp.DAL.Entities.Persons.EmployeeEntity", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Persons.Customers.CustomerEntity", b =>
                {
                    b.HasOne("ExchangeApp.DAL.Entities.Persons.PersonEntity", null)
                        .WithOne()
                        .HasForeignKey("ExchangeApp.DAL.Entities.Persons.Customers.CustomerEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExchangeApp.DAL.Entities.TransactionEntity", "Transaction")
                        .WithOne("Customer")
                        .HasForeignKey("ExchangeApp.DAL.Entities.Persons.Customers.CustomerEntity", "TransactionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Persons.EmployeeEntity", b =>
                {
                    b.HasOne("ExchangeApp.DAL.Entities.Persons.PersonEntity", null)
                        .WithOne()
                        .HasForeignKey("ExchangeApp.DAL.Entities.Persons.EmployeeEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Persons.Customers.BusinessCustomerEntity", b =>
                {
                    b.HasOne("ExchangeApp.DAL.Entities.Persons.Customers.CustomerEntity", null)
                        .WithOne()
                        .HasForeignKey("ExchangeApp.DAL.Entities.Persons.Customers.BusinessCustomerEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Persons.Customers.IndividualCustomerEntity", b =>
                {
                    b.HasOne("ExchangeApp.DAL.Entities.Persons.Customers.CustomerEntity", null)
                        .WithOne()
                        .HasForeignKey("ExchangeApp.DAL.Entities.Persons.Customers.IndividualCustomerEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Persons.Customers.MinorCustomerEntity", b =>
                {
                    b.HasOne("ExchangeApp.DAL.Entities.Persons.Customers.CustomerEntity", null)
                        .WithOne()
                        .HasForeignKey("ExchangeApp.DAL.Entities.Persons.Customers.MinorCustomerEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.TransactionEntity", b =>
                {
                    b.Navigation("Customer");
                });
#pragma warning restore 612, 618
        }
    }
}
