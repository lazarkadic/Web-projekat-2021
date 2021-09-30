using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KlinikaID",
                table: "Soba",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Klinika",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojSoba = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klinika", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Soba_KlinikaID",
                table: "Soba",
                column: "KlinikaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Soba_Klinika_KlinikaID",
                table: "Soba",
                column: "KlinikaID",
                principalTable: "Klinika",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Soba_Klinika_KlinikaID",
                table: "Soba");

            migrationBuilder.DropTable(
                name: "Klinika");

            migrationBuilder.DropIndex(
                name: "IX_Soba_KlinikaID",
                table: "Soba");

            migrationBuilder.DropColumn(
                name: "KlinikaID",
                table: "Soba");
        }
    }
}
