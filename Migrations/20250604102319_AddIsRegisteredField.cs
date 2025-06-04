using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Registeration.Migrations
{
    /// <inheritdoc />
    public partial class AddIsRegisteredField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isRegistered",
                table: "Registrations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isRegistered",
                table: "Registrations");
        }
    }
}
