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
                    { new Guid("10d05faf-e87e-41a9-bbbd-b94bb4fb1c33"), "AED", "784", "UAE Dirham" },
                    { new Guid("4f0be9f2-cb66-4d32-8f67-cf6b40497c94"), "AMD", "51", "Armenian Dram" },
                    { new Guid("ad1f6b71-30c7-49fa-9350-bf74ed785e23"), "ANG", "532", "Netherlands Antillian Guilder" },
                    { new Guid("b0ea83ca-4260-44a9-ac40-f24dd285032b"), "AFN", "971", "Afghan Afghani" },
                    { new Guid("f632d840-ff58-42bc-ab8f-a2c9de53bdee"), "ALL", "8", "Albanian Lek" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("b6bf79ee-74ae-4c7d-bf44-4e6897fe8e65"), "Admin" },
                    { new Guid("e9956abf-21bb-47ef-ac56-c0ea1300a813"), "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("20505800-43c3-43c4-81c6-b028d33421c9"), "penis@gmail.com", "Mike Vazovskiy", "$2a$11$estu3icfrbj8ipgM92N73.UHyTWE6rLdxDOiFoeKmUAFkKAoFPJIi" },
                    { new Guid("5f876f3f-0653-4f65-94da-1c6d61cc9855"), "popka@gmail.com", "John Doe", "$2a$11$mGspcWTKRctWsE3bzYZ2Zu6pmBF2zV9b.xYf38D5AQ.lnlsW.5zPq" }
                });

            migrationBuilder.InsertData(
                table: "Commissions",
                columns: new[] { "Id", "CurrencyFromId", "CurrencyToId", "Value" },
                values: new object[,]
                {
                    { new Guid("0ca984e5-0457-40ee-9cf5-425fabd861ea"), new Guid("10d05faf-e87e-41a9-bbbd-b94bb4fb1c33"), new Guid("4f0be9f2-cb66-4d32-8f67-cf6b40497c94"), 2.72429603119848m },
                    { new Guid("147ce461-c1b1-415c-8e68-c3677f105ed0"), new Guid("f632d840-ff58-42bc-ab8f-a2c9de53bdee"), new Guid("10d05faf-e87e-41a9-bbbd-b94bb4fb1c33"), 5.88291618177289m },
                    { new Guid("1eee8195-ddb3-4814-bb49-b4c7f848e109"), new Guid("ad1f6b71-30c7-49fa-9350-bf74ed785e23"), new Guid("10d05faf-e87e-41a9-bbbd-b94bb4fb1c33"), 3.67050007044066m },
                    { new Guid("21a497d8-5a31-4f54-9b14-7c4da02242ef"), new Guid("f632d840-ff58-42bc-ab8f-a2c9de53bdee"), new Guid("b0ea83ca-4260-44a9-ac40-f24dd285032b"), 7.35019691442417m },
                    { new Guid("28525077-0382-49c2-87dc-ac0f2f8b4578"), new Guid("4f0be9f2-cb66-4d32-8f67-cf6b40497c94"), new Guid("10d05faf-e87e-41a9-bbbd-b94bb4fb1c33"), 0.886309093455327m },
                    { new Guid("50de7a08-107e-4a48-8304-6130d655f988"), new Guid("4f0be9f2-cb66-4d32-8f67-cf6b40497c94"), new Guid("ad1f6b71-30c7-49fa-9350-bf74ed785e23"), 0.691888693851692m },
                    { new Guid("51c191a0-a364-42af-b000-5d0375cb808e"), new Guid("b0ea83ca-4260-44a9-ac40-f24dd285032b"), new Guid("ad1f6b71-30c7-49fa-9350-bf74ed785e23"), 8.99749947318829m },
                    { new Guid("59c2c10d-bdf3-45f5-a0f5-ff5e8e5c7a72"), new Guid("b0ea83ca-4260-44a9-ac40-f24dd285032b"), new Guid("10d05faf-e87e-41a9-bbbd-b94bb4fb1c33"), 5.51914896135217m },
                    { new Guid("6395e4e5-1f4b-4cdc-b2f5-eb4df0f71532"), new Guid("10d05faf-e87e-41a9-bbbd-b94bb4fb1c33"), new Guid("f632d840-ff58-42bc-ab8f-a2c9de53bdee"), 1.8676034484307m },
                    { new Guid("65416614-a71a-4fa6-9a90-83eb713d7185"), new Guid("10d05faf-e87e-41a9-bbbd-b94bb4fb1c33"), new Guid("ad1f6b71-30c7-49fa-9350-bf74ed785e23"), 3.45448064881854m },
                    { new Guid("8ccb820f-0785-4b72-86b2-241ba429ce85"), new Guid("ad1f6b71-30c7-49fa-9350-bf74ed785e23"), new Guid("f632d840-ff58-42bc-ab8f-a2c9de53bdee"), 1.66359018752944m },
                    { new Guid("920b2599-e782-465a-b7ef-a9bd2f136041"), new Guid("f632d840-ff58-42bc-ab8f-a2c9de53bdee"), new Guid("4f0be9f2-cb66-4d32-8f67-cf6b40497c94"), 5.79770773882617m },
                    { new Guid("95c3a3fc-cdcf-4288-8991-e64f6a5cd5be"), new Guid("10d05faf-e87e-41a9-bbbd-b94bb4fb1c33"), new Guid("b0ea83ca-4260-44a9-ac40-f24dd285032b"), 1.47567225742711m },
                    { new Guid("9848f644-9e43-4bfa-9141-92f52388b606"), new Guid("ad1f6b71-30c7-49fa-9350-bf74ed785e23"), new Guid("4f0be9f2-cb66-4d32-8f67-cf6b40497c94"), 5.10298606424193m },
                    { new Guid("9dd5b838-6958-46c9-a9dc-554cbcb1a582"), new Guid("b0ea83ca-4260-44a9-ac40-f24dd285032b"), new Guid("4f0be9f2-cb66-4d32-8f67-cf6b40497c94"), 2.97320475596912m },
                    { new Guid("a9330857-fbcb-4fe5-9617-818b64d1102a"), new Guid("f632d840-ff58-42bc-ab8f-a2c9de53bdee"), new Guid("ad1f6b71-30c7-49fa-9350-bf74ed785e23"), 8.35206356351371m },
                    { new Guid("c7742747-87a2-48c0-ba8b-4f2a62604a69"), new Guid("4f0be9f2-cb66-4d32-8f67-cf6b40497c94"), new Guid("f632d840-ff58-42bc-ab8f-a2c9de53bdee"), 5.67199697000153m },
                    { new Guid("ce6000fb-a74c-4ae0-9e0c-99e452d46b7c"), new Guid("ad1f6b71-30c7-49fa-9350-bf74ed785e23"), new Guid("b0ea83ca-4260-44a9-ac40-f24dd285032b"), 7.87293382943556m },
                    { new Guid("d2a753b7-b4f1-4122-9f5a-bc58c2e01169"), new Guid("b0ea83ca-4260-44a9-ac40-f24dd285032b"), new Guid("f632d840-ff58-42bc-ab8f-a2c9de53bdee"), 1.86624136835203m },
                    { new Guid("da1ed5d3-6359-4015-ac70-e6fc308ff113"), new Guid("4f0be9f2-cb66-4d32-8f67-cf6b40497c94"), new Guid("b0ea83ca-4260-44a9-ac40-f24dd285032b"), 8.24596405810693m }
                });

            migrationBuilder.InsertData(
                table: "ExchangeRates",
                columns: new[] { "Id", "CurrencyFromId", "CurrencyToId", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { new Guid("02569039-a97f-439a-848a-40ac69d6a233"), new Guid("f632d840-ff58-42bc-ab8f-a2c9de53bdee"), new Guid("b0ea83ca-4260-44a9-ac40-f24dd285032b"), new DateTimeOffset(new DateTime(2024, 2, 5, 11, 42, 19, 540, DateTimeKind.Unspecified).AddTicks(7964), new TimeSpan(0, 0, 0, 0, 0)), 8.21807839890546m },
                    { new Guid("1a2f85fb-3468-4f56-898a-17cc8e02a43b"), new Guid("4f0be9f2-cb66-4d32-8f67-cf6b40497c94"), new Guid("ad1f6b71-30c7-49fa-9350-bf74ed785e23"), new DateTimeOffset(new DateTime(2024, 2, 5, 11, 42, 19, 540, DateTimeKind.Unspecified).AddTicks(7964), new TimeSpan(0, 0, 0, 0, 0)), 5.85422826910279m },
                    { new Guid("2e19ada1-f8dc-4ee2-89df-fc55ba053085"), new Guid("ad1f6b71-30c7-49fa-9350-bf74ed785e23"), new Guid("b0ea83ca-4260-44a9-ac40-f24dd285032b"), new DateTimeOffset(new DateTime(2024, 2, 5, 11, 42, 19, 540, DateTimeKind.Unspecified).AddTicks(7964), new TimeSpan(0, 0, 0, 0, 0)), 7.01524675795986m },
                    { new Guid("355aeda1-da89-45a6-9571-a8a48a035568"), new Guid("b0ea83ca-4260-44a9-ac40-f24dd285032b"), new Guid("10d05faf-e87e-41a9-bbbd-b94bb4fb1c33"), new DateTimeOffset(new DateTime(2024, 2, 5, 11, 42, 19, 540, DateTimeKind.Unspecified).AddTicks(7964), new TimeSpan(0, 0, 0, 0, 0)), 2.22921438042379m },
                    { new Guid("41c3ac97-2563-4944-aeed-6ef04347c25b"), new Guid("10d05faf-e87e-41a9-bbbd-b94bb4fb1c33"), new Guid("ad1f6b71-30c7-49fa-9350-bf74ed785e23"), new DateTimeOffset(new DateTime(2024, 2, 5, 11, 42, 19, 540, DateTimeKind.Unspecified).AddTicks(7964), new TimeSpan(0, 0, 0, 0, 0)), 0.771742040662266m },
                    { new Guid("6115075b-5cc7-43cd-b447-b4f325549da7"), new Guid("4f0be9f2-cb66-4d32-8f67-cf6b40497c94"), new Guid("10d05faf-e87e-41a9-bbbd-b94bb4fb1c33"), new DateTimeOffset(new DateTime(2024, 2, 5, 11, 42, 19, 540, DateTimeKind.Unspecified).AddTicks(7964), new TimeSpan(0, 0, 0, 0, 0)), 1.71384606475925m },
                    { new Guid("618a91ca-7592-493e-a169-6a46bc8707f6"), new Guid("f632d840-ff58-42bc-ab8f-a2c9de53bdee"), new Guid("10d05faf-e87e-41a9-bbbd-b94bb4fb1c33"), new DateTimeOffset(new DateTime(2024, 2, 5, 11, 42, 19, 540, DateTimeKind.Unspecified).AddTicks(7964), new TimeSpan(0, 0, 0, 0, 0)), 2.53065618990091m },
                    { new Guid("78b6721e-636c-49a5-aef2-4b581bd4dfea"), new Guid("ad1f6b71-30c7-49fa-9350-bf74ed785e23"), new Guid("f632d840-ff58-42bc-ab8f-a2c9de53bdee"), new DateTimeOffset(new DateTime(2024, 2, 5, 11, 42, 19, 540, DateTimeKind.Unspecified).AddTicks(7964), new TimeSpan(0, 0, 0, 0, 0)), 9.12762745283286m },
                    { new Guid("94be7864-75b9-4598-bbf9-219af73cfe93"), new Guid("10d05faf-e87e-41a9-bbbd-b94bb4fb1c33"), new Guid("4f0be9f2-cb66-4d32-8f67-cf6b40497c94"), new DateTimeOffset(new DateTime(2024, 2, 5, 11, 42, 19, 540, DateTimeKind.Unspecified).AddTicks(7964), new TimeSpan(0, 0, 0, 0, 0)), 0.375540754241749m },
                    { new Guid("9b3ccdf1-2c22-46d1-b41a-81a3cd35b2e2"), new Guid("10d05faf-e87e-41a9-bbbd-b94bb4fb1c33"), new Guid("f632d840-ff58-42bc-ab8f-a2c9de53bdee"), new DateTimeOffset(new DateTime(2024, 2, 5, 11, 42, 19, 540, DateTimeKind.Unspecified).AddTicks(7964), new TimeSpan(0, 0, 0, 0, 0)), 5.12705620768923m },
                    { new Guid("a39d74f3-67a6-46b0-ae94-14888384fff2"), new Guid("b0ea83ca-4260-44a9-ac40-f24dd285032b"), new Guid("4f0be9f2-cb66-4d32-8f67-cf6b40497c94"), new DateTimeOffset(new DateTime(2024, 2, 5, 11, 42, 19, 540, DateTimeKind.Unspecified).AddTicks(7964), new TimeSpan(0, 0, 0, 0, 0)), 2.27538136635462m },
                    { new Guid("b436306c-23d8-4277-84e0-de82a4d3d0a6"), new Guid("b0ea83ca-4260-44a9-ac40-f24dd285032b"), new Guid("ad1f6b71-30c7-49fa-9350-bf74ed785e23"), new DateTimeOffset(new DateTime(2024, 2, 5, 11, 42, 19, 540, DateTimeKind.Unspecified).AddTicks(7964), new TimeSpan(0, 0, 0, 0, 0)), 1.12527130755973m },
                    { new Guid("b733fcc7-56dc-4793-80c3-e7185ceb71ed"), new Guid("ad1f6b71-30c7-49fa-9350-bf74ed785e23"), new Guid("10d05faf-e87e-41a9-bbbd-b94bb4fb1c33"), new DateTimeOffset(new DateTime(2024, 2, 5, 11, 42, 19, 540, DateTimeKind.Unspecified).AddTicks(7964), new TimeSpan(0, 0, 0, 0, 0)), 3.33632272288771m },
                    { new Guid("c70f3f9a-0d50-499d-a175-f93592d919b2"), new Guid("f632d840-ff58-42bc-ab8f-a2c9de53bdee"), new Guid("ad1f6b71-30c7-49fa-9350-bf74ed785e23"), new DateTimeOffset(new DateTime(2024, 2, 5, 11, 42, 19, 540, DateTimeKind.Unspecified).AddTicks(7964), new TimeSpan(0, 0, 0, 0, 0)), 3.72566822570559m },
                    { new Guid("e9f712ee-817e-40aa-a0d0-80dacc3a3370"), new Guid("f632d840-ff58-42bc-ab8f-a2c9de53bdee"), new Guid("4f0be9f2-cb66-4d32-8f67-cf6b40497c94"), new DateTimeOffset(new DateTime(2024, 2, 5, 11, 42, 19, 540, DateTimeKind.Unspecified).AddTicks(7964), new TimeSpan(0, 0, 0, 0, 0)), 5.03287693135132m },
                    { new Guid("eb4855b4-9423-4e58-a460-0130db28122e"), new Guid("4f0be9f2-cb66-4d32-8f67-cf6b40497c94"), new Guid("b0ea83ca-4260-44a9-ac40-f24dd285032b"), new DateTimeOffset(new DateTime(2024, 2, 5, 11, 42, 19, 540, DateTimeKind.Unspecified).AddTicks(7964), new TimeSpan(0, 0, 0, 0, 0)), 4.96074310781057m },
                    { new Guid("eee76612-13f7-442f-8688-e92c5c3755f7"), new Guid("b0ea83ca-4260-44a9-ac40-f24dd285032b"), new Guid("f632d840-ff58-42bc-ab8f-a2c9de53bdee"), new DateTimeOffset(new DateTime(2024, 2, 5, 11, 42, 19, 540, DateTimeKind.Unspecified).AddTicks(7964), new TimeSpan(0, 0, 0, 0, 0)), 7.87453609369409m },
                    { new Guid("f4044b4f-f798-4284-9f8b-ce405bb500c8"), new Guid("ad1f6b71-30c7-49fa-9350-bf74ed785e23"), new Guid("4f0be9f2-cb66-4d32-8f67-cf6b40497c94"), new DateTimeOffset(new DateTime(2024, 2, 5, 11, 42, 19, 540, DateTimeKind.Unspecified).AddTicks(7964), new TimeSpan(0, 0, 0, 0, 0)), 7.99237167573898m },
                    { new Guid("f5ffe8c6-52e0-427f-abf3-0322dab589c6"), new Guid("4f0be9f2-cb66-4d32-8f67-cf6b40497c94"), new Guid("f632d840-ff58-42bc-ab8f-a2c9de53bdee"), new DateTimeOffset(new DateTime(2024, 2, 5, 11, 42, 19, 540, DateTimeKind.Unspecified).AddTicks(7964), new TimeSpan(0, 0, 0, 0, 0)), 1.734162200406m },
                    { new Guid("f8e040e6-1c06-46b2-abc7-53a64125eb75"), new Guid("10d05faf-e87e-41a9-bbbd-b94bb4fb1c33"), new Guid("b0ea83ca-4260-44a9-ac40-f24dd285032b"), new DateTimeOffset(new DateTime(2024, 2, 5, 11, 42, 19, 540, DateTimeKind.Unspecified).AddTicks(7964), new TimeSpan(0, 0, 0, 0, 0)), 9.02858918602043m }
                });

            migrationBuilder.InsertData(
                table: "MoneyAccounts",
                columns: new[] { "Id", "Balance", "CurrencyId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0adfa3dd-11b8-48d1-92ce-f0dda1aa1551"), 0m, new Guid("b0ea83ca-4260-44a9-ac40-f24dd285032b"), new Guid("20505800-43c3-43c4-81c6-b028d33421c9") },
                    { new Guid("203fd9c1-a137-40f6-9035-5c1f4719c07a"), 0m, new Guid("f632d840-ff58-42bc-ab8f-a2c9de53bdee"), new Guid("20505800-43c3-43c4-81c6-b028d33421c9") },
                    { new Guid("63c5c16c-c86b-42fd-ac6d-c59fdf2a666c"), 0m, new Guid("4f0be9f2-cb66-4d32-8f67-cf6b40497c94"), new Guid("20505800-43c3-43c4-81c6-b028d33421c9") },
                    { new Guid("7abda455-a128-4ab9-b9fe-1bffd21c1fe7"), 0m, new Guid("4f0be9f2-cb66-4d32-8f67-cf6b40497c94"), new Guid("5f876f3f-0653-4f65-94da-1c6d61cc9855") },
                    { new Guid("7dbb16e5-d807-4850-a839-ee1f0536af2f"), 0m, new Guid("ad1f6b71-30c7-49fa-9350-bf74ed785e23"), new Guid("20505800-43c3-43c4-81c6-b028d33421c9") },
                    { new Guid("93ae58fe-c20e-4397-97ea-cd30bc3ee0b0"), 0m, new Guid("f632d840-ff58-42bc-ab8f-a2c9de53bdee"), new Guid("5f876f3f-0653-4f65-94da-1c6d61cc9855") },
                    { new Guid("a97608f1-cc31-4ccc-a26d-a995917eabe2"), 0m, new Guid("b0ea83ca-4260-44a9-ac40-f24dd285032b"), new Guid("5f876f3f-0653-4f65-94da-1c6d61cc9855") },
                    { new Guid("d34e316d-76f9-4b65-a8ed-9c6a0e9fce62"), 0m, new Guid("10d05faf-e87e-41a9-bbbd-b94bb4fb1c33"), new Guid("5f876f3f-0653-4f65-94da-1c6d61cc9855") },
                    { new Guid("dc2887a6-6669-4a3e-8141-2d979631990f"), 0m, new Guid("10d05faf-e87e-41a9-bbbd-b94bb4fb1c33"), new Guid("20505800-43c3-43c4-81c6-b028d33421c9") },
                    { new Guid("ede4cf1f-b73c-4f69-bb64-fcb34b97f762"), 0m, new Guid("ad1f6b71-30c7-49fa-9350-bf74ed785e23"), new Guid("5f876f3f-0653-4f65-94da-1c6d61cc9855") }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("b6bf79ee-74ae-4c7d-bf44-4e6897fe8e65"), new Guid("20505800-43c3-43c4-81c6-b028d33421c9") },
                    { new Guid("e9956abf-21bb-47ef-ac56-c0ea1300a813"), new Guid("20505800-43c3-43c4-81c6-b028d33421c9") },
                    { new Guid("e9956abf-21bb-47ef-ac56-c0ea1300a813"), new Guid("5f876f3f-0653-4f65-94da-1c6d61cc9855") }
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
