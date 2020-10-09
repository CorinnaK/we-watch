using Microsoft.EntityFrameworkCore.Migrations;

namespace we_watch.Migrations
{
    public partial class SchemaUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Show_User",
                table: "show");

            migrationBuilder.DropForeignKey(
                name: "FK_Watcher_User",
                table: "Watcher");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "ShowCard",
                type: "int(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShowCard_UserID",
                table: "ShowCard",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowCard_User",
                table: "ShowCard",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowCard_User",
                table: "ShowCard");

            migrationBuilder.DropIndex(
                name: "IX_ShowCard_UserID",
                table: "ShowCard");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "ShowCard");

            migrationBuilder.AddForeignKey(
                name: "FK_Show_User",
                table: "show",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Watcher_User",
                table: "Watcher",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
