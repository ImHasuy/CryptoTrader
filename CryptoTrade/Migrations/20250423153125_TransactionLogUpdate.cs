using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoTrade.Migrations
{
    /// <inheritdoc />
    public partial class TransactionLogUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBuy",
                table: "TransactionLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBuy",
                table: "TransactionLogs");
        }
    }
}
