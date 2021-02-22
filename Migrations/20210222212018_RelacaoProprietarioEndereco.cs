using Microsoft.EntityFrameworkCore.Migrations;

namespace PetFelizApi.Migrations
{
    public partial class RelacaoProprietarioEndereco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProprietarioId",
                table: "EnderProprietario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EnderProprietario_ProprietarioId",
                table: "EnderProprietario",
                column: "ProprietarioId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EnderProprietario_Proprietario_ProprietarioId",
                table: "EnderProprietario",
                column: "ProprietarioId",
                principalTable: "Proprietario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnderProprietario_Proprietario_ProprietarioId",
                table: "EnderProprietario");

            migrationBuilder.DropIndex(
                name: "IX_EnderProprietario_ProprietarioId",
                table: "EnderProprietario");

            migrationBuilder.DropColumn(
                name: "ProprietarioId",
                table: "EnderProprietario");
        }
    }
}
