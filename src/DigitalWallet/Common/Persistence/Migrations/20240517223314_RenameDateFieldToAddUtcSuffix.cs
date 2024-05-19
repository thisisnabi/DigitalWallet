using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalWallet.Common.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameDateFieldToAddUtcSuffix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                schema: "wallet",
                table: "Wallets",
                newName: "CreatedOnUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                schema: "wallet",
                table: "Transactions",
                newName: "CreatedOnUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                schema: "wallet",
                table: "Wallets",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUtc",
                schema: "wallet",
                table: "Transactions",
                newName: "CreatedOn");
        }
    }
}
