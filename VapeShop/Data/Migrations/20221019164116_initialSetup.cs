using Microsoft.EntityFrameworkCore.Migrations;

namespace VapeShop.Data.Migrations
{
    public partial class initialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VapeItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VapeImg = table.Column<string>(nullable: false),
                    Maker = table.Column<string>(nullable: false),
                    Model = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VapeItem", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VapeItem");
        }
    }
}
