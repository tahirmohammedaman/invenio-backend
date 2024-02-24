using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invenio.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameProductImagestoImagePaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Images",
                table: "Products",
                newName: "ImagePaths");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePaths",
                table: "Products",
                newName: "Images");
        }
    }
}
