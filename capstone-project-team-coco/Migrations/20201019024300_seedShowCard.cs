using Microsoft.EntityFrameworkCore.Migrations;

namespace we_watch.Migrations
{
    public partial class seedShowCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowCard_User",
                table: "ShowCard");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "ShowCard");

            migrationBuilder.InsertData(
                table: "ShowCard",
                columns: new[] { "ShowCardID", "CurrentEpisode", "CurrentSeason", "ShowID", "Status", "UserID", "WatcherID" },
                values: new object[,]
                {
                    { -1, (short)1, (short)-2, -1, "Current", -2, -2 },
                    { -2, (short)10, (short)-2, -1, "Current", -2, -3 },
                    { -3, (short)12, (short)-4, -2, "Current", -3, -3 },
                    { -4, (short)30, (short)-5, -2, "Current", -3, -1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ShowCard_User",
                table: "ShowCard",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowCard_User",
                table: "ShowCard");

            migrationBuilder.DeleteData(
                table: "ShowCard",
                keyColumn: "ShowCardID",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "ShowCard",
                keyColumn: "ShowCardID",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "ShowCard",
                keyColumn: "ShowCardID",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "ShowCard",
                keyColumn: "ShowCardID",
                keyValue: -1);

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "ShowCard",
                type: "varchar(20)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("MySql:Collation", "utf8mb4_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowCard_User",
                table: "ShowCard",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
