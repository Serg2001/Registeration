using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Registeration.Migrations
{
    /// <inheritdoc />
    public partial class NewDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtherPupil_Regions_RegionId",
                table: "OtherPupil");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherPupil_Regions_RegionId",
                table: "OtherPupil",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtherPupil_Regions_RegionId",
                table: "OtherPupil");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherPupil_Regions_RegionId",
                table: "OtherPupil",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
