using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LapShop.MVC.Migrations
{
    /// <inheritdoc />
    public partial class addRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01979187-c392-77be-89d1-ffe5c2623fae", "01979187-c392-77be-89d1-ffe6910b4102", "ApplicationRole", false, false, "Admin", "ADMIN" },
                    { "01979187-c392-77be-89d1-ffe7d2df25d4", "01979187-c392-77be-89d1-ffe8850dfea6", "ApplicationRole", true, false, "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDisabled", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "01978f6e-267c-7c96-b179-d2e08d73f69b", 0, "01078f6e-267c-7c96-b179-d2e127b3bb2c", "admin@lap-shop.com", true, "Survey Basket", false, "Admin", false, null, "ADMIN@LAP-SHOP.COM", "ADMIN@LAP-SHOP.COM", "AQAAAAIAAYagAAAAEOm19rPH9+gX4oopeWRXkyo+gPyFA3OBPZVkNori56HqKq38sCBNntaiBa7n8sADpA==", null, false, "35744942C16C49B9B2300A03094FAF85", false, "admin@lap-shop.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "01979187-c392-77be-89d1-ffe5c2623fae", "01978f6e-267c-7c96-b179-d2e08d73f69b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01979187-c392-77be-89d1-ffe7d2df25d4");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "01979187-c392-77be-89d1-ffe5c2623fae", "01978f6e-267c-7c96-b179-d2e08d73f69b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01979187-c392-77be-89d1-ffe5c2623fae");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "01978f6e-267c-7c96-b179-d2e08d73f69b");
        }
    }
}
