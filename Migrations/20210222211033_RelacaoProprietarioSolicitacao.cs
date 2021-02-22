using Microsoft.EntityFrameworkCore.Migrations;

namespace PetFelizApi.Migrations
{
    public partial class RelacaoProprietarioSolicitacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProprietarioId",
                table: "SolicitacaoServico",
                type: "int",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoServico_ProprietarioId",
                table: "SolicitacaoServico",
                column: "ProprietarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoServico_Proprietario_ProprietarioId",
                table: "SolicitacaoServico",
                column: "ProprietarioId",
                principalTable: "Proprietario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoServico_Proprietario_ProprietarioId",
                table: "SolicitacaoServico");

            migrationBuilder.DropIndex(
                name: "IX_SolicitacaoServico_ProprietarioId",
                table: "SolicitacaoServico");

            migrationBuilder.DropColumn(
                name: "ProprietarioId",
                table: "SolicitacaoServico");
        }
    }
}
