using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Books.data.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToListings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookTitle",
                table: "listing",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "listing",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookTitle",
                table: "listing");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "listing");
        }
    }
}
