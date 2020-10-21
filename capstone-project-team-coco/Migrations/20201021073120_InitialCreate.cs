using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace we_watch.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "show",
                columns: table => new
                {
                    ShowID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_show", x => x.ShowID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "varchar(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci"),
                    Salt = table.Column<string>(type: "varchar(10)", nullable: false)
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
                name: "Watcher",
                columns: table => new
                {
                    WatcherID = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Watcher", x => x.WatcherID);
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
                    UserID = table.Column<int>(type: "int(10)", nullable: false),
                    ShowID = table.Column<int>(type: "int(10)", nullable: false),
                    WatcherID = table.Column<int>(type: "int(10)", nullable: false),
                    CurrentSeason = table.Column<short>(type: "smallint(2)", nullable: false),
                    CurrentEpisode = table.Column<short>(type: "smallint(2)", nullable: false)
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
                        name: "FK_ShowCard_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShowCard_Watcher",
                        column: x => x.WatcherID,
                        principalTable: "Watcher",
                        principalColumn: "WatcherID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserID", "Email", "HashPassword", "Salt" },
                values: new object[,]
                {
                    { -1, "someone@somewhere.something", "2334814362998297759587574090140267323532918138392977707124924545", "wherew" },
                    { -2, "aspin@go.com", "2334814362998297759587574090140267323532918138392977707124924545", "Yes" },
                    { -3, "goaspin@gmail.com", "3/H/c1lljJe2l9+DQCsr3NSSPhFyj/SZV7hA5wUQxnI=", "1859530424" }
                });

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

            migrationBuilder.InsertData(
                table: "ShowCard",
                columns: new[] { "ShowCardID", "CurrentEpisode", "CurrentSeason", "ShowID", "UserID", "WatcherID" },
                values: new object[,]
                {
                    { -4, (short)30, (short)-5, -2, -3, -1 },
                    { -1, (short)1, (short)-2, -1, -3, -2 },
                    { -2, (short)10, (short)-2, -1, -3, -3 },
                    { -3, (short)12, (short)-4, -2, -3, -3 }
                });

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

            migrationBuilder.CreateIndex(
                name: "FK_ShowCard_Show",
                table: "ShowCard",
                column: "ShowID");

            migrationBuilder.CreateIndex(
                name: "IX_ShowCard_UserID",
                table: "ShowCard",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "FK_ShowCard_Watcher",
                table: "ShowCard",
                column: "WatcherID");

            migrationBuilder.CreateIndex(
                name: "FK_ShowSeason_Show",
                table: "ShowSeason",
                column: "ShowID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShowCard");

            migrationBuilder.DropTable(
                name: "ShowSeason");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Watcher");

            migrationBuilder.DropTable(
                name: "show");
        }
    }
}
