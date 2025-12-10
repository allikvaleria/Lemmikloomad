using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lemmikloomad.Migrations
{
    /// <inheritdoc />
    public partial class addedKliinik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kliinikud",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kliinikud", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Omanikud",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Perekonnanimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sugu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Omanikud", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lemmikloomad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kaal = table.Column<double>(type: "float", nullable: false),
                    OmanikId = table.Column<int>(type: "int", nullable: false),
                    KliinikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lemmikloomad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lemmikloomad_Kliinikud_KliinikId",
                        column: x => x.KliinikId,
                        principalTable: "Kliinikud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lemmikloomad_Omanikud_OmanikId",
                        column: x => x.OmanikId,
                        principalTable: "Omanikud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lemmikloomad_KliinikId",
                table: "Lemmikloomad",
                column: "KliinikId");

            migrationBuilder.CreateIndex(
                name: "IX_Lemmikloomad_OmanikId",
                table: "Lemmikloomad",
                column: "OmanikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lemmikloomad");

            migrationBuilder.DropTable(
                name: "Kliinikud");

            migrationBuilder.DropTable(
                name: "Omanikud");
        }
    }
}
