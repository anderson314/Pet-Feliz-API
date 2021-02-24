using Microsoft.EntityFrameworkCore.Migrations;

namespace PetFelizApi.Migrations
{
    public partial class RelacaoDogWalkerSolicitacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DogWalkerId",
                table: "SolicitacaoServico",
                type: "int",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoServico_DogWalkerId",
                table: "SolicitacaoServico",
                column: "DogWalkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoServico_DogWalker_DogWalkerId",
                table: "SolicitacaoServico",
                column: "DogWalkerId",
                principalTable: "DogWalker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoServico_DogWalker_DogWalkerId",
                table: "SolicitacaoServico");

            migrationBuilder.DropIndex(
                name: "IX_SolicitacaoServico_DogWalkerId",
                table: "SolicitacaoServico");

            migrationBuilder.DropColumn(
                name: "DogWalkerId",
                table: "SolicitacaoServico");
        }
    }
}
