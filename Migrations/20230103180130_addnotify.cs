using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BatteryPeykCustomers.Migrations
{
    /// <inheritdoc />
    public partial class addnotify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReplaceDate",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "StopNotify",
                table: "Customer");
        }
    }
}
