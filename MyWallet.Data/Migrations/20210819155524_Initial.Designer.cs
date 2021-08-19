﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyWallet.Data;

namespace MyWallet.Data.Migrations
{
    [DbContext(typeof(MyWalletContext))]
    [Migration("20210819155524_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("MyWallet.Data.Entities.CategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(true)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(true)
                        .HasColumnType("varchar(30)");

                    b.Property<Guid?>("ParrentId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ParrentId");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("MyWallet.Data.Entities.HistoryLineEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(38,2)");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Date");

                    b.ToTable("history_lines");
                });

            modelBuilder.Entity("MyWallet.Data.Entities.CategoryEntity", b =>
                {
                    b.HasOne("MyWallet.Data.Entities.CategoryEntity", "Parrent")
                        .WithMany("Child")
                        .HasForeignKey("ParrentId");

                    b.Navigation("Parrent");
                });

            modelBuilder.Entity("MyWallet.Data.Entities.HistoryLineEntity", b =>
                {
                    b.HasOne("MyWallet.Data.Entities.CategoryEntity", "Category")
                        .WithMany("HistoryLines")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("MyWallet.Data.Entities.CategoryEntity", b =>
                {
                    b.Navigation("Child");

                    b.Navigation("HistoryLines");
                });
#pragma warning restore 612, 618
        }
    }
}
