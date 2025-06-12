using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Registeration.Migrations
{
    /// <inheritdoc />
    public partial class OtherPupil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "OtherPupil",
                newName: "SurName");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OtherPupil",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "OtherPupil");

            migrationBuilder.RenameColumn(
                name: "SurName",
                table: "OtherPupil",
                newName: "FullName");
        }
    }
}
