using Microsoft.EntityFrameworkCore;
using PetFelizApi.Models;
using PetFelizApi.Models.Enuns;

namespace PetFelizApi.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }   

        public DbSet<EnderecoDogWalker> EnderecoDogwalker { get; set; }
        public DbSet<DogWalker> DogWalker { get; set; }
        public DbSet<Proprietario> Proprietario { get; set; }
        public DbSet<EnderecoProprietario> EnderProprietario { get; set; }
        public DbSet<SolicitacaoServico> SolicitacaoServico { get; set; }
        public DbSet<Cao> Cao                               { get; set; }
        
    }
}