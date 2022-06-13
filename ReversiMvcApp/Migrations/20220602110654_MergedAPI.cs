using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReversiMvcApp.Migrations
{
    public partial class MergedAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Spel",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Speler1Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Speler1Naam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Speler2Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Omschrijving = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AandeBeurt = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Beurten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bord = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Uitslag",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpelID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Winnaar = table.Column<int>(type: "int", nullable: false),
                    PuntenWit = table.Column<int>(type: "int", nullable: false),
                    PuntenZwart = table.Column<int>(type: "int", nullable: false),
                    Speler1Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Speler2Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opgegeven = table.Column<bool>(type: "bit", nullable: false),
                    Opgever = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uitslag", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spel");

            migrationBuilder.DropTable(
                name: "Uitslag");
        }
    }
}
