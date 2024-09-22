using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BatteryPeykCustomers.Migrations
{
    /// <inheritdoc />
    public partial class final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debt_Reason_ReasonId",
                table: "Debt");

            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Counterparty_CounterpartyId",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Reason_ReasonId",
                table: "Expense");

            migrationBuilder.DropIndex(
                name: "IX_Expense_CounterpartyId",
                table: "Expense");

            migrationBuilder.DropIndex(
                name: "IX_Expense_ReasonId",
                table: "Expense");

            migrationBuilder.DropIndex(
                name: "IX_Debt_ReasonId",
                table: "Debt");

            migrationBuilder.CreateTable(
                name: "Guarranty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AmperId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guarranty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guarranty_Amper_AmperId",
                        column: x => x.AmperId,
                        principalTable: "Amper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Guarranty_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profit", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guarranty_AmperId",
                table: "Guarranty",
                column: "AmperId");

            migrationBuilder.CreateIndex(
                name: "IX_Guarranty_CompanyId",
                table: "Guarranty",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guarranty");

            migrationBuilder.DropTable(
                name: "Profit");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_CounterpartyId",
                table: "Expense",
                column: "CounterpartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_ReasonId",
                table: "Expense",
                column: "ReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Debt_ReasonId",
                table: "Debt",
                column: "ReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Debt_Reason_ReasonId",
                table: "Debt",
                column: "ReasonId",
                principalTable: "Reason",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Counterparty_CounterpartyId",
                table: "Expense",
                column: "CounterpartyId",
                principalTable: "Counterparty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Reason_ReasonId",
                table: "Expense",
                column: "ReasonId",
                principalTable: "Reason",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
