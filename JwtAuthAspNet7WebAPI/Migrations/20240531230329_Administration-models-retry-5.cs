using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtAuthAspNet7WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Administrationmodelsretry5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_ApplicationUserDto_EmployeeUserName",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_EmployeeUserName",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "EmployeeUserName",
                table: "Documents");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Documents",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_EmployeeId",
                table: "Documents",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_ApplicationUserDto_EmployeeId",
                table: "Documents",
                column: "EmployeeId",
                principalTable: "ApplicationUserDto",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_ApplicationUserDto_EmployeeId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_EmployeeId",
                table: "Documents");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Documents",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeUserName",
                table: "Documents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_EmployeeUserName",
                table: "Documents",
                column: "EmployeeUserName");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_ApplicationUserDto_EmployeeUserName",
                table: "Documents",
                column: "EmployeeUserName",
                principalTable: "ApplicationUserDto",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
