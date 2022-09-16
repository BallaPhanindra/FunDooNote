using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class CollM5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collaborators",
                columns: table => new
                {
                    collaboratorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    collabEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userId = table.Column<int>(type: "int", nullable: false),
                    NoteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborators", x => x.collaboratorId);
                    table.ForeignKey(
                        name: "FK_Collaborators_Note_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Note",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Collaborators_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_NoteId",
                table: "Collaborators",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_userId",
                table: "Collaborators",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collaborators");
        }
    }
}
