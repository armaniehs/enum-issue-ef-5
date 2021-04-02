using Microsoft.EntityFrameworkCore.Migrations;
using EnumIssue.PoxyEnum;

namespace EnumIssue.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListItem",
                columns: table => new
                {
                    ListTypeId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListItem", x => new { x.ListTypeId, x.Id });
                    table.ForeignKey(
                        name: "FK_ListItem_ListType_ListTypeId",
                        column: x => x.ListTypeId,
                        principalTable: "ListType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusListTypeId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    PhaseListTypeId = table.Column<int>(type: "int", nullable: false),
                    PhaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_ListType_PhaseListTypeId",
                        column: x => x.PhaseListTypeId,
                        principalTable: "ListType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Project_ListType_StatusListTypeId",
                        column: x => x.StatusListTypeId,
                        principalTable: "ListType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_PhaseListTypeId",
                table: "Project",
                column: "PhaseListTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_StatusListTypeId",
                table: "Project",
                column: "StatusListTypeId");

            migrationBuilder.HackInForeighKeys();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListItem");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "ListType");
        }
    }
}
