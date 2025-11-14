using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BatteryPeykCustomers.Migrations
{
    /// <inheritdoc />
    public partial class customer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UsedHistory_CustomerId",
                table: "UsedHistory",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsedHistory_Customer_CustomerId",
                table: "UsedHistory",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsedHistory_Customer_CustomerId",
                table: "UsedHistory");

            migrationBuilder.DropIndex(
                name: "IX_UsedHistory_CustomerId",
                table: "UsedHistory");
        }
    }
}
