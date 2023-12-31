﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TP24Entities;

#nullable disable

namespace TP24Entities.Migrations
{
    [DbContext(typeof(ReceivablesContext))]
    [Migration("20231001032421_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.22");

            modelBuilder.Entity("TP24Persistence.Models.Receivable", b =>
                {
                    b.Property<string>("Reference")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("Cancelled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClosedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DebtorAddress1")
                        .HasColumnType("TEXT");

                    b.Property<string>("DebtorAddress2")
                        .HasColumnType("TEXT");

                    b.Property<string>("DebtorCountryCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DebtorName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DebtorReference")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DebtorRegistrationNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("DebtorState")
                        .HasColumnType("TEXT");

                    b.Property<string>("DebtorTown")
                        .HasColumnType("TEXT");

                    b.Property<string>("DebtorZip")
                        .HasColumnType("TEXT");

                    b.Property<string>("DueDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("IssueDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("OpeningValue")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PaidValue")
                        .HasColumnType("TEXT");

                    b.HasKey("Reference");

                    b.ToTable("Receivables");
                });
#pragma warning restore 612, 618
        }
    }
}
