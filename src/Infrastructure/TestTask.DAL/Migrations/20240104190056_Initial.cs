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
                    { new Guid("0e564296-5a8b-4c6d-91c6-ecfbde48650b"), new Guid("eaefb5a3-e088-4e02-9302-b72bb2d41ac3"), new Guid("f37ee4ac-a1d5-40e0-991a-c210608f57e8"), 0.1m },
                    { new Guid("3b17bbec-6d82-4a8c-bf3f-dd8e191a2f85"), new Guid("f37ee4ac-a1d5-40e0-991a-c210608f57e8"), new Guid("63bec71f-cdf6-4a74-a6b3-8573f8f6deeb"), 0.15m },
                    { new Guid("5f295087-2358-4568-a3da-77155799d3e5"), new Guid("98357d6f-bac0-41f2-be65-a2395b1b191a"), new Guid("eaefb5a3-e088-4e02-9302-b72bb2d41ac3"), 0.14m },
                    { new Guid("a24c421e-1f7e-443d-8062-41a38c372471"), new Guid("f37ee4ac-a1d5-40e0-991a-c210608f57e8"), new Guid("eaefb5a3-e088-4e02-9302-b72bb2d41ac3"), 0.21m },
                    { new Guid("a2621aeb-2d61-4aa8-85e4-9d8a8c3dbf29"), new Guid("eaefb5a3-e088-4e02-9302-b72bb2d41ac3"), new Guid("98357d6f-bac0-41f2-be65-a2395b1b191a"), 0.08m }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "AlphabeticCode", "NumericCode", "Title" },
                values: new object[,]
                {
                    { new Guid("63bec71f-cdf6-4a74-a6b3-8573f8f6deeb"), "EUR", "978", "Euro" },
                    { new Guid("98357d6f-bac0-41f2-be65-a2395b1b191a"), "USD", "840", "United States Dollar" },
                    { new Guid("b59103c2-3981-4040-b781-2b756ec90537"), "JPY", "392", "Japanese Yen" },
                    { new Guid("bcee18dd-2a87-4a22-9dcd-723d06e8bb0a"), "GBP", "826", "British Pound Sterling" },
                    { new Guid("eaefb5a3-e088-4e02-9302-b72bb2d41ac3"), "UAH", "980", "Ukrainian Hryvnia" },
                    { new Guid("f37ee4ac-a1d5-40e0-991a-c210608f57e8"), "RUB", "643", "Russian Ruble" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("57c91276-93d9-4b32-b468-510cf1581316"), "Admin" },
                    { new Guid("da754e23-9a83-4b27-a888-ce2be8bdcacc"), "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("6e5fa713-85b7-45b3-a449-84db569fbd68"), "popka@gmail.com", "John Doe", "$2a$11$iAmaLZfjKjvAW2loyO7syuvYNl0tL8dDUFLdUUmzR1SQQPXasXgV6" },
                    { new Guid("d9ac5237-cf1e-4caa-9f28-577c81590bde"), "penis@gmail.com", "Mike Vazovskiy", "$2a$11$0N4916UYQVUVa8po.3JBSetUNxmmVflGtRWToySL2rKFMBl7HV0vW" }
                });

            migrationBuilder.InsertData(
                table: "MoneyAccounts",
                columns: new[] { "Id", "Balance", "CurrencyId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0fc419f4-c255-4551-894f-3e8242c67f69"), 0m, new Guid("b59103c2-3981-4040-b781-2b756ec90537"), new Guid("d9ac5237-cf1e-4caa-9f28-577c81590bde") },
                    { new Guid("22911a6c-3069-4f2b-b7e6-7f9b5671d112"), 0m, new Guid("eaefb5a3-e088-4e02-9302-b72bb2d41ac3"), new Guid("6e5fa713-85b7-45b3-a449-84db569fbd68") },
                    { new Guid("2db58c2a-7c53-471b-882b-713691d906b4"), 0m, new Guid("63bec71f-cdf6-4a74-a6b3-8573f8f6deeb"), new Guid("6e5fa713-85b7-45b3-a449-84db569fbd68") },
                    { new Guid("331625f7-2b22-4490-8892-fa6ab53b386d"), 0m, new Guid("eaefb5a3-e088-4e02-9302-b72bb2d41ac3"), new Guid("d9ac5237-cf1e-4caa-9f28-577c81590bde") },
                    { new Guid("36468dce-a1a7-463b-8785-f62a4d24f9a8"), 0m, new Guid("f37ee4ac-a1d5-40e0-991a-c210608f57e8"), new Guid("6e5fa713-85b7-45b3-a449-84db569fbd68") },
                    { new Guid("53ae5ea7-1cc3-4e4d-8e3e-76a6d161f37f"), 0m, new Guid("98357d6f-bac0-41f2-be65-a2395b1b191a"), new Guid("6e5fa713-85b7-45b3-a449-84db569fbd68") },
                    { new Guid("68126598-094b-43b9-b7d2-7f493eaf474f"), 0m, new Guid("f37ee4ac-a1d5-40e0-991a-c210608f57e8"), new Guid("d9ac5237-cf1e-4caa-9f28-577c81590bde") },
                    { new Guid("76de6af2-d93f-4ccb-8652-ea89e5735dab"), 0m, new Guid("bcee18dd-2a87-4a22-9dcd-723d06e8bb0a"), new Guid("6e5fa713-85b7-45b3-a449-84db569fbd68") },
                    { new Guid("90b5c6dd-b33e-4834-a2ae-dbcb64a67fc3"), 0m, new Guid("98357d6f-bac0-41f2-be65-a2395b1b191a"), new Guid("d9ac5237-cf1e-4caa-9f28-577c81590bde") },
                    { new Guid("c7fcfc4c-3772-42c0-b735-e145acc5bba0"), 0m, new Guid("63bec71f-cdf6-4a74-a6b3-8573f8f6deeb"), new Guid("d9ac5237-cf1e-4caa-9f28-577c81590bde") },
                    { new Guid("e2230fd7-50ae-4172-97bf-399cfa885d9b"), 0m, new Guid("bcee18dd-2a87-4a22-9dcd-723d06e8bb0a"), new Guid("d9ac5237-cf1e-4caa-9f28-577c81590bde") },
                    { new Guid("ec72f279-fa77-43de-a2a5-75e19c695edd"), 0m, new Guid("b59103c2-3981-4040-b781-2b756ec90537"), new Guid("6e5fa713-85b7-45b3-a449-84db569fbd68") }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("da754e23-9a83-4b27-a888-ce2be8bdcacc"), new Guid("6e5fa713-85b7-45b3-a449-84db569fbd68") },
                    { new Guid("57c91276-93d9-4b32-b468-510cf1581316"), new Guid("d9ac5237-cf1e-4caa-9f28-577c81590bde") },
                    { new Guid("da754e23-9a83-4b27-a888-ce2be8bdcacc"), new Guid("d9ac5237-cf1e-4caa-9f28-577c81590bde") }
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
