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
                    Value = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
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
                    { new Guid("3213b06e-aa75-4d3d-9d8e-33bf4d8ae673"), "ANG", "532", "Netherlands Antillian Guilder" },
                    { new Guid("5704271f-5817-445a-8a5e-6f7455766d7f"), "ALL", "8", "Albanian Lek" },
                    { new Guid("73d7a62c-e7fa-4781-8326-7768aa56eb9b"), "AMD", "51", "Armenian Dram" },
                    { new Guid("f2ca62d4-5f5e-491c-8cf7-b456cec368d5"), "AFN", "971", "Afghan Afghani" },
                    { new Guid("f8af699b-a251-4153-b42b-f0fccaa6bee0"), "AED", "784", "UAE Dirham" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("8c584b56-feef-4403-af9c-95f4883b7b83"), "Admin" },
                    { new Guid("d6ffd0ce-6f1d-48a8-af37-974ae74b1ae6"), "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("895ed007-9c23-4245-882c-217ca7e02b7e"), "penis@gmail.com", "Mike Vazovskiy", "$2a$11$qXxrqcC75oz5NYGZaQ/6p.BtSIYxVGFa45.TTKUeA47k9TbJqdE5m" },
                    { new Guid("f1e15df8-87aa-4434-813f-ca92a2af6d52"), "popka@gmail.com", "John Doe", "$2a$11$UjZF/zc6XPJHXlmd4bBHweoPWrm.6H.FjHBaaVfvV.SayMzKqTwau" }
                });

            migrationBuilder.InsertData(
                table: "Commissions",
                columns: new[] { "Id", "CurrencyFromId", "CurrencyToId", "Value" },
                values: new object[,]
                {
                    { new Guid("07392968-8d5d-4c9d-b8c0-793d0a4b8a67"), new Guid("f8af699b-a251-4153-b42b-f0fccaa6bee0"), new Guid("f2ca62d4-5f5e-491c-8cf7-b456cec368d5"), 2.2270383442593m },
                    { new Guid("281011aa-12ce-4ee4-8148-24d2064a5c68"), new Guid("3213b06e-aa75-4d3d-9d8e-33bf4d8ae673"), new Guid("f2ca62d4-5f5e-491c-8cf7-b456cec368d5"), 0.126517033431398m },
                    { new Guid("2f0e8230-38a6-4b1f-a489-c706a20c83fa"), new Guid("f8af699b-a251-4153-b42b-f0fccaa6bee0"), new Guid("3213b06e-aa75-4d3d-9d8e-33bf4d8ae673"), 5.43549310155683m },
                    { new Guid("3634e0e5-38ca-4b4e-a8d7-d28f6d6d4c78"), new Guid("73d7a62c-e7fa-4781-8326-7768aa56eb9b"), new Guid("3213b06e-aa75-4d3d-9d8e-33bf4d8ae673"), 4.18638209899037m },
                    { new Guid("3912270c-40dc-4c94-b4fb-64d44f99ab46"), new Guid("3213b06e-aa75-4d3d-9d8e-33bf4d8ae673"), new Guid("5704271f-5817-445a-8a5e-6f7455766d7f"), 0.666973945298662m },
                    { new Guid("3d39f381-a9ee-4a92-bd56-0f7964244ab7"), new Guid("3213b06e-aa75-4d3d-9d8e-33bf4d8ae673"), new Guid("73d7a62c-e7fa-4781-8326-7768aa56eb9b"), 4.2374759282518m },
                    { new Guid("50320c99-d112-4c4b-8bb0-02203ec89fc9"), new Guid("f2ca62d4-5f5e-491c-8cf7-b456cec368d5"), new Guid("f8af699b-a251-4153-b42b-f0fccaa6bee0"), 7.87618010800062m },
                    { new Guid("6b669d34-f8d0-4cf0-ba2d-ac2b866db078"), new Guid("5704271f-5817-445a-8a5e-6f7455766d7f"), new Guid("73d7a62c-e7fa-4781-8326-7768aa56eb9b"), 8.78802446998824m },
                    { new Guid("76acdfc0-28a3-479a-9c1c-53329ccaf75c"), new Guid("5704271f-5817-445a-8a5e-6f7455766d7f"), new Guid("f2ca62d4-5f5e-491c-8cf7-b456cec368d5"), 2.06334551214867m },
                    { new Guid("81875e10-8e75-4c98-93d8-a872466b0087"), new Guid("f8af699b-a251-4153-b42b-f0fccaa6bee0"), new Guid("5704271f-5817-445a-8a5e-6f7455766d7f"), 6.95343438950926m },
                    { new Guid("8757d171-8a6f-44eb-88ce-c08030ed0b6e"), new Guid("73d7a62c-e7fa-4781-8326-7768aa56eb9b"), new Guid("5704271f-5817-445a-8a5e-6f7455766d7f"), 4.90283570666045m },
                    { new Guid("8ffac78e-5459-422b-865f-76083608a720"), new Guid("f2ca62d4-5f5e-491c-8cf7-b456cec368d5"), new Guid("3213b06e-aa75-4d3d-9d8e-33bf4d8ae673"), 8.52950501608402m },
                    { new Guid("9c4aeefa-685e-4dbd-b52d-ba444e43cd2a"), new Guid("3213b06e-aa75-4d3d-9d8e-33bf4d8ae673"), new Guid("f8af699b-a251-4153-b42b-f0fccaa6bee0"), 8.05226803016824m },
                    { new Guid("a85fb95f-8b06-4816-968e-898c5c43b9be"), new Guid("73d7a62c-e7fa-4781-8326-7768aa56eb9b"), new Guid("f8af699b-a251-4153-b42b-f0fccaa6bee0"), 5.75373600617953m },
                    { new Guid("bd2cd155-6ec5-4968-8629-72fd64c0f06b"), new Guid("f8af699b-a251-4153-b42b-f0fccaa6bee0"), new Guid("73d7a62c-e7fa-4781-8326-7768aa56eb9b"), 9.86968378204344m },
                    { new Guid("c51d45cb-11b0-4c71-938e-58b2b443be54"), new Guid("5704271f-5817-445a-8a5e-6f7455766d7f"), new Guid("f8af699b-a251-4153-b42b-f0fccaa6bee0"), 7.67845818639682m },
                    { new Guid("cc64d622-55af-479f-bbd9-08969cab5f58"), new Guid("f2ca62d4-5f5e-491c-8cf7-b456cec368d5"), new Guid("73d7a62c-e7fa-4781-8326-7768aa56eb9b"), 1.59778150904579m },
                    { new Guid("e7e66c52-97c3-46d4-b9f6-a5d014b1dbb1"), new Guid("f2ca62d4-5f5e-491c-8cf7-b456cec368d5"), new Guid("5704271f-5817-445a-8a5e-6f7455766d7f"), 1.9759383337984m },
                    { new Guid("f70276c8-1911-4c3c-b411-8d83fe216181"), new Guid("5704271f-5817-445a-8a5e-6f7455766d7f"), new Guid("3213b06e-aa75-4d3d-9d8e-33bf4d8ae673"), 7.92325677162923m },
                    { new Guid("fa369e42-9ce1-4f0e-88a8-5b4ffd9d9d6a"), new Guid("73d7a62c-e7fa-4781-8326-7768aa56eb9b"), new Guid("f2ca62d4-5f5e-491c-8cf7-b456cec368d5"), 0.245477647278161m }
                });

            migrationBuilder.InsertData(
                table: "ExchangeRates",
                columns: new[] { "Id", "CurrencyFromId", "CurrencyToId", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { new Guid("0644fe26-cf82-4c27-8d51-62b06f934359"), new Guid("3213b06e-aa75-4d3d-9d8e-33bf4d8ae673"), new Guid("73d7a62c-e7fa-4781-8326-7768aa56eb9b"), new DateTimeOffset(new DateTime(2024, 2, 7, 11, 16, 53, 58, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 0, 0, 0, 0)), 4.46181650288965m },
                    { new Guid("184ca71a-403c-4609-8858-8e69864850e7"), new Guid("73d7a62c-e7fa-4781-8326-7768aa56eb9b"), new Guid("3213b06e-aa75-4d3d-9d8e-33bf4d8ae673"), new DateTimeOffset(new DateTime(2024, 2, 7, 11, 16, 53, 58, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 0, 0, 0, 0)), 9.68014375579476m },
                    { new Guid("2d6ddffe-d0e1-42ea-a1e6-2f1153b1c932"), new Guid("73d7a62c-e7fa-4781-8326-7768aa56eb9b"), new Guid("f2ca62d4-5f5e-491c-8cf7-b456cec368d5"), new DateTimeOffset(new DateTime(2024, 2, 7, 11, 16, 53, 58, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 0, 0, 0, 0)), 5.07777715514935m },
                    { new Guid("2f0be45a-5662-4b8e-8b70-7a552eb0f874"), new Guid("73d7a62c-e7fa-4781-8326-7768aa56eb9b"), new Guid("f8af699b-a251-4153-b42b-f0fccaa6bee0"), new DateTimeOffset(new DateTime(2024, 2, 7, 11, 16, 53, 58, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 0, 0, 0, 0)), 5.68645418398078m },
                    { new Guid("36ef8d82-67ca-4b38-acea-bae3a74078bb"), new Guid("73d7a62c-e7fa-4781-8326-7768aa56eb9b"), new Guid("5704271f-5817-445a-8a5e-6f7455766d7f"), new DateTimeOffset(new DateTime(2024, 2, 7, 11, 16, 53, 58, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 0, 0, 0, 0)), 7.56158947185133m },
                    { new Guid("57746b40-d4b4-4da9-b4c2-721f969f8f4d"), new Guid("5704271f-5817-445a-8a5e-6f7455766d7f"), new Guid("f2ca62d4-5f5e-491c-8cf7-b456cec368d5"), new DateTimeOffset(new DateTime(2024, 2, 7, 11, 16, 53, 58, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 0, 0, 0, 0)), 6.716622508041m },
                    { new Guid("7b7ccbd6-cb1f-4d86-a622-d83d92902612"), new Guid("f2ca62d4-5f5e-491c-8cf7-b456cec368d5"), new Guid("73d7a62c-e7fa-4781-8326-7768aa56eb9b"), new DateTimeOffset(new DateTime(2024, 2, 7, 11, 16, 53, 58, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 0, 0, 0, 0)), 2.77725398416325m },
                    { new Guid("7be6558d-bbd1-41f3-8cd7-ecdab3cba385"), new Guid("f2ca62d4-5f5e-491c-8cf7-b456cec368d5"), new Guid("3213b06e-aa75-4d3d-9d8e-33bf4d8ae673"), new DateTimeOffset(new DateTime(2024, 2, 7, 11, 16, 53, 58, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 0, 0, 0, 0)), 6.54969046702452m },
                    { new Guid("84b45975-ed9f-4dd1-9f26-81a07393609f"), new Guid("3213b06e-aa75-4d3d-9d8e-33bf4d8ae673"), new Guid("f8af699b-a251-4153-b42b-f0fccaa6bee0"), new DateTimeOffset(new DateTime(2024, 2, 7, 11, 16, 53, 58, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 0, 0, 0, 0)), 7.17342710784825m },
                    { new Guid("950d73d8-3e74-4a60-9e57-c2356075f4b6"), new Guid("5704271f-5817-445a-8a5e-6f7455766d7f"), new Guid("3213b06e-aa75-4d3d-9d8e-33bf4d8ae673"), new DateTimeOffset(new DateTime(2024, 2, 7, 11, 16, 53, 58, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 0, 0, 0, 0)), 0.46835236663189m },
                    { new Guid("9eae5288-1c49-4269-bdb3-10b41cece012"), new Guid("f2ca62d4-5f5e-491c-8cf7-b456cec368d5"), new Guid("f8af699b-a251-4153-b42b-f0fccaa6bee0"), new DateTimeOffset(new DateTime(2024, 2, 7, 11, 16, 53, 58, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 0, 0, 0, 0)), 1.79140119088406m },
                    { new Guid("9faf1da1-8f09-4c25-a8c2-8b9b3e79d0a6"), new Guid("5704271f-5817-445a-8a5e-6f7455766d7f"), new Guid("73d7a62c-e7fa-4781-8326-7768aa56eb9b"), new DateTimeOffset(new DateTime(2024, 2, 7, 11, 16, 53, 58, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 0, 0, 0, 0)), 6.58188878167991m },
                    { new Guid("ae8ffb23-7b42-476f-b36a-c70c13d8ad45"), new Guid("f8af699b-a251-4153-b42b-f0fccaa6bee0"), new Guid("f2ca62d4-5f5e-491c-8cf7-b456cec368d5"), new DateTimeOffset(new DateTime(2024, 2, 7, 11, 16, 53, 58, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 0, 0, 0, 0)), 4.966276976087m },
                    { new Guid("c613c0c8-fa90-44c3-865a-355aa5903a3d"), new Guid("f8af699b-a251-4153-b42b-f0fccaa6bee0"), new Guid("3213b06e-aa75-4d3d-9d8e-33bf4d8ae673"), new DateTimeOffset(new DateTime(2024, 2, 7, 11, 16, 53, 58, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 0, 0, 0, 0)), 3.94577231866189m },
                    { new Guid("e0d3a1ce-e25a-428c-b0d9-ba8856285ab4"), new Guid("3213b06e-aa75-4d3d-9d8e-33bf4d8ae673"), new Guid("5704271f-5817-445a-8a5e-6f7455766d7f"), new DateTimeOffset(new DateTime(2024, 2, 7, 11, 16, 53, 58, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 0, 0, 0, 0)), 0.901346880852377m },
                    { new Guid("f1839fe5-d893-4509-b77c-3d9c5894f4d5"), new Guid("f2ca62d4-5f5e-491c-8cf7-b456cec368d5"), new Guid("5704271f-5817-445a-8a5e-6f7455766d7f"), new DateTimeOffset(new DateTime(2024, 2, 7, 11, 16, 53, 58, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 0, 0, 0, 0)), 3.50496773916495m },
                    { new Guid("f1a94063-dfc9-4eaa-a517-b38c9a8a12f0"), new Guid("5704271f-5817-445a-8a5e-6f7455766d7f"), new Guid("f8af699b-a251-4153-b42b-f0fccaa6bee0"), new DateTimeOffset(new DateTime(2024, 2, 7, 11, 16, 53, 58, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 0, 0, 0, 0)), 3.57545146628002m },
                    { new Guid("f810688d-10eb-48b6-9fb7-de3109ed284d"), new Guid("3213b06e-aa75-4d3d-9d8e-33bf4d8ae673"), new Guid("f2ca62d4-5f5e-491c-8cf7-b456cec368d5"), new DateTimeOffset(new DateTime(2024, 2, 7, 11, 16, 53, 58, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 0, 0, 0, 0)), 7.46985068366841m },
                    { new Guid("fc3b8bce-cd35-4bc7-8fea-4c328ef5acae"), new Guid("f8af699b-a251-4153-b42b-f0fccaa6bee0"), new Guid("73d7a62c-e7fa-4781-8326-7768aa56eb9b"), new DateTimeOffset(new DateTime(2024, 2, 7, 11, 16, 53, 58, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 0, 0, 0, 0)), 1.49080628471838m },
                    { new Guid("fe0b9966-e070-4881-bded-14c74e35585f"), new Guid("f8af699b-a251-4153-b42b-f0fccaa6bee0"), new Guid("5704271f-5817-445a-8a5e-6f7455766d7f"), new DateTimeOffset(new DateTime(2024, 2, 7, 11, 16, 53, 58, DateTimeKind.Unspecified).AddTicks(2808), new TimeSpan(0, 0, 0, 0, 0)), 8.53871262495142m }
                });

            migrationBuilder.InsertData(
                table: "MoneyAccounts",
                columns: new[] { "Id", "Balance", "CurrencyId", "UserId" },
                values: new object[,]
                {
                    { new Guid("1e9126cf-98d7-42a9-88eb-ccb776689701"), 0m, new Guid("5704271f-5817-445a-8a5e-6f7455766d7f"), new Guid("f1e15df8-87aa-4434-813f-ca92a2af6d52") },
                    { new Guid("444b4612-145c-45dd-906a-0a84ea816618"), 0m, new Guid("f8af699b-a251-4153-b42b-f0fccaa6bee0"), new Guid("895ed007-9c23-4245-882c-217ca7e02b7e") },
                    { new Guid("50898eb1-4889-4904-b57b-c65c3d5c52b4"), 0m, new Guid("f2ca62d4-5f5e-491c-8cf7-b456cec368d5"), new Guid("895ed007-9c23-4245-882c-217ca7e02b7e") },
                    { new Guid("8b967caf-0ec5-469b-8fa9-4b1961206d8f"), 0m, new Guid("5704271f-5817-445a-8a5e-6f7455766d7f"), new Guid("895ed007-9c23-4245-882c-217ca7e02b7e") },
                    { new Guid("a876ecd0-639c-42d5-807c-40d7974a0fd9"), 0m, new Guid("3213b06e-aa75-4d3d-9d8e-33bf4d8ae673"), new Guid("895ed007-9c23-4245-882c-217ca7e02b7e") },
                    { new Guid("aa6b92c0-cda6-4a19-a371-57de4e59a81b"), 0m, new Guid("f8af699b-a251-4153-b42b-f0fccaa6bee0"), new Guid("f1e15df8-87aa-4434-813f-ca92a2af6d52") },
                    { new Guid("ac53626d-02cb-4d91-b408-cc20035cd044"), 0m, new Guid("73d7a62c-e7fa-4781-8326-7768aa56eb9b"), new Guid("f1e15df8-87aa-4434-813f-ca92a2af6d52") },
                    { new Guid("cc36e31b-b870-4927-9f9d-751cca34da5a"), 0m, new Guid("3213b06e-aa75-4d3d-9d8e-33bf4d8ae673"), new Guid("f1e15df8-87aa-4434-813f-ca92a2af6d52") },
                    { new Guid("e3776ba3-fd49-4036-b735-1066f6b98c24"), 0m, new Guid("f2ca62d4-5f5e-491c-8cf7-b456cec368d5"), new Guid("f1e15df8-87aa-4434-813f-ca92a2af6d52") },
                    { new Guid("eaffc45f-a1c6-48b4-88f0-81aaa58445a9"), 0m, new Guid("73d7a62c-e7fa-4781-8326-7768aa56eb9b"), new Guid("895ed007-9c23-4245-882c-217ca7e02b7e") }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("8c584b56-feef-4403-af9c-95f4883b7b83"), new Guid("895ed007-9c23-4245-882c-217ca7e02b7e") },
                    { new Guid("d6ffd0ce-6f1d-48a8-af37-974ae74b1ae6"), new Guid("895ed007-9c23-4245-882c-217ca7e02b7e") },
                    { new Guid("d6ffd0ce-6f1d-48a8-af37-974ae74b1ae6"), new Guid("f1e15df8-87aa-4434-813f-ca92a2af6d52") }
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
