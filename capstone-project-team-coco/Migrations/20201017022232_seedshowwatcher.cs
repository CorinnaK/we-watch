using Microsoft.EntityFrameworkCore.Migrations;

namespace we_watch.Migrations
{
    public partial class seedshowwatcher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchHistory_ShowCard",
                table: "WatchHistory");

            migrationBuilder.DropColumn(
                name: "TotalSeasons",
                table: "show");

            migrationBuilder.RenameIndex(
                name: "FK_WatchHistory_ShowCard",
                table: "WatchHistory",
                newName: "IX_WatchHistory_ShowCardID");

            migrationBuilder.AlterColumn<string>(
                name: "Salt",
                table: "User",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_general_ci");

            migrationBuilder.InsertData(
                table: "Watcher",
                columns: new[] { "WatcherID", "Name" },
                values: new object[,]
                {
                    { -1, "Bob Jones" },
                    { -2, "Sally Jenkins" },
                    { -3, "Austin Jane" }
                });

            migrationBuilder.InsertData(
                table: "show",
                columns: new[] { "ShowID", "Title" },
                values: new object[,]
                {
                    { -1, "Game of Thrones" },
                    { -2, "American Idol" },
                    { -3, "Fringe" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_WatchHistory_ShowCard_ShowCardID",
                table: "WatchHistory",
                column: "ShowCardID",
                principalTable: "ShowCard",
                principalColumn: "ShowCardID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchHistory_ShowCard_ShowCardID",
                table: "WatchHistory");

            migrationBuilder.DeleteData(
                table: "Watcher",
                keyColumn: "WatcherID",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Watcher",
                keyColumn: "WatcherID",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Watcher",
                keyColumn: "WatcherID",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "show",
                keyColumn: "ShowID",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "show",
                keyColumn: "ShowID",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "show",
                keyColumn: "ShowID",
                keyValue: -1);

            migrationBuilder.RenameIndex(
                name: "IX_WatchHistory_ShowCardID",
                table: "WatchHistory",
                newName: "FK_WatchHistory_ShowCard");

            migrationBuilder.AlterColumn<string>(
                name: "Salt",
                table: "User",
                type: "varchar(32)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:Collation", "utf8mb4_general_ci");

            migrationBuilder.AddColumn<short>(
                name: "TotalSeasons",
                table: "show",
                type: "smallint(2)",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchHistory_ShowCard",
                table: "WatchHistory",
                column: "ShowCardID",
                principalTable: "ShowCard",
                principalColumn: "ShowCardID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
