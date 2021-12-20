using Microsoft.EntityFrameworkCore.Migrations;

namespace HADI.Migrations
{
    public partial class AddingCheckListReportBridgeTableToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckListReportBridgeTables",
                columns: table => new
                {
                    CheckListId = table.Column<int>(type: "int", nullable: false),
                    CheckListReportId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListReportBridgeTables", x => new { x.CheckListId, x.CheckListReportId });
                    table.ForeignKey(
                        name: "FK_CheckListReportBridgeTables_CheckListReports_CheckListReportId",
                        column: x => x.CheckListReportId,
                        principalTable: "CheckListReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CheckListReportBridgeTables_Checklists_CheckListId",
                        column: x => x.CheckListId,
                        principalTable: "Checklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckListReportBridgeTables_CheckListReportId",
                table: "CheckListReportBridgeTables",
                column: "CheckListReportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckListReportBridgeTables");
        }
    }
}
