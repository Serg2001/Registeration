using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Registeration.Migrations
{
    /// <inheritdoc />
    public partial class PupilFull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OtherTeacher_FullName_SocNumber_SchoolId",
                table: "OtherTeacher");

            migrationBuilder.DropIndex(
                name: "IX_OtherPupil_FullName_SocNumber_SchoolId",
                table: "OtherPupil");

            migrationBuilder.DropColumn(
                name: "IsArmenianCitizen",
                table: "OtherPupil");

            migrationBuilder.CreateIndex(
                name: "IX_OtherTeacher_SocNumber_SchoolId",
                table: "OtherTeacher",
                columns: new[] { "SocNumber", "SchoolId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OtherPupil_Grade_SocNumber_SchoolId",
                table: "OtherPupil",
                columns: new[] { "Grade", "SocNumber", "SchoolId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OtherTeacher_SocNumber_SchoolId",
                table: "OtherTeacher");

            migrationBuilder.DropIndex(
                name: "IX_OtherPupil_Grade_SocNumber_SchoolId",
                table: "OtherPupil");

            migrationBuilder.AddColumn<bool>(
                name: "IsArmenianCitizen",
                table: "OtherPupil",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_OtherTeacher_FullName_SocNumber_SchoolId",
                table: "OtherTeacher",
                columns: new[] { "FullName", "SocNumber", "SchoolId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OtherPupil_FullName_SocNumber_SchoolId",
                table: "OtherPupil",
                columns: new[] { "FullName", "SocNumber", "SchoolId" },
                unique: true);
        }
    }
}
