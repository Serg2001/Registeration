using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Registeration.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexOnEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Registrations_Email",
                table: "Registrations",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Registrations_Email",
                table: "Registrations");
        }
    }
}
