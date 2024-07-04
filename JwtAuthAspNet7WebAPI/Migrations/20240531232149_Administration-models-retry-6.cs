using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtAuthAspNet7WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Administrationmodelsretry6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_ApplicationUserDto_EmployeeId",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Documents",
                newName: "EmployeeUserName");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_EmployeeId",
                table: "Documents",
                newName: "IX_Documents_EmployeeUserName");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_ApplicationUserDto_EmployeeUserName",
                table: "Documents",
                column: "EmployeeUserName",
                principalTable: "ApplicationUserDto",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_ApplicationUserDto_EmployeeUserName",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "EmployeeUserName",
                table: "Documents",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_EmployeeUserName",
                table: "Documents",
                newName: "IX_Documents_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_ApplicationUserDto_EmployeeId",
                table: "Documents",
                column: "EmployeeId",
                principalTable: "ApplicationUserDto",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
