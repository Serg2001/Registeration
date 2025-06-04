using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Registeration.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintOnOtherPupil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OtherPupil_FullName_SocNumber_Id",
                table: "OtherPupil");

            migrationBuilder.CreateIndex(
                name: "IX_OtherPupil_FullName_SocNumber_SchoolId",
                table: "OtherPupil",
                columns: new[] { "FullName", "SocNumber", "SchoolId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OtherPupil_FullName_SocNumber_SchoolId",
                table: "OtherPupil");

            migrationBuilder.CreateIndex(
                name: "IX_OtherPupil_FullName_SocNumber_Id",
                table: "OtherPupil",
                columns: new[] { "FullName", "SocNumber", "Id" },
                unique: true);
        }
    }
}
