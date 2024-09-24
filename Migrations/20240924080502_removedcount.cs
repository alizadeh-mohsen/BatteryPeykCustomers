using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BatteryPeykCustomers.Migrations
{
    /// <inheritdoc />
    public partial class removedcount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debt_Counterparty_CounterpartyId",
                table: "Debt");

            migrationBuilder.DropIndex(
                name: "IX_Debt_CounterpartyId",
                table: "Debt");

            migrationBuilder.DropColumn(
                name: "CounterpartyId",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "ReasonId",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "CounterpartyId",
                table: "Debt");

            migrationBuilder.DropColumn(
                name: "ReasonId",
                table: "Debt");

            migrationBuilder.DropColumn(
                name: "Settled",
                table: "Debt");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Expense",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Debt",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Credit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    AmperId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credit_Amper_AmperId",
                        column: x => x.AmperId,
                        principalTable: "Amper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Credit_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Credit_AmperId",
                table: "Credit",
                column: "AmperId");

            migrationBuilder.CreateIndex(
                name: "IX_Credit_CompanyId",
                table: "Credit",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Credit");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Expense",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "CounterpartyId",
                table: "Expense",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReasonId",
                table: "Expense",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Debt",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "CounterpartyId",
                table: "Debt",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReasonId",
                table: "Debt",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Settled",
                table: "Debt",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Debt_CounterpartyId",
                table: "Debt",
                column: "CounterpartyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Debt_Counterparty_CounterpartyId",
                table: "Debt",
                column: "CounterpartyId",
                principalTable: "Counterparty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
