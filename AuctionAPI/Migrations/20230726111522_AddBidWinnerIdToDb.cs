using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddBidWinnerIdToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BidWinnerId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_BidWinnerId",
                table: "Products",
                column: "BidWinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_BidWinnerId",
                table: "Products",
                column: "BidWinnerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_BidWinnerId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BidWinnerId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BidWinnerId",
                table: "Products");
        }
    }
}
