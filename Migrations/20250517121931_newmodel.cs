using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Registeration.Migrations
{
    /// <inheritdoc />
    public partial class newmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtherPupil_Regions_RegionId",
                table: "OtherPupil");

            migrationBuilder.DropIndex(
                name: "IX_OtherPupil_RegionId",
                table: "OtherPupil");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "OtherPupil");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RegionId",
                table: "OtherPupil",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OtherPupil_RegionId",
                table: "OtherPupil",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherPupil_Regions_RegionId",
                table: "OtherPupil",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
