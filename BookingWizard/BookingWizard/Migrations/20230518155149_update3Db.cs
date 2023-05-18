using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingWizard.Migrations
{
    /// <inheritdoc />
    public partial class update3Db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HotelDescription",
                table: "hotels",
                newName: "HotelShortDescription");

            migrationBuilder.AddColumn<string>(
                name: "HotelLongDescription",
                table: "hotels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HotelLongDescription",
                table: "hotels");

            migrationBuilder.RenameColumn(
                name: "HotelShortDescription",
                table: "hotels",
                newName: "HotelDescription");
        }
    }
}
