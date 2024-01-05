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
                name: "MoneyOperations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MoneyAccountFromId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MoneyAccountToId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MoneyAmount = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    OperationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoveType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoneyOperations_Commissions_CommissionId",
                        column: x => x.CommissionId,
                        principalTable: "Commissions",
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

            migrationBuilder.InsertData(
                table: "Commissions",
                columns: new[] { "Id", "CurrencyFromId", "CurrencyToId", "Value" },
                values: new object[,]
                {
                    { new Guid("076b3d84-c7cb-423d-81bd-788e44f676a4"), new Guid("e56277ad-bb5a-4fc8-b4ab-7b6cb56a4e6f"), new Guid("c9f2924e-523c-46b1-bdd1-404b3a972d41"), 0.15m },
                    { new Guid("58290782-772c-4e3e-af67-1435299ccaae"), new Guid("e56277ad-bb5a-4fc8-b4ab-7b6cb56a4e6f"), new Guid("18081637-6a28-4de3-a797-4eb340092044"), 0.21m },
                    { new Guid("90dddea6-03ba-4de6-bb34-c9c53d80cd35"), new Guid("fe59caa8-249c-4db5-b700-f40e3acade15"), new Guid("18081637-6a28-4de3-a797-4eb340092044"), 0.14m },
                    { new Guid("9baa1ddf-7294-431f-8701-6cbe66109a63"), new Guid("18081637-6a28-4de3-a797-4eb340092044"), new Guid("e56277ad-bb5a-4fc8-b4ab-7b6cb56a4e6f"), 0.1m },
                    { new Guid("a22532ba-a850-425c-9ee8-19266e29bc26"), new Guid("18081637-6a28-4de3-a797-4eb340092044"), new Guid("fe59caa8-249c-4db5-b700-f40e3acade15"), 0.08m }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "AlphabeticCode", "NumericCode", "Title" },
                values: new object[,]
                {
                    { new Guid("18081637-6a28-4de3-a797-4eb340092044"), "UAH", "980", "Ukrainian Hryvnia" },
                    { new Guid("98d7a0df-d4af-4408-85e3-19ad0c74ed5b"), "GBP", "826", "British Pound Sterling" },
                    { new Guid("99b68f3a-c560-4b60-ae7a-b500828197e8"), "JPY", "392", "Japanese Yen" },
                    { new Guid("c9f2924e-523c-46b1-bdd1-404b3a972d41"), "EUR", "978", "Euro" },
                    { new Guid("e56277ad-bb5a-4fc8-b4ab-7b6cb56a4e6f"), "RUB", "643", "Russian Ruble" },
                    { new Guid("fe59caa8-249c-4db5-b700-f40e3acade15"), "USD", "840", "United States Dollar" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("b1deb257-494e-44db-9767-8a9aee44cc55"), "Admin" },
                    { new Guid("f0a5b9ba-08ec-4c37-ad09-601c3094a042"), "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("32810ebd-20d1-49d3-8846-e4760bf8a568"), "penis@gmail.com", "Mike Vazovskiy", "$2a$11$CJElh7xnoiJMrc62slsmZeYxNcfTJ83YohHQWx4.1hcAjsLmcDePu" },
                    { new Guid("ec38f2a7-5b5f-4422-8d61-0fd21a0c860e"), "popka@gmail.com", "John Doe", "$2a$11$qj0wZu3aGCy7NV7frR8q.utwvmPBVG91MJI5RbBTEoUerQbtVLJlO" }
                });

            migrationBuilder.InsertData(
                table: "MoneyAccounts",
                columns: new[] { "Id", "Balance", "CurrencyId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0d65ddf5-ccd4-435e-9ff4-18757074e8f7"), 0m, new Guid("fe59caa8-249c-4db5-b700-f40e3acade15"), new Guid("ec38f2a7-5b5f-4422-8d61-0fd21a0c860e") },
                    { new Guid("215e66f0-4257-4961-a2c1-224d94bdc134"), 0m, new Guid("c9f2924e-523c-46b1-bdd1-404b3a972d41"), new Guid("ec38f2a7-5b5f-4422-8d61-0fd21a0c860e") },
                    { new Guid("42ebedae-12ec-4427-bf30-bdf05121d839"), 0m, new Guid("98d7a0df-d4af-4408-85e3-19ad0c74ed5b"), new Guid("32810ebd-20d1-49d3-8846-e4760bf8a568") },
                    { new Guid("483f4fa2-63a9-41b4-b482-e529bcc55ff4"), 0m, new Guid("99b68f3a-c560-4b60-ae7a-b500828197e8"), new Guid("32810ebd-20d1-49d3-8846-e4760bf8a568") },
                    { new Guid("60d407e1-b513-4822-aa8c-560f4c5ff62c"), 0m, new Guid("99b68f3a-c560-4b60-ae7a-b500828197e8"), new Guid("ec38f2a7-5b5f-4422-8d61-0fd21a0c860e") },
                    { new Guid("75c62c2c-025a-40e9-beb7-a7f42350fbb7"), 0m, new Guid("98d7a0df-d4af-4408-85e3-19ad0c74ed5b"), new Guid("ec38f2a7-5b5f-4422-8d61-0fd21a0c860e") },
                    { new Guid("929586c8-7f25-4583-b105-60b0713cbeb2"), 0m, new Guid("fe59caa8-249c-4db5-b700-f40e3acade15"), new Guid("32810ebd-20d1-49d3-8846-e4760bf8a568") },
                    { new Guid("b5868064-8697-493e-b3a0-89025c1137f4"), 0m, new Guid("18081637-6a28-4de3-a797-4eb340092044"), new Guid("32810ebd-20d1-49d3-8846-e4760bf8a568") },
                    { new Guid("d6a91e97-ae87-4f3d-8ff5-820dfe1ac513"), 0m, new Guid("18081637-6a28-4de3-a797-4eb340092044"), new Guid("ec38f2a7-5b5f-4422-8d61-0fd21a0c860e") },
                    { new Guid("ea9b2daa-8b28-4d03-b0ea-dbea16b2b9a6"), 0m, new Guid("e56277ad-bb5a-4fc8-b4ab-7b6cb56a4e6f"), new Guid("ec38f2a7-5b5f-4422-8d61-0fd21a0c860e") },
                    { new Guid("ed5a4252-47b4-4848-bed7-4e8483d49f2d"), 0m, new Guid("e56277ad-bb5a-4fc8-b4ab-7b6cb56a4e6f"), new Guid("32810ebd-20d1-49d3-8846-e4760bf8a568") },
                    { new Guid("fcff9f53-9ee1-400f-83bf-3a1f2a59361f"), 0m, new Guid("c9f2924e-523c-46b1-bdd1-404b3a972d41"), new Guid("32810ebd-20d1-49d3-8846-e4760bf8a568") }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("b1deb257-494e-44db-9767-8a9aee44cc55"), new Guid("32810ebd-20d1-49d3-8846-e4760bf8a568") },
                    { new Guid("f0a5b9ba-08ec-4c37-ad09-601c3094a042"), new Guid("32810ebd-20d1-49d3-8846-e4760bf8a568") },
                    { new Guid("f0a5b9ba-08ec-4c37-ad09-601c3094a042"), new Guid("ec38f2a7-5b5f-4422-8d61-0fd21a0c860e") }
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
                name: "IX_MoneyOperations_CommissionId",
                table: "MoneyOperations",
                column: "CommissionId");

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
                name: "MoneyAccounts");

            migrationBuilder.DropTable(
                name: "MoneyOperations");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Commissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
