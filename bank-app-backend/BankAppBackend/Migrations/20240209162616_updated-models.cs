using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAppBackend.Migrations
{
    /// <inheritdoc />
    public partial class updatedmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Tellers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Tellers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Applicants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tellers_EmailAddress",
                table: "Tellers",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_EmailAddress",
                table: "Applicants",
                column: "EmailAddress",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tellers_EmailAddress",
                table: "Tellers");

            migrationBuilder.DropIndex(
                name: "IX_Applicants_EmailAddress",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Tellers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Tellers");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Applicants");
        }
    }
}
