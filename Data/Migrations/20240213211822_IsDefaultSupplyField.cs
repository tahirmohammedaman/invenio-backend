using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invenio.Data.Migrations
{
    /// <inheritdoc />
    public partial class IsDefaultSupplyField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefaultSupply",
                table: "Supplies",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefaultSupply",
                table: "Supplies");
        }
    }
}
