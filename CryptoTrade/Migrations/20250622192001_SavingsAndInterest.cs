using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoTrade.Migrations
{
    /// <inheritdoc />
    public partial class SavingsAndInterest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InterestRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CryptoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InterestRate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Savings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CryptoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    ExcpedtedValueAfterLock = table.Column<double>(type: "float", nullable: false),
                    LockTimeInDays = table.Column<int>(type: "int", nullable: false),
                    DailyInterest = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Savings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterestRates");

            migrationBuilder.DropTable(
                name: "Savings");
        }
    }
}
