using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Books.data.Migrations
{
    /// <inheritdoc />
    public partial class Makingconnections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "listing",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_listing_BookId",
                table: "listing",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_listing_UsersUserId",
                table: "listing",
                column: "UsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_listing_books_BookId",
                table: "listing",
                column: "BookId",
                principalTable: "books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_listing_users_UsersUserId",
                table: "listing",
                column: "UsersUserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_listing_books_BookId",
                table: "listing");

            migrationBuilder.DropForeignKey(
                name: "FK_listing_users_UsersUserId",
                table: "listing");

            migrationBuilder.DropIndex(
                name: "IX_listing_BookId",
                table: "listing");

            migrationBuilder.DropIndex(
                name: "IX_listing_UsersUserId",
                table: "listing");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "listing");
        }
    }
}
