using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PetFelizApi.Models.Enuns;

namespace PetFelizApi.Models
{
    public class DogWalker
    {
        public int Id                           { get; set; }
        public TipoConta TipoConta              { get; set; }
        public EnderecoDogWalker EndDogWalker   { get; set; }
        public string Nome                      { get; set; }
        public Byte[] FotoPerfil                { get; set; } 
        public DateTime DataNascimento          { get; set; }
        public DateTime DataCadastro            { get; set; }
        public string Email                     { get; set; }
        public string Celular                   { get; set; }

        [Column(TypeName = "decimal(1, 1)")]       
        public Decimal AvaliacaoMedia           { get; set; }
        public string Sobre                     { get; set; }
        public string Preferencias              { get; set; }        
        public byte[] PasswordHash              { get; set; }
        public byte[] PasswordSalt              { get; set; }

        [NotMapped]
        public string PasswordString            { get; set; }
         
        [Column(TypeName = "decimal(4, 2)")]       
        public Decimal ValorServico             { get; set; }  
        public Boolean Disponivel               { get; set; }
        public Boolean AceitaCartao             { get; set; }   
        public Double LatitudeDW               { get; set; }       
        public Double LongitudeDW              { get; set; } 

        public List<SolicitacaoServico> Solicitacoes { get; set; }
              

    }
}