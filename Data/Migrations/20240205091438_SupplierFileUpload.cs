using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invenio.Data.Migrations
{
    /// <inheritdoc />
    public partial class SupplierFileUpload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoPath",
                table: "Suppliers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoPath",
                table: "Suppliers");
        }
    }
}
