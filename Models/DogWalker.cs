using System;
using PetFelizApi.Models.Enuns;

namespace PetFelizApi.Models
{
    public class DogWalker
    {
        public int IdDogWalker                  { get; set; }
        public TipoConta TipoConta              { get; set; }
        public EnderecoDogWalker EndDogWalker   { get; set; }
        public string Nome                      { get; set; }
        public DateTime DataNascimento          { get; set; }
        public DateTime DataCadastro            { get; set; }
        public string Email                     { get; set; }
        public int Celular                      { get; set; }
        public Decimal AvaliacaoMedia           { get; set; }
        public string Sobre                     { get; set; }
        public string Preferencias              { get; set; }        
        public byte[] PasswordHash              { get; set; }
        public byte[] PasswordSalt              { get; set; }        
        public Decimal ValorServico              { get; set; }            
    }
}