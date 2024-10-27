using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalSite.Data.Migrations
{
    public partial class UpdatedatatypeforWishListId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_WishList",
                table: "WishList");

            // Drop the old WishListId column
            migrationBuilder.DropColumn(
                name: "WishListId",
                table: "WishList");

            // Add the new WishListId column with the IDENTITY property
            migrationBuilder.AddColumn<int>(
                name: "WishListId",
                table: "WishList",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1"); // This sets the IDENTITY property

            // Re-add the primary key constraint on the new WishListId column
            migrationBuilder.AddPrimaryKey(
                name: "PK_WishList",
                table: "WishList",
                column: "WishListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverse the process in case of rollback
            migrationBuilder.DropPrimaryKey(
                name: "PK_WishList",
                table: "WishList");

            migrationBuilder.DropColumn(
                name: "WishListId",
                table: "WishList");

            migrationBuilder.AddColumn<int>(
                name: "WishListId",
                table: "WishList",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishList",
                table: "WishList",
                column: "WishListId");
        }
    }
}
