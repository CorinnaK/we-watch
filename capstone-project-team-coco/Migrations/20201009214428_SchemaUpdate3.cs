using Microsoft.EntityFrameworkCore.Migrations;

namespace we_watch.Migrations
{
    public partial class SchemaUpdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "FK_Watcher_User",
                table: "Watcher");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Watcher");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "show");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Watcher",
                type: "int(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "show",
                type: "int(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "FK_Watcher_User",
                table: "Watcher",
                column: "UserID");
        }
    }
}
