using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PetFelizApi.Models.Enuns;

namespace PetFelizApi.Models
{
    public class Servico
    {
        public int Id { get; set; }
        public EstadoSolicitacao Estado { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime HoraSolicitacao { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraTermino { get; set; }

        [Column (TypeName = "decimal(5,2)")]
        public Decimal ValorTotal { get; set; }
        public List<UsuariosServico> Usuarios { get; set; }
        public List<CaoServico> Caes { get; set; }
        public Double LatitudeProp { get; set; }
        public Double LongitudeProp { get; set; }
    }
}