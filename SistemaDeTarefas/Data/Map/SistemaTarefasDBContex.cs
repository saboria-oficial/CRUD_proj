using Microsoft.EntityFrameworkCore; 
using SistemaDeTarefas.Data.Map; 
using SistemaDeTarefas.Models; 

namespace SistemaDeTarefas.Data
{
    public class SistemaTarefasDBContex : DbContext
    {
        public SistemaTarefasDBContex(DbContextOptions<SistemaTarefasDBContex> options)
            : base(options)
        {
        }

        public DbSet<ProdutoModel> Produto { get; set; }

        public DbSet<RestauranteModel> Restaurante { get; set; }


        public DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new RestauranteMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
