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
    }
}