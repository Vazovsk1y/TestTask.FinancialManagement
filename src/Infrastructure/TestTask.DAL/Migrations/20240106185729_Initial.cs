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
                });

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

            migrationBuilder.InsertData(
                table: "Commissions",
                columns: new[] { "Id", "CurrencyFromId", "CurrencyToId", "Value" },
                values: new object[,]
                {
                    { new Guid("1e5ae49d-9d21-44e3-a35f-121d41c96f5c"), new Guid("91ee387d-153f-4614-a02a-5321d0f69bb1"), new Guid("95d3abcd-05a9-48fe-9f28-ab0fe379387b"), 0.1m },
                    { new Guid("29866c48-8153-4bb8-a438-010aee0a0f38"), new Guid("95d3abcd-05a9-48fe-9f28-ab0fe379387b"), new Guid("91ee387d-153f-4614-a02a-5321d0f69bb1"), 0.21m },
                    { new Guid("7dd318c2-b785-4ace-86fb-85cbfa3b7b4e"), new Guid("91ee387d-153f-4614-a02a-5321d0f69bb1"), new Guid("78f83ec9-90ab-4ce0-a3d6-196372aa72a9"), 0.08m },
                    { new Guid("9d04d7e2-cd09-4b46-8234-4fcbc534eb7d"), new Guid("95d3abcd-05a9-48fe-9f28-ab0fe379387b"), new Guid("560e3524-1693-4fba-ade8-370da5b9a02b"), 0.15m },
                    { new Guid("b3ff0421-30aa-4402-85e3-0d7c2ef618cc"), new Guid("78f83ec9-90ab-4ce0-a3d6-196372aa72a9"), new Guid("91ee387d-153f-4614-a02a-5321d0f69bb1"), 0.14m }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "AlphabeticCode", "NumericCode", "Title" },
                values: new object[,]
                {
                    { new Guid("0229db88-1778-45a7-8367-755e0e4936e8"), "GBP", "826", "British Pound Sterling" },
                    { new Guid("560e3524-1693-4fba-ade8-370da5b9a02b"), "EUR", "978", "Euro" },
                    { new Guid("78f83ec9-90ab-4ce0-a3d6-196372aa72a9"), "USD", "840", "United States Dollar" },
                    { new Guid("91ee387d-153f-4614-a02a-5321d0f69bb1"), "UAH", "980", "Ukrainian Hryvnia" },
                    { new Guid("923a0172-2022-40e7-bc3d-05b6aa0cd5cc"), "JPY", "392", "Japanese Yen" },
                    { new Guid("95d3abcd-05a9-48fe-9f28-ab0fe379387b"), "RUB", "643", "Russian Ruble" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("1834f226-0d3e-4782-8bc6-45195c929baf"), "Admin" },
                    { new Guid("e949b90f-172b-4478-bfba-b309341024f3"), "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("78272231-c67b-4cf0-ae43-557b152b35fd"), "popka@gmail.com", "John Doe", "$2a$11$N6v06ElDxQu2ehLHLQvxmeHTgBNzkgHX/vc0OAllW87BLDMbZXG3y" },
                    { new Guid("ecfe7076-acba-4d23-8cdb-2d954afb450a"), "penis@gmail.com", "Mike Vazovskiy", "$2a$11$ppPf9PYy0HU/rlyBO14vgOi1Q2tBFcq9RIc73IXpS1/Sw7y2IxeKm" }
                });

            migrationBuilder.InsertData(
                table: "MoneyAccounts",
                columns: new[] { "Id", "Balance", "CurrencyId", "UserId" },
                values: new object[,]
                {
                    { new Guid("29299afb-8591-4c6b-a10a-785f74ff56b8"), 0m, new Guid("0229db88-1778-45a7-8367-755e0e4936e8"), new Guid("ecfe7076-acba-4d23-8cdb-2d954afb450a") },
                    { new Guid("2ea873c9-6720-4010-83d7-09527ee0244d"), 0m, new Guid("560e3524-1693-4fba-ade8-370da5b9a02b"), new Guid("ecfe7076-acba-4d23-8cdb-2d954afb450a") },
                    { new Guid("4721de23-e8a4-4780-9c77-dbf5138705d4"), 0m, new Guid("78f83ec9-90ab-4ce0-a3d6-196372aa72a9"), new Guid("ecfe7076-acba-4d23-8cdb-2d954afb450a") },
                    { new Guid("5a9c29c5-e1ee-4e42-87c5-f1b3d7d77055"), 0m, new Guid("91ee387d-153f-4614-a02a-5321d0f69bb1"), new Guid("ecfe7076-acba-4d23-8cdb-2d954afb450a") },
                    { new Guid("a04ef95e-6fb0-4f41-a81a-f4e9cddff841"), 0m, new Guid("91ee387d-153f-4614-a02a-5321d0f69bb1"), new Guid("78272231-c67b-4cf0-ae43-557b152b35fd") },
                    { new Guid("a8cb3937-853f-4d01-9042-8e720c8fc8e1"), 0m, new Guid("923a0172-2022-40e7-bc3d-05b6aa0cd5cc"), new Guid("78272231-c67b-4cf0-ae43-557b152b35fd") },
                    { new Guid("acba0c34-e0aa-4c58-aadd-9ba1ee925669"), 0m, new Guid("95d3abcd-05a9-48fe-9f28-ab0fe379387b"), new Guid("78272231-c67b-4cf0-ae43-557b152b35fd") },
                    { new Guid("bec5fdbd-ca38-49a5-a0e9-c6071623dc5f"), 0m, new Guid("95d3abcd-05a9-48fe-9f28-ab0fe379387b"), new Guid("ecfe7076-acba-4d23-8cdb-2d954afb450a") },
                    { new Guid("caf09fd6-c04c-4b2f-9939-60b6c36b7160"), 0m, new Guid("78f83ec9-90ab-4ce0-a3d6-196372aa72a9"), new Guid("78272231-c67b-4cf0-ae43-557b152b35fd") },
                    { new Guid("dca585a8-3e6c-41df-bb0a-470e3ce650e3"), 0m, new Guid("560e3524-1693-4fba-ade8-370da5b9a02b"), new Guid("78272231-c67b-4cf0-ae43-557b152b35fd") },
                    { new Guid("e4730aa0-3b14-4b77-93f0-1f21fb4854ce"), 0m, new Guid("923a0172-2022-40e7-bc3d-05b6aa0cd5cc"), new Guid("ecfe7076-acba-4d23-8cdb-2d954afb450a") },
                    { new Guid("f5939608-cefe-490e-a6c6-3c9e78eb3645"), 0m, new Guid("0229db88-1778-45a7-8367-755e0e4936e8"), new Guid("78272231-c67b-4cf0-ae43-557b152b35fd") }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("e949b90f-172b-4478-bfba-b309341024f3"), new Guid("78272231-c67b-4cf0-ae43-557b152b35fd") },
                    { new Guid("1834f226-0d3e-4782-8bc6-45195c929baf"), new Guid("ecfe7076-acba-4d23-8cdb-2d954afb450a") },
                    { new Guid("e949b90f-172b-4478-bfba-b309341024f3"), new Guid("ecfe7076-acba-4d23-8cdb-2d954afb450a") }
                });

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
                name: "IX_MoneyAccounts_CurrencyId",
                table: "MoneyAccounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyAccounts_UserId",
                table: "MoneyAccounts",
                column: "UserId");

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
                name: "MoneyAccounts");

            migrationBuilder.DropTable(
                name: "MoneyOperations");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
