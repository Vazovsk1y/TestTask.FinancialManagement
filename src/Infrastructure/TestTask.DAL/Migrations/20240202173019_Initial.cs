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
                    { new Guid("39539dfa-9514-43f9-b200-9168f5ed57e3"), "USD", "840", "United States Dollar" },
                    { new Guid("51bda368-44d3-4c3c-a12b-4d110af2aa58"), "UAH", "980", "Ukrainian Hryvnia" },
                    { new Guid("8810620d-7a4d-443f-8d6c-33711fff897a"), "GBP", "826", "British Pound Sterling" },
                    { new Guid("9cb41dc5-6c19-4f20-a1be-785c74608426"), "RUB", "643", "Russian Ruble" },
                    { new Guid("df68b4b6-3262-46b0-ad0c-f82518080f07"), "JPY", "392", "Japanese Yen" },
                    { new Guid("fa5f6314-81c2-4423-85dc-705a6241cabc"), "EUR", "978", "Euro" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("0a4abf0b-5e14-4a5a-9614-694adf676cbf"), "User" },
                    { new Guid("9c6aa58f-bad4-4238-9f4e-bdf04d0cdbf8"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("61a8814d-e898-4346-866e-b9abe231d74a"), "popka@gmail.com", "John Doe", "$2a$11$3n/IoRP2UZYbW14mChjkGO8FT2jrkIDIXJAFWqxNE0PomFUU.BRrS" },
                    { new Guid("93e8a673-5c07-4d41-8eb3-dbc9713588d3"), "penis@gmail.com", "Mike Vazovskiy", "$2a$11$p9jpLyVIrFG1boinEavjCOjn87R/li8jkoCgWc5rSrTXkTenPyZU." }
                });

            migrationBuilder.InsertData(
                table: "Commissions",
                columns: new[] { "Id", "CurrencyFromId", "CurrencyToId", "Value" },
                values: new object[,]
                {
                    { new Guid("06c4758a-19ff-41ab-983f-75a8e08ec78a"), new Guid("9cb41dc5-6c19-4f20-a1be-785c74608426"), new Guid("51bda368-44d3-4c3c-a12b-4d110af2aa58"), 0.21m },
                    { new Guid("077045d1-8307-4248-9a80-0ebba6702608"), new Guid("51bda368-44d3-4c3c-a12b-4d110af2aa58"), new Guid("9cb41dc5-6c19-4f20-a1be-785c74608426"), 0.1m },
                    { new Guid("29de9a6f-83a3-41cc-9a47-f753bbe78988"), new Guid("9cb41dc5-6c19-4f20-a1be-785c74608426"), new Guid("fa5f6314-81c2-4423-85dc-705a6241cabc"), 0.15m },
                    { new Guid("3d6aafdd-8dc5-4aa0-ad0d-b7bfeff520d4"), new Guid("39539dfa-9514-43f9-b200-9168f5ed57e3"), new Guid("51bda368-44d3-4c3c-a12b-4d110af2aa58"), 0.14m },
                    { new Guid("d7f63369-4d78-4705-8de2-2fa37e2fade7"), new Guid("51bda368-44d3-4c3c-a12b-4d110af2aa58"), new Guid("39539dfa-9514-43f9-b200-9168f5ed57e3"), 0.08m }
                });

            migrationBuilder.InsertData(
                table: "MoneyAccounts",
                columns: new[] { "Id", "Balance", "CurrencyId", "UserId" },
                values: new object[,]
                {
                    { new Guid("080eb4d9-32a0-46f6-896a-2f3533fcec58"), 0m, new Guid("51bda368-44d3-4c3c-a12b-4d110af2aa58"), new Guid("93e8a673-5c07-4d41-8eb3-dbc9713588d3") },
                    { new Guid("4a6ce671-2c14-4b01-952c-10ec370a7175"), 0m, new Guid("df68b4b6-3262-46b0-ad0c-f82518080f07"), new Guid("93e8a673-5c07-4d41-8eb3-dbc9713588d3") },
                    { new Guid("5e458d2d-a77f-4d6e-bc27-40ce6b65f264"), 0m, new Guid("9cb41dc5-6c19-4f20-a1be-785c74608426"), new Guid("93e8a673-5c07-4d41-8eb3-dbc9713588d3") },
                    { new Guid("78a2d9a7-da65-4140-a123-d24881bdd762"), 0m, new Guid("51bda368-44d3-4c3c-a12b-4d110af2aa58"), new Guid("61a8814d-e898-4346-866e-b9abe231d74a") },
                    { new Guid("865897d9-ba0a-4cf0-9df6-77ed4671c06d"), 0m, new Guid("9cb41dc5-6c19-4f20-a1be-785c74608426"), new Guid("61a8814d-e898-4346-866e-b9abe231d74a") },
                    { new Guid("8c837b24-f23e-42e9-b56e-7fbcce2e9b16"), 0m, new Guid("8810620d-7a4d-443f-8d6c-33711fff897a"), new Guid("93e8a673-5c07-4d41-8eb3-dbc9713588d3") },
                    { new Guid("b1e2e6af-c58e-496b-9f34-80a752877018"), 0m, new Guid("39539dfa-9514-43f9-b200-9168f5ed57e3"), new Guid("93e8a673-5c07-4d41-8eb3-dbc9713588d3") },
                    { new Guid("be264be9-9f5e-4bd4-9798-bfbae25ec1f5"), 0m, new Guid("39539dfa-9514-43f9-b200-9168f5ed57e3"), new Guid("61a8814d-e898-4346-866e-b9abe231d74a") },
                    { new Guid("cf707dcf-06ed-457d-b74f-b7fddc8eff2f"), 0m, new Guid("fa5f6314-81c2-4423-85dc-705a6241cabc"), new Guid("61a8814d-e898-4346-866e-b9abe231d74a") },
                    { new Guid("e1a0a2f6-34cf-4b22-94ec-9ae80a9c6ff9"), 0m, new Guid("df68b4b6-3262-46b0-ad0c-f82518080f07"), new Guid("61a8814d-e898-4346-866e-b9abe231d74a") },
                    { new Guid("eec5e097-115c-42c2-8217-2a1ea59b648f"), 0m, new Guid("8810620d-7a4d-443f-8d6c-33711fff897a"), new Guid("61a8814d-e898-4346-866e-b9abe231d74a") },
                    { new Guid("fe70e26f-fe0c-495d-972e-392709789ea4"), 0m, new Guid("fa5f6314-81c2-4423-85dc-705a6241cabc"), new Guid("93e8a673-5c07-4d41-8eb3-dbc9713588d3") }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0a4abf0b-5e14-4a5a-9614-694adf676cbf"), new Guid("61a8814d-e898-4346-866e-b9abe231d74a") },
                    { new Guid("0a4abf0b-5e14-4a5a-9614-694adf676cbf"), new Guid("93e8a673-5c07-4d41-8eb3-dbc9713588d3") },
                    { new Guid("9c6aa58f-bad4-4238-9f4e-bdf04d0cdbf8"), new Guid("93e8a673-5c07-4d41-8eb3-dbc9713588d3") }
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
