using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TelegramBot.Migrations
{
    public partial class HostKeyToFingerprint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HostKey",
                table: "RemoteNodes",
                newName: "HostFingerprint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HostFingerprint",
                table: "RemoteNodes",
                newName: "HostKey");
        }
    }
}
