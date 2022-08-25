using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityDataLibrary.Migrations
{
    public partial class initailmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "Varchar(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "Varchar(50)", maxLength: 50, nullable: false),
                    AffiliatedCollege = table.Column<string>(type: "Varchar(50)", maxLength: 50, nullable: false),
                    EstablishedYear = table.Column<int>(type: "int", maxLength: 9999, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Universities_Name",
                table: "Universities",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Universities");
        }
    }
}
