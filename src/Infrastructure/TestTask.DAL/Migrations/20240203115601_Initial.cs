using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestTask.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    AlphabeticCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    NumericCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Commissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyFromId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commissions_Currencies_CurrencyFromId",
                        column: x => x.CurrencyFromId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Commissions_Currencies_CurrencyToId",
                        column: x => x.CurrencyToId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyFromId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_Currencies_CurrencyFromId",
                        column: x => x.CurrencyFromId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExchangeRates_Currencies_CurrencyToId",
                        column: x => x.CurrencyToId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MoneyAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoneyAccounts_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoneyAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoneyOperations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MoneyAccountFromId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MoneyAccountToId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AppliedCommissionValue = table.Column<decimal>(type: "decimal(4,4)", precision: 4, scale: 4, nullable: false),
                    AppliedExchangeRate = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    MoneyAmount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    OperationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoveType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoneyOperations_MoneyAccounts_MoneyAccountFromId",
                        column: x => x.MoneyAccountFromId,
                        principalTable: "MoneyAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MoneyOperations_MoneyAccounts_MoneyAccountToId",
                        column: x => x.MoneyAccountToId,
                        principalTable: "MoneyAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "AlphabeticCode", "NumericCode", "Title" },
                values: new object[,]
                {
                    { new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"), "USD", "840", "United States Dollar" },
                    { new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"), "EUR", "978", "Euro" },
                    { new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"), "RUB", "643", "Russian Ruble" },
                    { new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"), "GBP", "826", "British Pound Sterling" },
                    { new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"), "JPY", "392", "Japanese Yen" },
                    { new Guid("e1b38044-a166-4bfb-a06d-d49299185132"), "UAH", "980", "Ukrainian Hryvnia" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("cb8c8584-f639-4327-8488-14d23e5b90c9"), "User" },
                    { new Guid("ee71ad91-e33b-4fdd-bc28-2648873fe072"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("0f35f7a8-cb06-4972-89e6-7695f115dd94"), "popka@gmail.com", "John Doe", "$2a$11$qK/t/g9n4onvWYYi6DBTQuQTnIu0SpeCFTmSEk2NNe.o/mQTxN6hW" },
                    { new Guid("85bba7f4-4959-4998-9f58-e0634a839938"), "penis@gmail.com", "Mike Vazovskiy", "$2a$11$eaR8t5zD.3YZyF2q2RSJg.cUhzKbQya3BVOOJXX2ncl/4mNvZp3pi" }
                });

            migrationBuilder.InsertData(
                table: "Commissions",
                columns: new[] { "Id", "CurrencyFromId", "CurrencyToId", "Value" },
                values: new object[,]
                {
                    { new Guid("04a10de6-cdfc-47dd-98e4-9f3b6260a7c8"), new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"), new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"), 0.15m },
                    { new Guid("3b2f692d-5ce9-41d9-8a2a-6db10f6f62c5"), new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"), new Guid("e1b38044-a166-4bfb-a06d-d49299185132"), 0.21m },
                    { new Guid("429bcaba-4806-44cc-896b-ba4782ce0e85"), new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"), new Guid("e1b38044-a166-4bfb-a06d-d49299185132"), 0.14m },
                    { new Guid("b60bee7c-1615-406b-ab03-c0afb9ce5591"), new Guid("e1b38044-a166-4bfb-a06d-d49299185132"), new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"), 0.1m },
                    { new Guid("c47ff62a-cc98-4e90-abac-0795708e4b1b"), new Guid("e1b38044-a166-4bfb-a06d-d49299185132"), new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"), 0.08m }
                });

            migrationBuilder.InsertData(
                table: "ExchangeRates",
                columns: new[] { "Id", "CurrencyFromId", "CurrencyToId", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { new Guid("069c3e72-e9de-49ce-8c4e-1b2d7031b666"), new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"), new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4542), new TimeSpan(0, 0, 0, 0, 0)), 9.73997466160134m },
                    { new Guid("09032ef6-aab2-488b-b457-857c1bee94af"), new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"), new Guid("e1b38044-a166-4bfb-a06d-d49299185132"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4630), new TimeSpan(0, 0, 0, 0, 0)), 5.11941632629037m },
                    { new Guid("1c0ea541-1716-4842-9057-5826fa9c5e4d"), new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"), new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4624), new TimeSpan(0, 0, 0, 0, 0)), 3.12332149292058m },
                    { new Guid("201ccc8e-f42a-432d-8c64-70739c7067e9"), new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"), new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4605), new TimeSpan(0, 0, 0, 0, 0)), 7.91525880182343m },
                    { new Guid("307ee3f5-5211-47b5-9368-e18607b4a306"), new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"), new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4552), new TimeSpan(0, 0, 0, 0, 0)), 5.32684989550903m },
                    { new Guid("34b99a25-5cb2-4deb-8c07-39928289a1a9"), new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"), new Guid("e1b38044-a166-4bfb-a06d-d49299185132"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4610), new TimeSpan(0, 0, 0, 0, 0)), 0.956869507055197m },
                    { new Guid("3672ab98-7b1e-41b1-9013-c1ad7b6693ea"), new Guid("e1b38044-a166-4bfb-a06d-d49299185132"), new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4582), new TimeSpan(0, 0, 0, 0, 0)), 2.47629842790256m },
                    { new Guid("37195dd2-7571-45aa-be1d-1e5fc43bc313"), new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"), new Guid("e1b38044-a166-4bfb-a06d-d49299185132"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4555), new TimeSpan(0, 0, 0, 0, 0)), 3.25724661516603m },
                    { new Guid("584d9b6b-9b29-42af-9ed7-0c8c7a998544"), new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"), new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4602), new TimeSpan(0, 0, 0, 0, 0)), 0.182055620075973m },
                    { new Guid("5ba62429-2f97-4066-abde-714492bc2e88"), new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"), new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4627), new TimeSpan(0, 0, 0, 0, 0)), 6.35891385564513m },
                    { new Guid("5d263bd4-7b79-4a16-bc63-feb12523ca3a"), new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"), new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4545), new TimeSpan(0, 0, 0, 0, 0)), 3.10715274212143m },
                    { new Guid("653662c0-af6b-4fd5-84e3-7cfce1bc1601"), new Guid("e1b38044-a166-4bfb-a06d-d49299185132"), new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4588), new TimeSpan(0, 0, 0, 0, 0)), 7.24689137534969m },
                    { new Guid("69dc7bd0-cd49-4650-b92a-63b11fc47eb2"), new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"), new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4621), new TimeSpan(0, 0, 0, 0, 0)), 8.04231763353333m },
                    { new Guid("7b51509a-9223-4f4e-ad1e-cf9fbd47a4b2"), new Guid("e1b38044-a166-4bfb-a06d-d49299185132"), new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4578), new TimeSpan(0, 0, 0, 0, 0)), 1.67168734679292m },
                    { new Guid("7b93d7ed-a99a-4429-884e-4c14cac37494"), new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"), new Guid("e1b38044-a166-4bfb-a06d-d49299185132"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4513), new TimeSpan(0, 0, 0, 0, 0)), 8.75902529581426m },
                    { new Guid("8683c472-94c9-4cbc-b717-b81b116e408f"), new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"), new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4539), new TimeSpan(0, 0, 0, 0, 0)), 3.01817083127321m },
                    { new Guid("8b3266b6-34f6-46ea-b30b-2cf345fe1190"), new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"), new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4618), new TimeSpan(0, 0, 0, 0, 0)), 9.92344216456518m },
                    { new Guid("a1ce1795-42bc-46d3-b3a5-17e6e8dd0076"), new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"), new Guid("e1b38044-a166-4bfb-a06d-d49299185132"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4654), new TimeSpan(0, 0, 0, 0, 0)), 0.675809731942313m },
                    { new Guid("a2548fe5-5c77-4b48-82f6-d5e6650de220"), new Guid("e1b38044-a166-4bfb-a06d-d49299185132"), new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4585), new TimeSpan(0, 0, 0, 0, 0)), 4.67515337237833m },
                    { new Guid("a8c1c242-ca65-4c30-b158-869b243246e6"), new Guid("e1b38044-a166-4bfb-a06d-d49299185132"), new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4574), new TimeSpan(0, 0, 0, 0, 0)), 6.06574013429135m },
                    { new Guid("aa8e74df-af4f-40cd-93e1-46b02aea55c5"), new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"), new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4485), new TimeSpan(0, 0, 0, 0, 0)), 3.04131710054229m },
                    { new Guid("abe9bbb0-9bda-433a-abd9-177edee4de9b"), new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"), new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4558), new TimeSpan(0, 0, 0, 0, 0)), 9.3480270036449m },
                    { new Guid("aebfba80-64af-4d91-a970-98a24ab8e639"), new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"), new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4691), new TimeSpan(0, 0, 0, 0, 0)), 3.29466090703852m },
                    { new Guid("af9601bd-3612-45b8-9a3f-b2aaea67a5d9"), new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"), new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4561), new TimeSpan(0, 0, 0, 0, 0)), 4.009217909874m },
                    { new Guid("b7368b98-32e9-49e7-802e-5b50ac44ae8d"), new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"), new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4568), new TimeSpan(0, 0, 0, 0, 0)), 5.16691625400702m },
                    { new Guid("c05a2ff9-4507-46d3-a24c-1b7d073d9213"), new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"), new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4638), new TimeSpan(0, 0, 0, 0, 0)), 1.21372110148081m },
                    { new Guid("c145e26c-ea93-4188-b0ac-13f77c9b41a1"), new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"), new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4657), new TimeSpan(0, 0, 0, 0, 0)), 9.14640483206101m },
                    { new Guid("dbb86bea-10f5-40dc-9b9a-3a051cfdd6f0"), new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"), new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4645), new TimeSpan(0, 0, 0, 0, 0)), 8.90861409620198m },
                    { new Guid("ec846390-a039-495e-8b7c-e539108d85f7"), new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"), new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4641), new TimeSpan(0, 0, 0, 0, 0)), 7.45526810517005m },
                    { new Guid("f256c99c-a1a7-4683-badb-c0f548b36919"), new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"), new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"), new DateTimeOffset(new DateTime(2024, 2, 3, 11, 56, 0, 439, DateTimeKind.Unspecified).AddTicks(4651), new TimeSpan(0, 0, 0, 0, 0)), 8.04652734594035m }
                });

            migrationBuilder.InsertData(
                table: "MoneyAccounts",
                columns: new[] { "Id", "Balance", "CurrencyId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0f9c9c67-212c-4474-ac4a-98dad0a1878f"), 0m, new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"), new Guid("85bba7f4-4959-4998-9f58-e0634a839938") },
                    { new Guid("1c15006a-0c1d-4d7d-9ca2-440070ca8d9d"), 0m, new Guid("122a22c8-a103-4f47-88dc-ad3a5de3b4a0"), new Guid("0f35f7a8-cb06-4972-89e6-7695f115dd94") },
                    { new Guid("442ae7aa-7f0f-4ff9-b459-27173e580eb1"), 0m, new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"), new Guid("85bba7f4-4959-4998-9f58-e0634a839938") },
                    { new Guid("4d2c9872-1600-42c6-a63d-25a92b45b6ab"), 0m, new Guid("160e499c-c882-4a34-a78f-e21796f94c1b"), new Guid("0f35f7a8-cb06-4972-89e6-7695f115dd94") },
                    { new Guid("613c1fa9-19e7-4123-98f0-5c6821b82d41"), 0m, new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"), new Guid("85bba7f4-4959-4998-9f58-e0634a839938") },
                    { new Guid("84f6ad01-bb2b-4d8b-8b11-f1324a76416a"), 0m, new Guid("e1b38044-a166-4bfb-a06d-d49299185132"), new Guid("0f35f7a8-cb06-4972-89e6-7695f115dd94") },
                    { new Guid("8b12f195-1cc4-46a2-bc60-8e0b8cbc58a4"), 0m, new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"), new Guid("0f35f7a8-cb06-4972-89e6-7695f115dd94") },
                    { new Guid("8f9748db-1206-422c-a606-7338ae4b4530"), 0m, new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"), new Guid("0f35f7a8-cb06-4972-89e6-7695f115dd94") },
                    { new Guid("d8e4868c-ac3d-4fa6-971c-8061d1702504"), 0m, new Guid("70ca9992-36e2-4634-a652-eb1f7419d3dd"), new Guid("85bba7f4-4959-4998-9f58-e0634a839938") },
                    { new Guid("ee21c009-1ffb-43cf-bf8e-9a21fdebdf94"), 0m, new Guid("4d62f294-8cfb-46b6-b6cc-27a6035be3b6"), new Guid("85bba7f4-4959-4998-9f58-e0634a839938") },
                    { new Guid("f8ddb558-6cff-4a4c-b090-c2055b703087"), 0m, new Guid("7bdaf9f8-6c3f-44c3-8bcb-fb95a1c68d25"), new Guid("0f35f7a8-cb06-4972-89e6-7695f115dd94") },
                    { new Guid("fa92c930-7254-4086-8c27-d8fb571d0655"), 0m, new Guid("e1b38044-a166-4bfb-a06d-d49299185132"), new Guid("85bba7f4-4959-4998-9f58-e0634a839938") }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("cb8c8584-f639-4327-8488-14d23e5b90c9"), new Guid("0f35f7a8-cb06-4972-89e6-7695f115dd94") },
                    { new Guid("cb8c8584-f639-4327-8488-14d23e5b90c9"), new Guid("85bba7f4-4959-4998-9f58-e0634a839938") },
                    { new Guid("ee71ad91-e33b-4fdd-bc28-2648873fe072"), new Guid("85bba7f4-4959-4998-9f58-e0634a839938") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commissions_CurrencyFromId",
                table: "Commissions",
                column: "CurrencyFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Commissions_CurrencyToId",
                table: "Commissions",
                column: "CurrencyToId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_AlphabeticCode",
                table: "Currencies",
                column: "AlphabeticCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_NumericCode",
                table: "Currencies",
                column: "NumericCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Title",
                table: "Currencies",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_CurrencyFromId",
                table: "ExchangeRates",
                column: "CurrencyFromId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_CurrencyToId",
                table: "ExchangeRates",
                column: "CurrencyToId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyAccounts_CurrencyId",
                table: "MoneyAccounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyAccounts_UserId",
                table: "MoneyAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyOperations_MoneyAccountFromId",
                table: "MoneyOperations",
                column: "MoneyAccountFromId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyOperations_MoneyAccountToId",
                table: "MoneyOperations",
                column: "MoneyAccountToId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Title",
                table: "Roles",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commissions");

            migrationBuilder.DropTable(
                name: "ExchangeRates");

            migrationBuilder.DropTable(
                name: "MoneyOperations");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "MoneyAccounts");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
