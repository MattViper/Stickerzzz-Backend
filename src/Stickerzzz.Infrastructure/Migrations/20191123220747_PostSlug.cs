using Microsoft.EntityFrameworkCore.Migrations;

namespace Stickerzzz.Infrastructure.Migrations
{
    public partial class PostSlug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagStickers_Tag_TagId",
                table: "TagStickers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_TagStickers_Tags_TagId",
                table: "TagStickers",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagStickers_Tags_TagId",
                table: "TagStickers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Posts");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_TagStickers_Tag_TagId",
                table: "TagStickers",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
