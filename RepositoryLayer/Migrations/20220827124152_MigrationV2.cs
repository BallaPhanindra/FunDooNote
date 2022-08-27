using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class MigrationV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userId",
                table: "users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "EmailId",
                table: "users",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "users",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "EmailId");
        }
    }
}
