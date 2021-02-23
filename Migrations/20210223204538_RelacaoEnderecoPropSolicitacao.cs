using Microsoft.EntityFrameworkCore.Migrations;

namespace PetFelizApi.Migrations
{
    public partial class RelacaoEnderecoPropSolicitacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnderecoProprietarioId",
                table: "SolicitacaoServico",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoServico_EnderecoProprietarioId",
                table: "SolicitacaoServico",
                column: "EnderecoProprietarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoServico_EnderProprietario_EnderecoProprietarioId",
                table: "SolicitacaoServico",
                column: "EnderecoProprietarioId",
                principalTable: "EnderProprietario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoServico_EnderProprietario_EnderecoProprietarioId",
                table: "SolicitacaoServico");

            migrationBuilder.DropIndex(
                name: "IX_SolicitacaoServico_EnderecoProprietarioId",
                table: "SolicitacaoServico");

            migrationBuilder.DropColumn(
                name: "EnderecoProprietarioId",
                table: "SolicitacaoServico");
        }
    }
}
