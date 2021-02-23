using Microsoft.EntityFrameworkCore.Migrations;

namespace PetFelizApi.Migrations
{
    public partial class RelacaoCaoSolicitacaoServico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CaoSolicitacoes",
                columns: table => new
                {
                    SolicitacaoServicoId = table.Column<int>(type: "int", nullable: false),
                    CaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaoSolicitacoes", x => new { x.CaoId, x.SolicitacaoServicoId });
                    table.ForeignKey(
                        name: "FK_CaoSolicitacoes_Cao_CaoId",
                        column: x => x.CaoId,
                        principalTable: "Cao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaoSolicitacoes_SolicitacaoServico_SolicitacaoServicoId",
                        column: x => x.SolicitacaoServicoId,
                        principalTable: "SolicitacaoServico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaoSolicitacoes_SolicitacaoServicoId",
                table: "CaoSolicitacoes",
                column: "SolicitacaoServicoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaoSolicitacoes");
        }
    }
}
