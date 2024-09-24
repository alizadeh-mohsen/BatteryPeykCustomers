using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BatteryPeykCustomers.Migrations
{
    /// <inheritdoc />
    public partial class removedcountd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credit_Amper_AmperId",
                table: "Credit");

            migrationBuilder.DropForeignKey(
                name: "FK_Credit_Company_CompanyId",
                table: "Credit");

            migrationBuilder.DropIndex(
                name: "IX_Credit_AmperId",
                table: "Credit");

            migrationBuilder.DropIndex(
                name: "IX_Credit_CompanyId",
                table: "Credit");

            migrationBuilder.DropColumn(
                name: "AmperId",
                table: "Credit");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Credit");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Credit",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Credit",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Credit");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Credit");

            migrationBuilder.AddColumn<int>(
                name: "AmperId",
                table: "Credit",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Credit",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Credit_AmperId",
                table: "Credit",
                column: "AmperId");

            migrationBuilder.CreateIndex(
                name: "IX_Credit_CompanyId",
                table: "Credit",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Credit_Amper_AmperId",
                table: "Credit",
                column: "AmperId",
                principalTable: "Amper",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Credit_Company_CompanyId",
                table: "Credit",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
