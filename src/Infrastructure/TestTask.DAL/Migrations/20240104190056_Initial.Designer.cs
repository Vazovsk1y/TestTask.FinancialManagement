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
    [Migration("20240104190056_Initial")]
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
                            Id = new Guid("0e564296-5a8b-4c6d-91c6-ecfbde48650b"),
                            CurrencyFromId = new Guid("eaefb5a3-e088-4e02-9302-b72bb2d41ac3"),
                            CurrencyToId = new Guid("f37ee4ac-a1d5-40e0-991a-c210608f57e8"),
                            Value = 0.1m
                        },
                        new
                        {
                            Id = new Guid("a24c421e-1f7e-443d-8062-41a38c372471"),
                            CurrencyFromId = new Guid("f37ee4ac-a1d5-40e0-991a-c210608f57e8"),
                            CurrencyToId = new Guid("eaefb5a3-e088-4e02-9302-b72bb2d41ac3"),
                            Value = 0.21m
                        },
                        new
                        {
                            Id = new Guid("5f295087-2358-4568-a3da-77155799d3e5"),
                            CurrencyFromId = new Guid("98357d6f-bac0-41f2-be65-a2395b1b191a"),
                            CurrencyToId = new Guid("eaefb5a3-e088-4e02-9302-b72bb2d41ac3"),
                            Value = 0.14m
                        },
                        new
                        {
                            Id = new Guid("a2621aeb-2d61-4aa8-85e4-9d8a8c3dbf29"),
                            CurrencyFromId = new Guid("eaefb5a3-e088-4e02-9302-b72bb2d41ac3"),
                            CurrencyToId = new Guid("98357d6f-bac0-41f2-be65-a2395b1b191a"),
                            Value = 0.08m
                        },
                        new
                        {
                            Id = new Guid("3b17bbec-6d82-4a8c-bf3f-dd8e191a2f85"),
                            CurrencyFromId = new Guid("f37ee4ac-a1d5-40e0-991a-c210608f57e8"),
                            CurrencyToId = new Guid("63bec71f-cdf6-4a74-a6b3-8573f8f6deeb"),
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
                            Id = new Guid("98357d6f-bac0-41f2-be65-a2395b1b191a"),
                            AlphabeticCode = "USD",
                            NumericCode = "840",
                            Title = "United States Dollar"
                        },
                        new
                        {
                            Id = new Guid("63bec71f-cdf6-4a74-a6b3-8573f8f6deeb"),
                            AlphabeticCode = "EUR",
                            NumericCode = "978",
                            Title = "Euro"
                        },
                        new
                        {
                            Id = new Guid("eaefb5a3-e088-4e02-9302-b72bb2d41ac3"),
                            AlphabeticCode = "UAH",
                            NumericCode = "980",
                            Title = "Ukrainian Hryvnia"
                        },
                        new
                        {
                            Id = new Guid("bcee18dd-2a87-4a22-9dcd-723d06e8bb0a"),
                            AlphabeticCode = "GBP",
                            NumericCode = "826",
                            Title = "British Pound Sterling"
                        },
                        new
                        {
                            Id = new Guid("b59103c2-3981-4040-b781-2b756ec90537"),
                            AlphabeticCode = "JPY",
                            NumericCode = "392",
                            Title = "Japanese Yen"
                        },
                        new
                        {
                            Id = new Guid("f37ee4ac-a1d5-40e0-991a-c210608f57e8"),
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
                            Id = new Guid("90b5c6dd-b33e-4834-a2ae-dbcb64a67fc3"),
                            Balance = 0m,
                            CurrencyId = new Guid("98357d6f-bac0-41f2-be65-a2395b1b191a"),
                            UserId = new Guid("d9ac5237-cf1e-4caa-9f28-577c81590bde")
                        },
                        new
                        {
                            Id = new Guid("53ae5ea7-1cc3-4e4d-8e3e-76a6d161f37f"),
                            Balance = 0m,
                            CurrencyId = new Guid("98357d6f-bac0-41f2-be65-a2395b1b191a"),
                            UserId = new Guid("6e5fa713-85b7-45b3-a449-84db569fbd68")
                        },
                        new
                        {
                            Id = new Guid("c7fcfc4c-3772-42c0-b735-e145acc5bba0"),
                            Balance = 0m,
                            CurrencyId = new Guid("63bec71f-cdf6-4a74-a6b3-8573f8f6deeb"),
                            UserId = new Guid("d9ac5237-cf1e-4caa-9f28-577c81590bde")
                        },
                        new
                        {
                            Id = new Guid("2db58c2a-7c53-471b-882b-713691d906b4"),
                            Balance = 0m,
                            CurrencyId = new Guid("63bec71f-cdf6-4a74-a6b3-8573f8f6deeb"),
                            UserId = new Guid("6e5fa713-85b7-45b3-a449-84db569fbd68")
                        },
                        new
                        {
                            Id = new Guid("331625f7-2b22-4490-8892-fa6ab53b386d"),
                            Balance = 0m,
                            CurrencyId = new Guid("eaefb5a3-e088-4e02-9302-b72bb2d41ac3"),
                            UserId = new Guid("d9ac5237-cf1e-4caa-9f28-577c81590bde")
                        },
                        new
                        {
                            Id = new Guid("22911a6c-3069-4f2b-b7e6-7f9b5671d112"),
                            Balance = 0m,
                            CurrencyId = new Guid("eaefb5a3-e088-4e02-9302-b72bb2d41ac3"),
                            UserId = new Guid("6e5fa713-85b7-45b3-a449-84db569fbd68")
                        },
                        new
                        {
                            Id = new Guid("e2230fd7-50ae-4172-97bf-399cfa885d9b"),
                            Balance = 0m,
                            CurrencyId = new Guid("bcee18dd-2a87-4a22-9dcd-723d06e8bb0a"),
                            UserId = new Guid("d9ac5237-cf1e-4caa-9f28-577c81590bde")
                        },
                        new
                        {
                            Id = new Guid("76de6af2-d93f-4ccb-8652-ea89e5735dab"),
                            Balance = 0m,
                            CurrencyId = new Guid("bcee18dd-2a87-4a22-9dcd-723d06e8bb0a"),
                            UserId = new Guid("6e5fa713-85b7-45b3-a449-84db569fbd68")
                        },
                        new
                        {
                            Id = new Guid("0fc419f4-c255-4551-894f-3e8242c67f69"),
                            Balance = 0m,
                            CurrencyId = new Guid("b59103c2-3981-4040-b781-2b756ec90537"),
                            UserId = new Guid("d9ac5237-cf1e-4caa-9f28-577c81590bde")
                        },
                        new
                        {
                            Id = new Guid("ec72f279-fa77-43de-a2a5-75e19c695edd"),
                            Balance = 0m,
                            CurrencyId = new Guid("b59103c2-3981-4040-b781-2b756ec90537"),
                            UserId = new Guid("6e5fa713-85b7-45b3-a449-84db569fbd68")
                        },
                        new
                        {
                            Id = new Guid("68126598-094b-43b9-b7d2-7f493eaf474f"),
                            Balance = 0m,
                            CurrencyId = new Guid("f37ee4ac-a1d5-40e0-991a-c210608f57e8"),
                            UserId = new Guid("d9ac5237-cf1e-4caa-9f28-577c81590bde")
                        },
                        new
                        {
                            Id = new Guid("36468dce-a1a7-463b-8785-f62a4d24f9a8"),
                            Balance = 0m,
                            CurrencyId = new Guid("f37ee4ac-a1d5-40e0-991a-c210608f57e8"),
                            UserId = new Guid("6e5fa713-85b7-45b3-a449-84db569fbd68")
                        });
                });

            modelBuilder.Entity("TestTask.Domain.Entities.MoneyOperation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CommissionId")
                        .HasColumnType("uniqueidentifier");

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

                    b.HasIndex("CommissionId");

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
                            Id = new Guid("da754e23-9a83-4b27-a888-ce2be8bdcacc"),
                            Title = "User"
                        },
                        new
                        {
                            Id = new Guid("57c91276-93d9-4b32-b468-510cf1581316"),
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
                            Id = new Guid("d9ac5237-cf1e-4caa-9f28-577c81590bde"),
                            Email = "penis@gmail.com",
                            FullName = "Mike Vazovskiy",
                            PasswordHash = "$2a$11$0N4916UYQVUVa8po.3JBSetUNxmmVflGtRWToySL2rKFMBl7HV0vW"
                        },
                        new
                        {
                            Id = new Guid("6e5fa713-85b7-45b3-a449-84db569fbd68"),
                            Email = "popka@gmail.com",
                            FullName = "John Doe",
                            PasswordHash = "$2a$11$iAmaLZfjKjvAW2loyO7syuvYNl0tL8dDUFLdUUmzR1SQQPXasXgV6"
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
                            UserId = new Guid("d9ac5237-cf1e-4caa-9f28-577c81590bde"),
                            RoleId = new Guid("da754e23-9a83-4b27-a888-ce2be8bdcacc")
                        },
                        new
                        {
                            UserId = new Guid("d9ac5237-cf1e-4caa-9f28-577c81590bde"),
                            RoleId = new Guid("57c91276-93d9-4b32-b468-510cf1581316")
                        },
                        new
                        {
                            UserId = new Guid("6e5fa713-85b7-45b3-a449-84db569fbd68"),
                            RoleId = new Guid("da754e23-9a83-4b27-a888-ce2be8bdcacc")
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

            modelBuilder.Entity("TestTask.Domain.Entities.MoneyOperation", b =>
                {
                    b.HasOne("TestTask.Domain.Entities.Commission", "Commission")
                        .WithMany()
                        .HasForeignKey("CommissionId");

                    b.Navigation("Commission");
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