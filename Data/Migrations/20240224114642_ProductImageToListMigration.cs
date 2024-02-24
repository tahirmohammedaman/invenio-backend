using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invenio.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductImageToListMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image1Path",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image2Path",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image3Path",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image4Path",
                table: "Products");

            migrationBuilder.AddColumn<List<string>>(
                name: "Images",
                table: "Products",
                type: "text[]",
                nullable: true);
            
            // Update existing entries' Images column to ['default_product_image.svg']
            migrationBuilder.Sql("UPDATE inveniodb.public.\"Products\" SET \"Images\" = ARRAY['default_product_image.svg']");

            // Alter the column to be non-nullable
            migrationBuilder.AlterColumn<string[]>(
                name: "Images",
                table: "Products",
                nullable: false
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Image1Path",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image2Path",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image3Path",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image4Path",
                table: "Products",
                type: "text",
                nullable: true);
        }
    }
}
