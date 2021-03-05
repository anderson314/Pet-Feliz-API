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
        
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Cao> Cao { get; set; }
        public DbSet<InformacoesServicoDogWalker> ServicoDogWalker { get; set; }
    }
}