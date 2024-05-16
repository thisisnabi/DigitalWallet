using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalWallet.Common.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDefualtSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "wallet");

            migrationBuilder.RenameTable(
                name: "Wallets",
                newName: "Wallets",
                newSchema: "wallet");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transactions",
                newSchema: "wallet");

            migrationBuilder.RenameTable(
                name: "Currencies",
                newName: "Currencies",
                newSchema: "wallet");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Wallets",
                schema: "wallet",
                newName: "Wallets");

            migrationBuilder.RenameTable(
                name: "Transactions",
                schema: "wallet",
                newName: "Transactions");

            migrationBuilder.RenameTable(
                name: "Currencies",
                schema: "wallet",
                newName: "Currencies");
        }
    }
}
