using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace dotnet_ids.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    Category = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    Destination = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Cost = table.Column<float>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Guides",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Password = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    JoiningDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Photo = table.Column<string>(type: "longtext", nullable: false),
                    Profession = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guides", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Guides_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "ID");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Gender = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    JoiningDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MobileNumber = table.Column<string>(type: "longtext", nullable: false),
                    EmergencyNumber = table.Column<string>(type: "longtext", nullable: false),
                    Photo = table.Column<string>(type: "longtext", nullable: false),
                    Profession = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Nationality = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Members_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "ID");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Guides_EventID",
                table: "Guides",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Members_EventID",
                table: "Members",
                column: "EventID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Guides");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
