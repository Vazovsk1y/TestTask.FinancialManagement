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
                    { new Guid("3849c2af-ee6e-41b2-a099-c8e5bf25d10a"), new Guid("399cdcff-124c-4a25-bcec-57ce228313cf"), new Guid("493fe233-6f2e-4254-8329-6f552a5d5afa"), 0.14m },
                    { new Guid("5ae2e435-60d4-4d50-9726-16ce7bc81421"), new Guid("493fe233-6f2e-4254-8329-6f552a5d5afa"), new Guid("fc2b4267-b924-464f-83d0-60a139438f84"), 0.1m },
                    { new Guid("72b457a7-a6ce-4db5-b73e-6b16b7f756dd"), new Guid("fc2b4267-b924-464f-83d0-60a139438f84"), new Guid("a5b7f1c0-dda9-46fe-b8ee-772fcb03c958"), 0.15m },
                    { new Guid("b9e6d347-afd5-4f32-83e3-13331b5157c8"), new Guid("fc2b4267-b924-464f-83d0-60a139438f84"), new Guid("493fe233-6f2e-4254-8329-6f552a5d5afa"), 0.21m },
                    { new Guid("e04dde74-2a0a-4568-806f-5d60333ff2a1"), new Guid("493fe233-6f2e-4254-8329-6f552a5d5afa"), new Guid("399cdcff-124c-4a25-bcec-57ce228313cf"), 0.08m }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "AlphabeticCode", "NumericCode", "Title" },
                values: new object[,]
                {
                    { new Guid("399cdcff-124c-4a25-bcec-57ce228313cf"), "USD", "840", "United States Dollar" },
                    { new Guid("493fe233-6f2e-4254-8329-6f552a5d5afa"), "UAH", "980", "Ukrainian Hryvnia" },
                    { new Guid("7ea95efd-0b61-4288-b618-b576af4c9fcd"), "JPY", "392", "Japanese Yen" },
                    { new Guid("a5b7f1c0-dda9-46fe-b8ee-772fcb03c958"), "EUR", "978", "Euro" },
                    { new Guid("d77e172a-6405-4468-878f-a9957a8d71a6"), "GBP", "826", "British Pound Sterling" },
                    { new Guid("fc2b4267-b924-464f-83d0-60a139438f84"), "RUB", "643", "Russian Ruble" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("0991d748-59a9-4749-9a3b-41075b73de74"), "User" },
                    { new Guid("83742101-eaef-4efb-b488-1288320ac083"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("1f07aed5-aabf-4bf1-9361-4b13d22f8736"), "penis@gmail.com", "Mike Vazovskiy", "$2a$11$ISB8FqPJVNnu8oNVV9n5T.HkKOSb6HlG8lyvPBpp05oSivnLprKN2" },
                    { new Guid("4583d3c6-ec37-475f-881d-af4e8836f056"), "popka@gmail.com", "John Doe", "$2a$11$nc5WXaDZ9M7klakIa3ngnO1CtEnQbyJ2GTsrPD7ABfCKnqM/JWjPq" }
                });

            migrationBuilder.InsertData(
                table: "MoneyAccounts",
                columns: new[] { "Id", "Balance", "CurrencyId", "UserId" },
                values: new object[,]
                {
                    { new Guid("038fda73-ea1e-46a2-92f5-661131dc8633"), 0m, new Guid("d77e172a-6405-4468-878f-a9957a8d71a6"), new Guid("1f07aed5-aabf-4bf1-9361-4b13d22f8736") },
                    { new Guid("3765d195-bd79-47da-82bf-9ae1f2b1b36d"), 0m, new Guid("a5b7f1c0-dda9-46fe-b8ee-772fcb03c958"), new Guid("1f07aed5-aabf-4bf1-9361-4b13d22f8736") },
                    { new Guid("381d6dfa-b38a-4843-8d23-a66be218fcbe"), 0m, new Guid("7ea95efd-0b61-4288-b618-b576af4c9fcd"), new Guid("4583d3c6-ec37-475f-881d-af4e8836f056") },
                    { new Guid("5f1175ab-962d-43ad-9c8d-de00f7ddfa95"), 0m, new Guid("d77e172a-6405-4468-878f-a9957a8d71a6"), new Guid("4583d3c6-ec37-475f-881d-af4e8836f056") },
                    { new Guid("6733d192-e8b0-4c15-a731-f7251adad67a"), 0m, new Guid("399cdcff-124c-4a25-bcec-57ce228313cf"), new Guid("1f07aed5-aabf-4bf1-9361-4b13d22f8736") },
                    { new Guid("7a18c0ec-2084-436b-872d-229de74d5f11"), 0m, new Guid("a5b7f1c0-dda9-46fe-b8ee-772fcb03c958"), new Guid("4583d3c6-ec37-475f-881d-af4e8836f056") },
                    { new Guid("8e2b2013-2662-445c-9c43-7442aa36120b"), 0m, new Guid("493fe233-6f2e-4254-8329-6f552a5d5afa"), new Guid("1f07aed5-aabf-4bf1-9361-4b13d22f8736") },
                    { new Guid("992974d5-3de6-4326-901b-3aee87b003a1"), 0m, new Guid("fc2b4267-b924-464f-83d0-60a139438f84"), new Guid("4583d3c6-ec37-475f-881d-af4e8836f056") },
                    { new Guid("a47e0d4a-7da7-4ed4-bf25-c1e1bf0d2b0b"), 0m, new Guid("493fe233-6f2e-4254-8329-6f552a5d5afa"), new Guid("4583d3c6-ec37-475f-881d-af4e8836f056") },
                    { new Guid("ba209627-3a7e-4397-b9ed-9b254c3c4e98"), 0m, new Guid("7ea95efd-0b61-4288-b618-b576af4c9fcd"), new Guid("1f07aed5-aabf-4bf1-9361-4b13d22f8736") },
                    { new Guid("c57a479f-eb03-4764-8606-6c2697df3051"), 0m, new Guid("fc2b4267-b924-464f-83d0-60a139438f84"), new Guid("1f07aed5-aabf-4bf1-9361-4b13d22f8736") },
                    { new Guid("c7481c2a-e3a0-4713-8ad1-ec9e0a4793dc"), 0m, new Guid("399cdcff-124c-4a25-bcec-57ce228313cf"), new Guid("4583d3c6-ec37-475f-881d-af4e8836f056") }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0991d748-59a9-4749-9a3b-41075b73de74"), new Guid("1f07aed5-aabf-4bf1-9361-4b13d22f8736") },
                    { new Guid("83742101-eaef-4efb-b488-1288320ac083"), new Guid("1f07aed5-aabf-4bf1-9361-4b13d22f8736") },
                    { new Guid("0991d748-59a9-4749-9a3b-41075b73de74"), new Guid("4583d3c6-ec37-475f-881d-af4e8836f056") }
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
