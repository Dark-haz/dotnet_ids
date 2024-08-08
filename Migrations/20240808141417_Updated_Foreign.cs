using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_ids.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Foreign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guides_Events_EventID",
                table: "Guides");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Events_EventID",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_EventID",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Guides_EventID",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "EventID",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "EventID",
                table: "Guides");

            migrationBuilder.CreateTable(
                name: "EventGuide",
                columns: table => new
                {
                    EventsID = table.Column<int>(type: "int", nullable: false),
                    GuidesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventGuide", x => new { x.EventsID, x.GuidesID });
                    table.ForeignKey(
                        name: "FK_EventGuide_Events_EventsID",
                        column: x => x.EventsID,
                        principalTable: "Events",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventGuide_Guides_GuidesID",
                        column: x => x.GuidesID,
                        principalTable: "Guides",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EventMember",
                columns: table => new
                {
                    EventsID = table.Column<int>(type: "int", nullable: false),
                    MembersID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventMember", x => new { x.EventsID, x.MembersID });
                    table.ForeignKey(
                        name: "FK_EventMember_Events_EventsID",
                        column: x => x.EventsID,
                        principalTable: "Events",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventMember_Members_MembersID",
                        column: x => x.MembersID,
                        principalTable: "Members",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EventGuide_GuidesID",
                table: "EventGuide",
                column: "GuidesID");

            migrationBuilder.CreateIndex(
                name: "IX_EventMember_MembersID",
                table: "EventMember",
                column: "MembersID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventGuide");

            migrationBuilder.DropTable(
                name: "EventMember");

            migrationBuilder.AddColumn<int>(
                name: "EventID",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventID",
                table: "Guides",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_EventID",
                table: "Members",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Guides_EventID",
                table: "Guides",
                column: "EventID");

            migrationBuilder.AddForeignKey(
                name: "FK_Guides_Events_EventID",
                table: "Guides",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Events_EventID",
                table: "Members",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "ID");
        }
    }
}
