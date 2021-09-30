using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class V4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacijent_Lekar_IzabraniLekarID",
                table: "Pacijent");

            migrationBuilder.DropTable(
                name: "Lekar");

            migrationBuilder.DropIndex(
                name: "IX_Pacijent_IzabraniLekarID",
                table: "Pacijent");

            migrationBuilder.DropColumn(
                name: "IzabraniLekarID",
                table: "Pacijent");

            migrationBuilder.AddColumn<string>(
                name: "Bolest",
                table: "Pacijent",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stanje",
                table: "Pacijent",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bolest",
                table: "Pacijent");

            migrationBuilder.DropColumn(
                name: "Stanje",
                table: "Pacijent");

            migrationBuilder.AddColumn<int>(
                name: "IzabraniLekarID",
                table: "Pacijent",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lekar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StrucnaSprema = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lekar", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pacijent_IzabraniLekarID",
                table: "Pacijent",
                column: "IzabraniLekarID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacijent_Lekar_IzabraniLekarID",
                table: "Pacijent",
                column: "IzabraniLekarID",
                principalTable: "Lekar",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
