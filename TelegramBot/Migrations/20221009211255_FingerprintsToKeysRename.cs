using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TelegramBot.Migrations
{
    public partial class FingerprintsToKeysRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HostFingerprints",
                table: "RemoteNodes",
                newName: "HostKeys");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HostKeys",
                table: "RemoteNodes",
                newName: "HostFingerprints");
        }
    }
}
