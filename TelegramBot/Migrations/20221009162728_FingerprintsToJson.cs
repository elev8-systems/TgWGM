using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TelegramBot.Migrations
{
    public partial class FingerprintsToJson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HostFingerprint",
                table: "RemoteNodes");

            migrationBuilder.AddColumn<string>(
                name: "HostFingerprints",
                table: "RemoteNodes",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HostFingerprints",
                table: "RemoteNodes");

            migrationBuilder.AddColumn<string>(
                name: "HostFingerprint",
                table: "RemoteNodes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
