using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Soba_Klinika_KlinikaID",
                table: "Soba");

            migrationBuilder.RenameColumn(
                name: "KlinikaID",
                table: "Soba",
                newName: "PripadaKliniciID");

            migrationBuilder.RenameIndex(
                name: "IX_Soba_KlinikaID",
                table: "Soba",
                newName: "IX_Soba_PripadaKliniciID");

            migrationBuilder.AddForeignKey(
                name: "FK_Soba_Klinika_PripadaKliniciID",
                table: "Soba",
                column: "PripadaKliniciID",
                principalTable: "Klinika",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Soba_Klinika_PripadaKliniciID",
                table: "Soba");

            migrationBuilder.RenameColumn(
                name: "PripadaKliniciID",
                table: "Soba",
                newName: "KlinikaID");

            migrationBuilder.RenameIndex(
                name: "IX_Soba_PripadaKliniciID",
                table: "Soba",
                newName: "IX_Soba_KlinikaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Soba_Klinika_KlinikaID",
                table: "Soba",
                column: "KlinikaID",
                principalTable: "Klinika",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
