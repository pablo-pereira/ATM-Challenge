﻿// <auto-generated />
using System;
using ATM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ATM.Migrations
{
    [DbContext(typeof(ATMContext))]
    [Migration("20201118084734_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ATM.Models.Cards", b =>
                {
                    b.Property<int>("CardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Balance")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsLoked")
                        .HasColumnType("bit");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("varchar(16)")
                        .HasMaxLength(16)
                        .IsUnicode(false);

                    b.Property<int>("Pin")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidDate")
                        .HasColumnType("date");

                    b.HasKey("CardId");

                    b.HasIndex("UserId");

                    b.ToTable("Cards");

                    b.HasData(
                        new
                        {
                            CardId = 1,
                            Balance = 35000L,
                            IsLoked = false,
                            Number = "4525111122223333",
                            Pin = 1234,
                            UserId = 1,
                            ValidDate = new DateTime(2021, 11, 17, 5, 47, 33, 829, DateTimeKind.Local).AddTicks(2245)
                        });
                });

            modelBuilder.Entity("ATM.Models.Operations", b =>
                {
                    b.Property<int>("OperationId")
                        .HasColumnType("int");

                    b.Property<int?>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOperation")
                        .HasColumnType("date");

                    b.Property<int>("OperationType")
                        .HasColumnType("int");

                    b.HasKey("OperationId");

                    b.HasIndex("CardId");

                    b.ToTable("Operations");

                    b.HasData(
                        new
                        {
                            OperationId = 1,
                            Amount = 1000,
                            CardId = 1,
                            DateOperation = new DateTime(2020, 11, 17, 5, 47, 33, 829, DateTimeKind.Local).AddTicks(2245),
                            OperationType = 0
                        });
                });

            modelBuilder.Entity("ATM.Models.Users", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Name = "Pablo"
                        });
                });

            modelBuilder.Entity("ATM.Models.Cards", b =>
                {
                    b.HasOne("ATM.Models.Users", "User")
                        .WithMany("Cards")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Cards_Users")
                        .IsRequired();
                });

            modelBuilder.Entity("ATM.Models.Operations", b =>
                {
                    b.HasOne("ATM.Models.Cards", "Card")
                        .WithMany("Operations")
                        .HasForeignKey("CardId")
                        .HasConstraintName("FK_Operations_Cards")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
