using System.Collections.Generic;

namespace PetFelizApi.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string AnoConclusao { get; set; }
        public InformacoesServicoDogWalker InfoServDogW { get; set; }
    }
}