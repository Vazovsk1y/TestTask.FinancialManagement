﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestTask.DAL;

#nullable disable

namespace TestTask.DAL.Migrations
{
    [DbContext(typeof(TestTaskDbContext))]
    [Migration("20240107185652_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TestTask.Domain.Entities.Commission", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CurrencyFromId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CurrencyToId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Value")
                        .HasPrecision(4, 2)
                        .HasColumnType("decimal(4,2)");

                    b.HasKey("Id");

                    b.ToTable("Commissions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5ae2e435-60d4-4d50-9726-16ce7bc81421"),
                            CurrencyFromId = new Guid("493fe233-6f2e-4254-8329-6f552a5d5afa"),
                            CurrencyToId = new Guid("fc2b4267-b924-464f-83d0-60a139438f84"),
                            Value = 0.1m
                        },
                        new
                        {
                            Id = new Guid("b9e6d347-afd5-4f32-83e3-13331b5157c8"),
                            CurrencyFromId = new Guid("fc2b4267-b924-464f-83d0-60a139438f84"),
                            CurrencyToId = new Guid("493fe233-6f2e-4254-8329-6f552a5d5afa"),
                            Value = 0.21m
                        },
                        new
                        {
                            Id = new Guid("3849c2af-ee6e-41b2-a099-c8e5bf25d10a"),
                            CurrencyFromId = new Guid("399cdcff-124c-4a25-bcec-57ce228313cf"),
                            CurrencyToId = new Guid("493fe233-6f2e-4254-8329-6f552a5d5afa"),
                            Value = 0.14m
                        },
                        new
                        {
                            Id = new Guid("e04dde74-2a0a-4568-806f-5d60333ff2a1"),
                            CurrencyFromId = new Guid("493fe233-6f2e-4254-8329-6f552a5d5afa"),
                            CurrencyToId = new Guid("399cdcff-124c-4a25-bcec-57ce228313cf"),
                            Value = 0.08m
                        },
                        new
                        {
                            Id = new Guid("72b457a7-a6ce-4db5-b73e-6b16b7f756dd"),
                            CurrencyFromId = new Guid("fc2b4267-b924-464f-83d0-60a139438f84"),
                            CurrencyToId = new Guid("a5b7f1c0-dda9-46fe-b8ee-772fcb03c958"),
                            Value = 0.15m
                        });
                });

            modelBuilder.Entity("TestTask.Domain.Entities.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AlphabeticCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("NumericCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("Id");

                    b.HasIndex("AlphabeticCode")
                        .IsUnique();

                    b.HasIndex("NumericCode")
                        .IsUnique();

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("399cdcff-124c-4a25-bcec-57ce228313cf"),
                            AlphabeticCode = "USD",
                            NumericCode = "840",
                            Title = "United States Dollar"
                        },
                        new
                        {
                            Id = new Guid("a5b7f1c0-dda9-46fe-b8ee-772fcb03c958"),
                            AlphabeticCode = "EUR",
                            NumericCode = "978",
                            Title = "Euro"
                        },
                        new
                        {
                            Id = new Guid("493fe233-6f2e-4254-8329-6f552a5d5afa"),
                            AlphabeticCode = "UAH",
                            NumericCode = "980",
                            Title = "Ukrainian Hryvnia"
                        },
                        new
                        {
                            Id = new Guid("d77e172a-6405-4468-878f-a9957a8d71a6"),
                            AlphabeticCode = "GBP",
                            NumericCode = "826",
                            Title = "British Pound Sterling"
                        },
                        new
                        {
                            Id = new Guid("7ea95efd-0b61-4288-b618-b576af4c9fcd"),
                            AlphabeticCode = "JPY",
                            NumericCode = "392",
                            Title = "Japanese Yen"
                        },
                        new
                        {
                            Id = new Guid("fc2b4267-b924-464f-83d0-60a139438f84"),
                            AlphabeticCode = "RUB",
                            NumericCode = "643",
                            Title = "Russian Ruble"
                        });
                });

            modelBuilder.Entity("TestTask.Domain.Entities.MoneyAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Balance")
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal(19,4)");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("UserId");

                    b.ToTable("MoneyAccounts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6733d192-e8b0-4c15-a731-f7251adad67a"),
                            Balance = 0m,
                            CurrencyId = new Guid("399cdcff-124c-4a25-bcec-57ce228313cf"),
                            UserId = new Guid("1f07aed5-aabf-4bf1-9361-4b13d22f8736")
                        },
                        new
                        {
                            Id = new Guid("c7481c2a-e3a0-4713-8ad1-ec9e0a4793dc"),
                            Balance = 0m,
                            CurrencyId = new Guid("399cdcff-124c-4a25-bcec-57ce228313cf"),
                            UserId = new Guid("4583d3c6-ec37-475f-881d-af4e8836f056")
                        },
                        new
                        {
                            Id = new Guid("3765d195-bd79-47da-82bf-9ae1f2b1b36d"),
                            Balance = 0m,
                            CurrencyId = new Guid("a5b7f1c0-dda9-46fe-b8ee-772fcb03c958"),
                            UserId = new Guid("1f07aed5-aabf-4bf1-9361-4b13d22f8736")
                        },
                        new
                        {
                            Id = new Guid("7a18c0ec-2084-436b-872d-229de74d5f11"),
                            Balance = 0m,
                            CurrencyId = new Guid("a5b7f1c0-dda9-46fe-b8ee-772fcb03c958"),
                            UserId = new Guid("4583d3c6-ec37-475f-881d-af4e8836f056")
                        },
                        new
                        {
                            Id = new Guid("8e2b2013-2662-445c-9c43-7442aa36120b"),
                            Balance = 0m,
                            CurrencyId = new Guid("493fe233-6f2e-4254-8329-6f552a5d5afa"),
                            UserId = new Guid("1f07aed5-aabf-4bf1-9361-4b13d22f8736")
                        },
                        new
                        {
                            Id = new Guid("a47e0d4a-7da7-4ed4-bf25-c1e1bf0d2b0b"),
                            Balance = 0m,
                            CurrencyId = new Guid("493fe233-6f2e-4254-8329-6f552a5d5afa"),
                            UserId = new Guid("4583d3c6-ec37-475f-881d-af4e8836f056")
                        },
                        new
                        {
                            Id = new Guid("038fda73-ea1e-46a2-92f5-661131dc8633"),
                            Balance = 0m,
                            CurrencyId = new Guid("d77e172a-6405-4468-878f-a9957a8d71a6"),
                            UserId = new Guid("1f07aed5-aabf-4bf1-9361-4b13d22f8736")
                        },
                        new
                        {
                            Id = new Guid("5f1175ab-962d-43ad-9c8d-de00f7ddfa95"),
                            Balance = 0m,
                            CurrencyId = new Guid("d77e172a-6405-4468-878f-a9957a8d71a6"),
                            UserId = new Guid("4583d3c6-ec37-475f-881d-af4e8836f056")
                        },
                        new
                        {
                            Id = new Guid("ba209627-3a7e-4397-b9ed-9b254c3c4e98"),
                            Balance = 0m,
                            CurrencyId = new Guid("7ea95efd-0b61-4288-b618-b576af4c9fcd"),
                            UserId = new Guid("1f07aed5-aabf-4bf1-9361-4b13d22f8736")
                        },
                        new
                        {
                            Id = new Guid("381d6dfa-b38a-4843-8d23-a66be218fcbe"),
                            Balance = 0m,
                            CurrencyId = new Guid("7ea95efd-0b61-4288-b618-b576af4c9fcd"),
                            UserId = new Guid("4583d3c6-ec37-475f-881d-af4e8836f056")
                        },
                        new
                        {
                            Id = new Guid("c57a479f-eb03-4764-8606-6c2697df3051"),
                            Balance = 0m,
                            CurrencyId = new Guid("fc2b4267-b924-464f-83d0-60a139438f84"),
                            UserId = new Guid("1f07aed5-aabf-4bf1-9361-4b13d22f8736")
                        },
                        new
                        {
                            Id = new Guid("992974d5-3de6-4326-901b-3aee87b003a1"),
                            Balance = 0m,
                            CurrencyId = new Guid("fc2b4267-b924-464f-83d0-60a139438f84"),
                            UserId = new Guid("4583d3c6-ec37-475f-881d-af4e8836f056")
                        });
                });

            modelBuilder.Entity("TestTask.Domain.Entities.MoneyOperation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AppliedCommissionValue")
                        .HasPrecision(4, 4)
                        .HasColumnType("decimal(4,4)");

                    b.Property<decimal>("AppliedExchangeRate")
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal(19,4)");

                    b.Property<Guid?>("MoneyAccountFromId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MoneyAccountToId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("MoneyAmount")
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal(19,4)");

                    b.Property<string>("MoveType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("OperationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("OperationType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MoneyOperations");
                });

            modelBuilder.Entity("TestTask.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0991d748-59a9-4749-9a3b-41075b73de74"),
                            Title = "User"
                        },
                        new
                        {
                            Id = new Guid("83742101-eaef-4efb-b488-1288320ac083"),
                            Title = "Admin"
                        });
                });

            modelBuilder.Entity("TestTask.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1f07aed5-aabf-4bf1-9361-4b13d22f8736"),
                            Email = "penis@gmail.com",
                            FullName = "Mike Vazovskiy",
                            PasswordHash = "$2a$11$ISB8FqPJVNnu8oNVV9n5T.HkKOSb6HlG8lyvPBpp05oSivnLprKN2"
                        },
                        new
                        {
                            Id = new Guid("4583d3c6-ec37-475f-881d-af4e8836f056"),
                            Email = "popka@gmail.com",
                            FullName = "John Doe",
                            PasswordHash = "$2a$11$nc5WXaDZ9M7klakIa3ngnO1CtEnQbyJ2GTsrPD7ABfCKnqM/JWjPq"
                        });
                });

            modelBuilder.Entity("TestTask.Domain.Entities.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("1f07aed5-aabf-4bf1-9361-4b13d22f8736"),
                            RoleId = new Guid("0991d748-59a9-4749-9a3b-41075b73de74")
                        },
                        new
                        {
                            UserId = new Guid("1f07aed5-aabf-4bf1-9361-4b13d22f8736"),
                            RoleId = new Guid("83742101-eaef-4efb-b488-1288320ac083")
                        },
                        new
                        {
                            UserId = new Guid("4583d3c6-ec37-475f-881d-af4e8836f056"),
                            RoleId = new Guid("0991d748-59a9-4749-9a3b-41075b73de74")
                        });
                });

            modelBuilder.Entity("TestTask.Domain.Entities.MoneyAccount", b =>
                {
                    b.HasOne("TestTask.Domain.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestTask.Domain.Entities.User", "User")
                        .WithMany("MoneyAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TestTask.Domain.Entities.UserRole", b =>
                {
                    b.HasOne("TestTask.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestTask.Domain.Entities.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TestTask.Domain.Entities.User", b =>
                {
                    b.Navigation("MoneyAccounts");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}