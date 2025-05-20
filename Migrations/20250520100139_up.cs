using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Registeration.Migrations
{
    /// <inheritdoc />
    public partial class up : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessCode",
                table: "OtherTeacher",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "OtherTeacher",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "OtherTeacher",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RegionId",
                table: "OtherTeacher",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OtherTeacher_RegionId",
                table: "OtherTeacher",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherTeacher_Regions_RegionId",
                table: "OtherTeacher",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtherTeacher_Regions_RegionId",
                table: "OtherTeacher");

            migrationBuilder.DropIndex(
                name: "IX_OtherTeacher_RegionId",
                table: "OtherTeacher");

            migrationBuilder.DropColumn(
                name: "AccessCode",
                table: "OtherTeacher");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "OtherTeacher");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "OtherTeacher");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "OtherTeacher");
        }
    }
}
