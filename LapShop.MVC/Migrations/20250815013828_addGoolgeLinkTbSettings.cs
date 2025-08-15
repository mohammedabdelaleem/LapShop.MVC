using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapShop.MVC.Migrations
{
    /// <inheritdoc />
    public partial class addGoolgeLinkTbSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoogleLink",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleLink",
                table: "Settings");
        }
    }
}
