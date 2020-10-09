using Microsoft.EntityFrameworkCore.Migrations;

namespace we_watch.Migrations
{
    public partial class SchemaUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "FK_Show_User",
                table: "show");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "FK_Show_User",
                table: "show",
                column: "UserID");
        }
    }
}
