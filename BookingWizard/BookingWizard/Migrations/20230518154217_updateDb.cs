using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingWizard.Migrations
{
    /// <inheritdoc />
    public partial class updateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "bookingId",
                table: "hotels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    arrival_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    date_of_departure = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_hotels_bookingId",
                table: "hotels",
                column: "bookingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_hotels_Booking_bookingId",
                table: "hotels",
                column: "bookingId",
                principalTable: "Booking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hotels_Booking_bookingId",
                table: "hotels");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_hotels_bookingId",
                table: "hotels");

            migrationBuilder.DropColumn(
                name: "bookingId",
                table: "hotels");
        }
    }
}
