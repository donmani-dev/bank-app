using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankTrackingSystem.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDataTypesToLong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ApplicantId",
                table: "Messages",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicantId",
                table: "Messages",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER");
        }
    }
}
