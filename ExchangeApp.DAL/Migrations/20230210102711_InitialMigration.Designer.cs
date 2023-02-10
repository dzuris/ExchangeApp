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
    [Migration("20230210102711_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("ExchangeApp.DAL.Entities.BranchEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("BranchAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("BranchPhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.CompanyEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("DIC")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ICO")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OwnerAddressCity")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OwnerAddressPSC")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OwnerAddressStreetNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OwnerTradeName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.CurrencyEntity", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<float>("MiddleCourse")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Code");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.CurrencyRatesEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<float>("AverageCourseRate")
                        .HasColumnType("REAL");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("TEXT");

                    b.Property<float?>("BuyRate")
                        .HasColumnType("REAL");

                    b.Property<float?>("BuyRateDeviation")
                        .HasColumnType("REAL");

                    b.Property<float?>("BuyRateDeviationPercent")
                        .HasColumnType("REAL");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float?>("SellRate")
                        .HasColumnType("REAL");

                    b.Property<float?>("SellRateDeviation")
                        .HasColumnType("REAL");

                    b.Property<float?>("SellRateDeviationPercent")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("CurrencyRates");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.CurrencySaleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("ActiveAboutAmount")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("CurrencyRatesEntityId")
                        .HasColumnType("TEXT");

                    b.Property<float?>("Sale")
                        .HasColumnType("REAL");

                    b.Property<float?>("SalePercent")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

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

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Persons");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PersonEntity");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.ShutterEntity", b =>
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

                    b.ToTable("Shutters");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.TransactionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Amount")
                        .HasColumnType("REAL");

                    b.Property<float>("CourseRate")
                        .HasColumnType("REAL");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.Property<int>("TransactionType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyCode");

                    b.HasIndex("CustomerId");

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

                    b.HasIndex("BranchId");

                    b.HasDiscriminator().HasValue("CustomerEntity");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Persons.EmployeeEntity", b =>
                {
                    b.HasBaseType("ExchangeApp.DAL.Entities.Persons.PersonEntity");

                    b.HasIndex("BranchId");

                    b.HasDiscriminator().HasValue("EmployeeEntity");
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

                    b.HasDiscriminator().HasValue("BusinessCustomerEntity");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Persons.Customers.IndividualCustomerEntity", b =>
                {
                    b.HasBaseType("ExchangeApp.DAL.Entities.Persons.Customers.CustomerEntity");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable(t =>
                        {
                            t.Property("Nationality")
                                .HasColumnName("IndividualCustomerEntity_Nationality");
                        });

                    b.HasDiscriminator().HasValue("IndividualCustomerEntity");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Persons.Customers.MinorCustomerEntity", b =>
                {
                    b.HasBaseType("ExchangeApp.DAL.Entities.Persons.Customers.CustomerEntity");

                    b.HasDiscriminator().HasValue("MinorCustomerEntity");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.BranchEntity", b =>
                {
                    b.HasOne("ExchangeApp.DAL.Entities.CompanyEntity", "Company")
                        .WithMany("Branches")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.DonationEntity", b =>
                {
                    b.HasOne("ExchangeApp.DAL.Entities.CurrencyEntity", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExchangeApp.DAL.Entities.Persons.EmployeeEntity", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.ShutterEntity", b =>
                {
                    b.HasOne("ExchangeApp.DAL.Entities.Persons.EmployeeEntity", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.TransactionEntity", b =>
                {
                    b.HasOne("ExchangeApp.DAL.Entities.CurrencyEntity", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExchangeApp.DAL.Entities.Persons.Customers.CustomerEntity", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("ExchangeApp.DAL.Entities.Persons.EmployeeEntity", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Customer");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Persons.Customers.CustomerEntity", b =>
                {
                    b.HasOne("ExchangeApp.DAL.Entities.BranchEntity", "Branch")
                        .WithMany("Customers")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.Persons.EmployeeEntity", b =>
                {
                    b.HasOne("ExchangeApp.DAL.Entities.BranchEntity", "Branch")
                        .WithMany("Employees")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.BranchEntity", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("ExchangeApp.DAL.Entities.CompanyEntity", b =>
                {
                    b.Navigation("Branches");
                });
#pragma warning restore 612, 618
        }
    }
}
