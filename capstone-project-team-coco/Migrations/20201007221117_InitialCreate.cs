using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace we_watch.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "varchar(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    Salt = table.Column<string>(type: "varchar(32)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    HashPassword = table.Column<string>(type: "char(64)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "show",
                columns: table => new
                {
                    ShowID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(type: "int(10)", nullable: false),
                    Title = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    TotalSeasons = table.Column<short>(type: "smallint(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_show", x => x.ShowID);
                    table.ForeignKey(
                        name: "FK_Show_TheUser",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WatcherID",
                columns: table => new
                {
                    WatcherID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(type: "int(10)", nullable: false),
                    Name = table.Column<string>(type: "varchar(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatcherID", x => x.WatcherID);
                    table.ForeignKey(
                        name: "FK_Watcher_TheUser",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShowSeason",
                columns: table => new
                {
                    ShowSeasonID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShowID = table.Column<int>(type: "int(10)", nullable: false),
                    IndividualSeason = table.Column<short>(type: "smallint(2)", nullable: false),
                    SeasonEpisodes = table.Column<short>(type: "smallint(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowSeason", x => x.ShowSeasonID);
                    table.ForeignKey(
                        name: "FK_ShowSeason_Show",
                        column: x => x.ShowID,
                        principalTable: "show",
                        principalColumn: "ShowID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShowCard",
                columns: table => new
                {
                    ShowCardID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShowID = table.Column<int>(type: "int(10)", nullable: false),
                    WatcherID = table.Column<int>(type: "int(10)", nullable: false),
                    Platform = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    CurrentSeason = table.Column<short>(type: "smallint(2)", nullable: false),
                    CurrentEpisode = table.Column<short>(type: "smallint(2)", nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowCard", x => x.ShowCardID);
                    table.ForeignKey(
                        name: "FK_ShowCard_Show",
                        column: x => x.ShowID,
                        principalTable: "show",
                        principalColumn: "ShowID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShowCard_Watcher",
                        column: x => x.WatcherID,
                        principalTable: "WatcherID",
                        principalColumn: "WatcherID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "watch_history",
                columns: table => new
                {
                    WatchHistoryID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShowCardID = table.Column<int>(type: "int(10)", nullable: false),
                    SeasonNum = table.Column<sbyte>(type: "tinyint(2)", nullable: false),
                    EpisodeNum = table.Column<sbyte>(type: "tinyint(2)", nullable: false),
                    Platform = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_watch_history", x => x.WatchHistoryID);
                    table.ForeignKey(
                        name: "FK_ShowSeason_Show",
                        column: x => x.ShowCardID,
                        principalTable: "ShowCard",
                        principalColumn: "ShowCardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "FK_Show_TheUser",
                table: "show",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "FK_ShowCard_Show",
                table: "ShowCard",
                column: "ShowID");

            migrationBuilder.CreateIndex(
                name: "FK_ShowCard_Watcher",
                table: "ShowCard",
                column: "WatcherID");

            migrationBuilder.CreateIndex(
                name: "FK_ShowSeason_Show",
                table: "ShowSeason",
                column: "ShowID");

            migrationBuilder.CreateIndex(
                name: "FK_WatchHistory_ShowCard",
                table: "watch_history",
                column: "ShowCardID");

            migrationBuilder.CreateIndex(
                name: "FK_Watcher_TheUser",
                table: "WatcherID",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShowSeason");

            migrationBuilder.DropTable(
                name: "watch_history");

            migrationBuilder.DropTable(
                name: "ShowCard");

            migrationBuilder.DropTable(
                name: "show");

            migrationBuilder.DropTable(
                name: "WatcherID");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
