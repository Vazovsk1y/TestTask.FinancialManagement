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
    [Migration("20240203115601_Initial")]
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

                    b.HasIndex("CurrencyFromId");

                    b.HasIndex("CurrencyToId");

                    b.ToTable("Commissions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b60bee7c-1615-406b-ab03-c0afb9ce5591"),
                            CurrencyFromId = new Guid("e1b38044-a166-4bfb-a06d-d49299185132"),
                            CurrencyToId = new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"),
                            Value = 0.1m
                        },
                        new
                        {
                            Id = new Guid("3b2f692d-5ce9-41d9-8a2a-6db10f6f62c5"),
                            CurrencyFromId = new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"),
                            CurrencyToId = new Guid("e1b38044-a166-4bfb-a06d-d49299185132"),
                            Value = 0.21m
                        },
                        new
                        {
                            Id = new Guid("429bcaba-4806-44cc-896b-ba4782ce0e85"),
                            CurrencyFromId = new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"),
                            CurrencyToId = new Guid("e1b38044-a166-4bfb-a06d-d49299185132"),
                            Value = 0.14m
                        },
                        new
                        {
                            Id = new Guid("c47ff62a-cc98-4e90-abac-0795708e4b1b"),
                            CurrencyFromId = new Guid("e1b38044-a166-4bfb-a06d-d49299185132"),
                            CurrencyToId = new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"),
                            Value = 0.08m
                        },
                        new
                        {
                            Id = new Guid("04a10de6-cdfc-47dd-98e4-9f3b6260a7c8"),
                            CurrencyFromId = new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"),
                            CurrencyToId = new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"),
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
                            Id = new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"),
                            AlphabeticCode = "USD",
                            NumericCode = "840",
                            Title = "United States Dollar"
                        },
                        new
                        {
                            Id = new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"),
                            AlphabeticCode = "EUR",
                            NumericCode = "978",
                            Title = "Euro"
                        },
                        new
                        {
                            Id = new Guid("e1b38044-a166-4bfb-a06d-d49299185132"),
                            AlphabeticCode = "UAH",
                            NumericCode = "980",
                            Title = "Ukrainian Hryvnia"
                        },
                        new
                        {
                            Id = new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"),
                            AlphabeticCode = "GBP",
                            NumericCode = "826",
                            Title = "British Pound Sterling"
                        },
                        new
                        {
                            Id = new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"),
                            AlphabeticCode = "JPY",
                            NumericCode = "392",
                            Title = "Japanese Yen"
                        },
                        new
                        {
                            Id = new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"),
                            AlphabeticCode = "RUB",
                            NumericCode = "643",
                            Title = "Russian Ruble"
                        });
                });

            modelBuilder.Entity("TestTask.Domain.Entities.ExchangeRate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CurrencyFromId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CurrencyToId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("Value")
                        .HasPrecision(4, 2)
                        .HasColumnType("decimal(4,2)");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyFromId");

                    b.HasIndex("CurrencyToId");

                    b.ToTable("ExchangeRates");

                    b.HasData(
                        new
                        {
                            Id = new Guid("aa8e74df-af4f-40cd-93e1-46b02aea55c5"),
                            CurrencyFromId = new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"),
                            CurrencyToId = new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4485), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 3.04131710054229m
                        },
                        new
                        {
                            Id = new Guid("7b93d7ed-a99a-4429-884e-4c14cac37494"),
                            CurrencyFromId = new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"),
                            CurrencyToId = new Guid("e1b38044-a166-4bfb-a06d-d49299185132"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4513), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 8.75902529581426m
                        },
                        new
                        {
                            Id = new Guid("8683c472-94c9-4cbc-b717-b81b116e408f"),
                            CurrencyFromId = new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"),
                            CurrencyToId = new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4539), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 3.01817083127321m
                        },
                        new
                        {
                            Id = new Guid("069c3e72-e9de-49ce-8c4e-1b2d7031b666"),
                            CurrencyFromId = new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"),
                            CurrencyToId = new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4542), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 9.73997466160134m
                        },
                        new
                        {
                            Id = new Guid("5d263bd4-7b79-4a16-bc63-feb12523ca3a"),
                            CurrencyFromId = new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"),
                            CurrencyToId = new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4545), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 3.10715274212143m
                        },
                        new
                        {
                            Id = new Guid("307ee3f5-5211-47b5-9368-e18607b4a306"),
                            CurrencyFromId = new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"),
                            CurrencyToId = new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4552), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 5.32684989550903m
                        },
                        new
                        {
                            Id = new Guid("37195dd2-7571-45aa-be1d-1e5fc43bc313"),
                            CurrencyFromId = new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"),
                            CurrencyToId = new Guid("e1b38044-a166-4bfb-a06d-d49299185132"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4555), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 3.25724661516603m
                        },
                        new
                        {
                            Id = new Guid("abe9bbb0-9bda-433a-abd9-177edee4de9b"),
                            CurrencyFromId = new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"),
                            CurrencyToId = new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4558), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 9.3480270036449m
                        },
                        new
                        {
                            Id = new Guid("af9601bd-3612-45b8-9a3f-b2aaea67a5d9"),
                            CurrencyFromId = new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"),
                            CurrencyToId = new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4561), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 4.009217909874m
                        },
                        new
                        {
                            Id = new Guid("b7368b98-32e9-49e7-802e-5b50ac44ae8d"),
                            CurrencyFromId = new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"),
                            CurrencyToId = new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4568), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 5.16691625400702m
                        },
                        new
                        {
                            Id = new Guid("a8c1c242-ca65-4c30-b158-869b243246e6"),
                            CurrencyFromId = new Guid("e1b38044-a166-4bfb-a06d-d49299185132"),
                            CurrencyToId = new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4574), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 6.06574013429135m
                        },
                        new
                        {
                            Id = new Guid("7b51509a-9223-4f4e-ad1e-cf9fbd47a4b2"),
                            CurrencyFromId = new Guid("e1b38044-a166-4bfb-a06d-d49299185132"),
                            CurrencyToId = new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4578), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 1.67168734679292m
                        },
                        new
                        {
                            Id = new Guid("3672ab98-7b1e-41b1-9013-c1ad7b6693ea"),
                            CurrencyFromId = new Guid("e1b38044-a166-4bfb-a06d-d49299185132"),
                            CurrencyToId = new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4582), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 2.47629842790256m
                        },
                        new
                        {
                            Id = new Guid("a2548fe5-5c77-4b48-82f6-d5e6650de220"),
                            CurrencyFromId = new Guid("e1b38044-a166-4bfb-a06d-d49299185132"),
                            CurrencyToId = new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4585), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 4.67515337237833m
                        },
                        new
                        {
                            Id = new Guid("653662c0-af6b-4fd5-84e3-7cfce1bc1601"),
                            CurrencyFromId = new Guid("e1b38044-a166-4bfb-a06d-d49299185132"),
                            CurrencyToId = new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4588), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 7.24689137534969m
                        },
                        new
                        {
                            Id = new Guid("584d9b6b-9b29-42af-9ed7-0c8c7a998544"),
                            CurrencyFromId = new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"),
                            CurrencyToId = new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4602), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 0.182055620075973m
                        },
                        new
                        {
                            Id = new Guid("201ccc8e-f42a-432d-8c64-70739c7067e9"),
                            CurrencyFromId = new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"),
                            CurrencyToId = new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4605), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 7.91525880182343m
                        },
                        new
                        {
                            Id = new Guid("34b99a25-5cb2-4deb-8c07-39928289a1a9"),
                            CurrencyFromId = new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"),
                            CurrencyToId = new Guid("e1b38044-a166-4bfb-a06d-d49299185132"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4610), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 0.956869507055197m
                        },
                        new
                        {
                            Id = new Guid("8b3266b6-34f6-46ea-b30b-2cf345fe1190"),
                            CurrencyFromId = new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"),
                            CurrencyToId = new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4618), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 9.92344216456518m
                        },
                        new
                        {
                            Id = new Guid("69dc7bd0-cd49-4650-b92a-63b11fc47eb2"),
                            CurrencyFromId = new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"),
                            CurrencyToId = new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4621), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 8.04231763353333m
                        },
                        new
                        {
                            Id = new Guid("1c0ea541-1716-4842-9057-5826fa9c5e4d"),
                            CurrencyFromId = new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"),
                            CurrencyToId = new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4624), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 3.12332149292058m
                        },
                        new
                        {
                            Id = new Guid("5ba62429-2f97-4066-abde-714492bc2e88"),
                            CurrencyFromId = new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"),
                            CurrencyToId = new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4627), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 6.35891385564513m
                        },
                        new
                        {
                            Id = new Guid("09032ef6-aab2-488b-b457-857c1bee94af"),
                            CurrencyFromId = new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"),
                            CurrencyToId = new Guid("e1b38044-a166-4bfb-a06d-d49299185132"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4630), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 5.11941632629037m
                        },
                        new
                        {
                            Id = new Guid("c05a2ff9-4507-46d3-a24c-1b7d073d9213"),
                            CurrencyFromId = new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"),
                            CurrencyToId = new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4638), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 1.21372110148081m
                        },
                        new
                        {
                            Id = new Guid("ec846390-a039-495e-8b7c-e539108d85f7"),
                            CurrencyFromId = new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"),
                            CurrencyToId = new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4641), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 7.45526810517005m
                        },
                        new
                        {
                            Id = new Guid("dbb86bea-10f5-40dc-9b9a-3a051cfdd6f0"),
                            CurrencyFromId = new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"),
                            CurrencyToId = new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4645), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 8.90861409620198m
                        },
                        new
                        {
                            Id = new Guid("f256c99c-a1a7-4683-badb-c0f548b36919"),
                            CurrencyFromId = new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"),
                            CurrencyToId = new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4651), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 8.04652734594035m
                        },
                        new
                        {
                            Id = new Guid("a1ce1795-42bc-46d3-b3a5-17e6e8dd0076"),
                            CurrencyFromId = new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"),
                            CurrencyToId = new Guid("e1b38044-a166-4bfb-a06d-d49299185132"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4654), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 0.675809731942313m
                        },
                        new
                        {
                            Id = new Guid("c145e26c-ea93-4188-b0ac-13f77c9b41a1"),
                            CurrencyFromId = new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"),
                            CurrencyToId = new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4657), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 9.14640483206101m
                        },
                        new
                        {
                            Id = new Guid("aebfba80-64af-4d91-a970-98a24ab8e639"),
                            CurrencyFromId = new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"),
                            CurrencyToId = new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"),
                            UpdatedAt = new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4691), new TimeSpan(0, 0, 0, 0, 0)),
                            Value = 3.29466090703852m
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
                            Id = new Guid("0f9c9c67-212c-4474-ac4a-98dad0a1878f"),
                            Balance = 0m,
                            CurrencyId = new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"),
                            UserId = new Guid("85bba7f4-4959-4998-9f58-e0634a839938")
                        },
                        new
                        {
                            Id = new Guid("1c15006a-0c1d-4d7d-9ca2-440070ca8d9d"),
                            Balance = 0m,
                            CurrencyId = new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"),
                            UserId = new Guid("0f35f7a8-cb06-4972-89e6-7695f115dd94")
                        },
                        new
                        {
                            Id = new Guid("442ae7aa-7f0f-4ff9-b459-27173e580eb1"),
                            Balance = 0m,
                            CurrencyId = new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"),
                            UserId = new Guid("85bba7f4-4959-4998-9f58-e0634a839938")
                        },
                        new
                        {
                            Id = new Guid("4d2c9872-1600-42c6-a63d-25a92b45b6ab"),
                            Balance = 0m,
                            CurrencyId = new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"),
                            UserId = new Guid("0f35f7a8-cb06-4972-89e6-7695f115dd94")
                        },
                        new
                        {
                            Id = new Guid("fa92c930-7254-4086-8c27-d8fb571d0655"),
                            Balance = 0m,
                            CurrencyId = new Guid("e1b38044-a166-4bfb-a06d-d49299185132"),
                            UserId = new Guid("85bba7f4-4959-4998-9f58-e0634a839938")
                        },
                        new
                        {
                            Id = new Guid("84f6ad01-bb2b-4d8b-8b11-f1324a76416a"),
                            Balance = 0m,
                            CurrencyId = new Guid("e1b38044-a166-4bfb-a06d-d49299185132"),
                            UserId = new Guid("0f35f7a8-cb06-4972-89e6-7695f115dd94")
                        },
                        new
                        {
                            Id = new Guid("d8e4868c-ac3d-4fa6-971c-8061d1702504"),
                            Balance = 0m,
                            CurrencyId = new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"),
                            UserId = new Guid("85bba7f4-4959-4998-9f58-e0634a839938")
                        },
                        new
                        {
                            Id = new Guid("8f9748db-1206-422c-a606-7338ae4b4530"),
                            Balance = 0m,
                            CurrencyId = new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"),
                            UserId = new Guid("0f35f7a8-cb06-4972-89e6-7695f115dd94")
                        },
                        new
                        {
                            Id = new Guid("613c1fa9-19e7-4123-98f0-5c6821b82d41"),
                            Balance = 0m,
                            CurrencyId = new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"),
                            UserId = new Guid("85bba7f4-4959-4998-9f58-e0634a839938")
                        },
                        new
                        {
                            Id = new Guid("f8ddb558-6cff-4a4c-b090-c2055b703087"),
                            Balance = 0m,
                            CurrencyId = new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"),
                            UserId = new Guid("0f35f7a8-cb06-4972-89e6-7695f115dd94")
                        },
                        new
                        {
                            Id = new Guid("ee21c009-1ffb-43cf-bf8e-9a21fdebdf94"),
                            Balance = 0m,
                            CurrencyId = new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"),
                            UserId = new Guid("85bba7f4-4959-4998-9f58-e0634a839938")
                        },
                        new
                        {
                            Id = new Guid("8b12f195-1cc4-46a2-bc60-8e0b8cbc58a4"),
                            Balance = 0m,
                            CurrencyId = new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"),
                            UserId = new Guid("0f35f7a8-cb06-4972-89e6-7695f115dd94")
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

                    b.HasIndex("MoneyAccountFromId");

                    b.HasIndex("MoneyAccountToId");

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
                            Id = new Guid("cb8c8584-f639-4327-8488-14d23e5b90c9"),
                            Title = "User"
                        },
                        new
                        {
                            Id = new Guid("ee71ad91-e33b-4fdd-bc28-2648873fe072"),
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
                            Id = new Guid("85bba7f4-4959-4998-9f58-e0634a839938"),
                            Email = "penis@gmail.com",
                            FullName = "Mike Vazovskiy",
                            PasswordHash = "$2a$11$eaR8t5zD.3YZyF2q2RSJg.cUhzKbQya3BVOOJXX2ncl/4mNvZp3pi"
                        },
                        new
                        {
                            Id = new Guid("0f35f7a8-cb06-4972-89e6-7695f115dd94"),
                            Email = "popka@gmail.com",
                            FullName = "John Doe",
                            PasswordHash = "$2a$11$qK/t/g9n4onvWYYi6DBTQuQTnIu0SpeCFTmSEk2NNe.o/mQTxN6hW"
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
                            UserId = new Guid("85bba7f4-4959-4998-9f58-e0634a839938"),
                            RoleId = new Guid("cb8c8584-f639-4327-8488-14d23e5b90c9")
                        },
                        new
                        {
                            UserId = new Guid("85bba7f4-4959-4998-9f58-e0634a839938"),
                            RoleId = new Guid("ee71ad91-e33b-4fdd-bc28-2648873fe072")
                        },
                        new
                        {
                            UserId = new Guid("0f35f7a8-cb06-4972-89e6-7695f115dd94"),
                            RoleId = new Guid("cb8c8584-f639-4327-8488-14d23e5b90c9")
                        });
                });

            modelBuilder.Entity("TestTask.Domain.Entities.Commission", b =>
                {
                    b.HasOne("TestTask.Domain.Entities.Currency", "From")
                        .WithMany()
                        .HasForeignKey("CurrencyFromId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TestTask.Domain.Entities.Currency", "To")
                        .WithMany()
                        .HasForeignKey("CurrencyToId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("From");

                    b.Navigation("To");
                });

            modelBuilder.Entity("TestTask.Domain.Entities.ExchangeRate", b =>
                {
                    b.HasOne("TestTask.Domain.Entities.Currency", "From")
                        .WithMany()
                        .HasForeignKey("CurrencyFromId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TestTask.Domain.Entities.Currency", "To")
                        .WithMany()
                        .HasForeignKey("CurrencyToId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("From");

                    b.Navigation("To");
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
                    b.HasOne("TestTask.Domain.Entities.MoneyAccount", "From")
                        .WithMany()
                        .HasForeignKey("MoneyAccountFromId");

                    b.HasOne("TestTask.Domain.Entities.MoneyAccount", "To")
                        .WithMany()
                        .HasForeignKey("MoneyAccountToId");

                    b.Navigation("From");

                    b.Navigation("To");
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
