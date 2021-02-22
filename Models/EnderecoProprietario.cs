using PetFelizApi.Models.Enuns;

namespace PetFelizApi.Models
{
    public class EnderecoProprietario
    {
        public int Id                       { get; set; }
        public EstadoBrasileiro Estado      { get; set; }
        public string Cidade                { get; set; }
        public string Bairro                { get; set; }
        public string Rua                   { get; set; }
        public string NmrEndereco           { get; set; }
        public Proprietario Proprietario    { get; set; }
        public int ProprietarioId           { get; set; }
    }
}