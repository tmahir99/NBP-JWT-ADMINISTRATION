using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtAuthAspNet7WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Administrationmodelsretry7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_ApplicationUserDto_EmployeeUserName",
                table: "Documents");

            migrationBuilder.DropTable(
                name: "ApplicationUserDto");

            migrationBuilder.DropIndex(
                name: "IX_Documents_EmployeeUserName",
                table: "Documents");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeUserName",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmployeeUserName",
                table: "Documents",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ApplicationUserDto",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserDto", x => x.UserName);
                });

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
