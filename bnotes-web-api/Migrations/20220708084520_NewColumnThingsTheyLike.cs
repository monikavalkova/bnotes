using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bnotes_web_api.Migrations
{
    public partial class NewColumnThingsTheyLike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThingsTheyLike",
                table: "Friends",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThingsTheyLike",
                table: "Friends");
        }
    }
}
