using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Registeration.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexOnOtherPupil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SocNumber",
                table: "OtherPupil",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_OtherPupil_FullName_SocNumber_Id",
                table: "OtherPupil",
                columns: new[] { "FullName", "SocNumber", "Id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OtherPupil_FullName_SocNumber_Id",
                table: "OtherPupil");

            migrationBuilder.AlterColumn<string>(
                name: "SocNumber",
                table: "OtherPupil",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
