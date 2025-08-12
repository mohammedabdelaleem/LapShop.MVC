using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapShop.MVC.Migrations
{
    /// <inheritdoc />
    public partial class AddingSecurityColumnsToSlider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TbSlider",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TbSlider",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CurrentState",
                table: "TbSlider",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "TbSlider",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "TbSlider",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "TbSalesInvoices",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TbSlider");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TbSlider");

            migrationBuilder.DropColumn(
                name: "CurrentState",
                table: "TbSlider");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TbSlider");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "TbSlider");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "TbSalesInvoices",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
