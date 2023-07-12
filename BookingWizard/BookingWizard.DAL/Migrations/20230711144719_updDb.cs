using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingWizard.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Cultures_CultureId",
                table: "Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resources",
                table: "Resources");

            migrationBuilder.RenameTable(
                name: "Resources",
                newName: "Resource");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_CultureId",
                table: "Resource",
                newName: "IX_Resource_CultureId");

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "hotels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resource",
                table: "Resource",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resource_Cultures_CultureId",
                table: "Resource",
                column: "CultureId",
                principalTable: "Cultures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resource_Cultures_CultureId",
                table: "Resource");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resource",
                table: "Resource");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "hotels");

            migrationBuilder.RenameTable(
                name: "Resource",
                newName: "Resources");

            migrationBuilder.RenameIndex(
                name: "IX_Resource_CultureId",
                table: "Resources",
                newName: "IX_Resources_CultureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resources",
                table: "Resources",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Cultures_CultureId",
                table: "Resources",
                column: "CultureId",
                principalTable: "Cultures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
