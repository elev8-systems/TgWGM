using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TelegramBot.Migrations
{
    public partial class UpdRemoteNodeStruct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InterfaceName",
                table: "RemoteNodes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterfaceName",
                table: "RemoteNodes");
        }
    }
}
