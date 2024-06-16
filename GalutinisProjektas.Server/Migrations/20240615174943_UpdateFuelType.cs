using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalutinisProjektas.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFuelType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FuelName",
                table: "FuelTypes",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FuelName",
                table: "FuelTypes");
        }
    }
}
