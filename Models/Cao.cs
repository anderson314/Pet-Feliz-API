using PetFelizApi.Models.Enuns;

namespace PetFelizApi.Models
{
    public class Cao
    {
        public int Id                       { get; set; }
        public IdadeCao Idade               { get; set; }
        public PorteCao  Porte              { get; set; }
        public Proprietario Proprietario    { get; set; } 
        public string Nome                  { get; set; }
        public string Raca                  { get; set; }
        public int  Peso                    { get; set; }
    }
}