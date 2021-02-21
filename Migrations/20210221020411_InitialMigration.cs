using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PetFelizApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnderecoDogwalker",
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
                    table.PrimaryKey("PK_EnderecoDogwalker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DogWalker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoConta = table.Column<int>(type: "int", nullable: false),
                    EndDogWalkerId = table.Column<int>(type: "int", nullable: true),
                    Nome = table.Column<string>(type: "varchar(40)", nullable: false),
                    FotoPerfil = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "date", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "varchar(40)", nullable: false),
                    Celular = table.Column<string>(type: "varchar(11)", nullable: false),
                    AvaliacaoMedia = table.Column<decimal>(type: "decimal(1,1)", nullable: true),
                    Sobre = table.Column<string>(type: "varchar(600)", nullable: true),
                    Preferencias = table.Column<string>(type: "varchar(200)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ValorServico = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    Disponivel = table.Column<bool>(type: "bit", nullable: false),
                    AceitaCartao = table.Column<bool>(type: "bit", nullable: true),
                    LatitudeDW = table.Column<double>(type: "float", nullable: true),
                    LongitudeDW = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DogWalker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DogWalker_EnderecoDogwalker_EndDogWalkerId",
                        column: x => x.EndDogWalkerId,
                        principalTable: "EnderecoDogwalker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DogWalker_EndDogWalkerId",
                table: "DogWalker",
                column: "EndDogWalkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DogWalker");

            migrationBuilder.DropTable(
                name: "EnderecoDogwalker");
        }
    }
}
