using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PetFelizApi.Migrations
{
    public partial class CriarTabelaEnderecoPropESolicitacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnderProprietario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(40)", nullable: false),
                    Bairro = table.Column<string>(type: "varchar(40)", nullable: false),
                    Rua = table.Column<string>(type: "varchar(40)", nullable: false),
                    NmrEndereco = table.Column<string>(type: "varchar(4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnderProprietario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolicitacaoServico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    DataSolicitacao = table.Column<DateTime>(type: "date", nullable: false),
                    HoraSolicitacao = table.Column<DateTime>(type: "time", nullable: false),
                    HoraInicio = table.Column<DateTime>(type: "time", nullable: false),
                    HoraTermino = table.Column<DateTime>(type: "time", nullable: false),
                    VlTotal = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    LatitudeProp = table.Column<double>(type: "float", nullable: false),
                    LongitudeProp = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacaoServico", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnderProprietario");

            migrationBuilder.DropTable(
                name: "SolicitacaoServico");
        }
    }
}
