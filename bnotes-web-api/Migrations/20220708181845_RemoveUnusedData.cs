using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bnotes_web_api.Migrations
{
    public partial class RemoveUnusedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropTable(
                name: "Notes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    FavouriteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FriendId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourites", x => x.FavouriteId);
                    table.ForeignKey(
                        name: "FK_Favourites_Friends_FriendId",
                        column: x => x.FriendId,
                        principalTable: "Friends",
                        principalColumn: "FriendId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FriendId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_Notes_Friends_FriendId",
                        column: x => x.FriendId,
                        principalTable: "Friends",
                        principalColumn: "FriendId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_FriendId",
                table: "Favourites",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_FriendId",
                table: "Notes",
                column: "FriendId");
        }
    }
}
