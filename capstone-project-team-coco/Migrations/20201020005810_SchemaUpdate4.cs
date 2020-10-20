using Microsoft.EntityFrameworkCore.Migrations;

namespace we_watch.Migrations
{
    public partial class SchemaUpdate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ShowCard");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ShowCard",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "ShowCard",
                keyColumn: "ShowCardID",
                keyValue: -4,
                column: "Status",
                value: "Current");

            migrationBuilder.UpdateData(
                table: "ShowCard",
                keyColumn: "ShowCardID",
                keyValue: -3,
                column: "Status",
                value: "Current");

            migrationBuilder.UpdateData(
                table: "ShowCard",
                keyColumn: "ShowCardID",
                keyValue: -2,
                column: "Status",
                value: "Current");

            migrationBuilder.UpdateData(
                table: "ShowCard",
                keyColumn: "ShowCardID",
                keyValue: -1,
                column: "Status",
                value: "Current");
        }
    }
}
