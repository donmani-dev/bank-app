using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankTrackingSystem.Migrations
{
    /// <inheritdoc />
    public partial class added_status_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "accountStatus",
                table: "Messages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "accountStatus",
                table: "Messages");
        }
    }
}
