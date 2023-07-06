using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingWizard.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Lat",
                table: "Address",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Lng",
                table: "Address",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "Address");
        }
    }
}
