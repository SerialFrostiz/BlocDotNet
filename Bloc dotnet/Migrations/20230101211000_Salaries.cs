using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloc_dotnet.Migrations
{
    public partial class Salaries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    IdSalarie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fixe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Portable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceIdService = table.Column<int>(type: "int", nullable: false),
                    SiteIdSite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => x.IdSalarie);
                    table.ForeignKey(
                        name: "FK_Salaries_Services_ServiceIdService",
                        column: x => x.ServiceIdService,
                        principalTable: "Services",
                        principalColumn: "IdService",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Salaries_Sites_SiteIdSite",
                        column: x => x.SiteIdSite,
                        principalTable: "Sites",
                        principalColumn: "IdSite",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_ServiceIdService",
                table: "Salaries",
                column: "ServiceIdService");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_SiteIdSite",
                table: "Salaries",
                column: "SiteIdSite");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Salaries");
        }
    }
}
