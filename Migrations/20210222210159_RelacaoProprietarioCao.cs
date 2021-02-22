using Microsoft.EntityFrameworkCore.Migrations;

namespace PetFelizApi.Migrations
{
    public partial class RelacaoProprietarioCao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProprietarioId",
                table: "Cao",
                type: "int",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Cao_ProprietarioId",
                table: "Cao",
                column: "ProprietarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cao_Proprietario_ProprietarioId",
                table: "Cao",
                column: "ProprietarioId",
                principalTable: "Proprietario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cao_Proprietario_ProprietarioId",
                table: "Cao");

            migrationBuilder.DropIndex(
                name: "IX_Cao_ProprietarioId",
                table: "Cao");

            migrationBuilder.DropColumn(
                name: "ProprietarioId",
                table: "Cao");
        }
    }
}
