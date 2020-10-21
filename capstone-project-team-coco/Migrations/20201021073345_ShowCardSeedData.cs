using Microsoft.EntityFrameworkCore.Migrations;

namespace we_watch.Migrations
{
    public partial class ShowCardSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ShowCard",
                columns: new[] { "ShowCardID", "CurrentEpisode", "CurrentSeason", "ShowID", "UserID", "WatcherID" },
                values: new object[,]
                {
                    { -1, (short)1, (short)-2, -1, -3, -2 },
                    { -2, (short)10, (short)-2, -1, -3, -3 },
                    { -3, (short)12, (short)-4, -2, -3, -3 },
                    { -4, (short)30, (short)-5, -2, -3, -1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
