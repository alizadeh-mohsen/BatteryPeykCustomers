using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BatteryPeykCustomers.Migrations
{
    /// <inheritdoc />
    public partial class inityd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Battery",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Car",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Guaranty",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "LifeExpectancy",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ReplaceDate",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "StopNotify",
                table: "Customer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Battery",
                table: "Customer",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Car",
                table: "Customer",
                type: "TEXT",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Customer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Guaranty",
                table: "Customer",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LifeExpectancy",
                table: "Customer",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Customer",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReplaceDate",
                table: "Customer",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "StopNotify",
                table: "Customer",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
