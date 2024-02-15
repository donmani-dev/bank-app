using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankTrackingSystem.Migrations
{
    /// <inheritdoc />
    public partial class added_email_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicantEmailAddress",
                table: "Messages",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicantEmailAddress",
                table: "Messages");
        }
    }
}
