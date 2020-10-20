using Microsoft.EntityFrameworkCore.Migrations;

namespace we_watch.Migrations
{
    public partial class UpdateForTesting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ShowCard",
                keyColumn: "ShowCardID",
                keyValue: -2,
                column: "UserID",
                value: -3);

            migrationBuilder.UpdateData(
                table: "ShowCard",
                keyColumn: "ShowCardID",
                keyValue: -1,
                column: "UserID",
                value: -3);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: -3,
                columns: new[] { "HashPassword", "Salt" },
                values: new object[] { "3/H/c1lljJe2l9+DQCsr3NSSPhFyj/SZV7hA5wUQxnI=", "1859530424" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ShowCard",
                keyColumn: "ShowCardID",
                keyValue: -2,
                column: "UserID",
                value: -2);

            migrationBuilder.UpdateData(
                table: "ShowCard",
                keyColumn: "ShowCardID",
                keyValue: -1,
                column: "UserID",
                value: -2);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: -3,
                columns: new[] { "HashPassword", "Salt" },
                values: new object[] { "2334814362998297759587574090140267323532918138392977707124924545", "Yes" });
        }
    }
}
