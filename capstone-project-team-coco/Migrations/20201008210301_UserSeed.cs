using Microsoft.EntityFrameworkCore.Migrations;

namespace we_watch.Migrations
{
    public partial class UserSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserID", "Email", "HashPassword", "Salt" },
                values: new object[] { -1, "someone@somewhere.something", "2334814362998297759587574090140267323532918138392977707124924545", "wherew" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserID", "Email", "HashPassword", "Salt" },
                values: new object[] { -2, "aspin@go.com", "2334814362998297759587574090140267323532918138392977707124924545", "Yes" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserID", "Email", "HashPassword", "Salt" },
                values: new object[] { -3, "goaspin@gmail.com", "2334814362998297759587574090140267323532918138392977707124924545", "Yes" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserID",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserID",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserID",
                keyValue: -1);
        }
    }
}
