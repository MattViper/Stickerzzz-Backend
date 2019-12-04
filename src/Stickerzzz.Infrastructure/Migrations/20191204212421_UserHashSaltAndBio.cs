using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stickerzzz.Infrastructure.Migrations
{
    public partial class UserHashSaltAndBio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Hash",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Hash",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "AspNetUsers");
        }
    }
}
