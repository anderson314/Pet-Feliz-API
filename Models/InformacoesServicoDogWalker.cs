using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetFelizApi.Models
{
    public class InformacoesServicoDogWalker
    {
        public int Id { get; set; }
        public Usuario DogWalker { get; set; }
        public int DogWalkerId { get; set; }

        [Column (TypeName = "decimal(1, 1)")]
        public decimal AvaliacaoMedia { get; set; }
        public string Sobre { get; set; }
        public string Preferencias { get; set; }

        [Column (TypeName = "decimal(3, 2)")]
        public decimal ValorServico { get; set; }
        public Boolean AceitaCartao { get; set; }
        
    }
}