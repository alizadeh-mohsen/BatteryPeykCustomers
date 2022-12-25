using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BatteryPeykCustomers.Migrations
{
    /// <inheritdoc />
    public partial class life : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LifeExpectancy",
                table: "Customer",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LifeExpectancy",
                table: "Customer");
        }
    }
}
