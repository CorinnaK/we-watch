using Microsoft.EntityFrameworkCore.Migrations;

namespace we_watch.Migrations
{
    public partial class seedShowSeason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ShowSeason",
                columns: new[] { "ShowSeasonID", "IndividualSeason", "SeasonEpisodes", "ShowID" },
                values: new object[,]
                {
                    { -1, (short)1, (short)10, -1 },
                    { -2, (short)2, (short)10, -1 },
                    { -3, (short)3, (short)10, -1 },
                    { -4, (short)1, (short)25, -2 },
                    { -5, (short)8, (short)40, -2 },
                    { -6, (short)5, (short)13, -3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShowSeason",
                keyColumn: "ShowSeasonID",
                keyValue: -6);

            migrationBuilder.DeleteData(
                table: "ShowSeason",
                keyColumn: "ShowSeasonID",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "ShowSeason",
                keyColumn: "ShowSeasonID",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "ShowSeason",
                keyColumn: "ShowSeasonID",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "ShowSeason",
                keyColumn: "ShowSeasonID",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "ShowSeason",
                keyColumn: "ShowSeasonID",
                keyValue: -1);
        }
    }
}
