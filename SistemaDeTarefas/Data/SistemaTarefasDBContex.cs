using Microsoft.EntityFrameworkCore; // Utilizado para trabalhar com Entity Framework Core
using SistemaDeTarefas.Data.Map; // Importa o namespace onde está definido o mapeamento das entidades
using SistemaDeTarefas.Models; // Importa o namespace onde está definido o modelo de usuário

namespace SistemaDeTarefas.Data
{
    // Classe que representa o contexto do banco de dados para o sistema de tarefas
    public class SistemaTarefasDBContex : DbContext
    {
        // Construtor que recebe as opções de configuração do DbContext
        public SistemaTarefasDBContex(DbContextOptions<SistemaTarefasDBContex> options)
            : base(options)
        {
        }

        // Define um DbSet para a entidade UsuarioModel, permitindo o acesso e manipulação dos usuários no banco de dados
        public DbSet<UsuarioModel> Usuarios { get; set; }

        // Método que sobrescreve o comportamento padrão do EF Core para configurar o modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica o mapeamento definido na classe UsuarioMap utilizando o modelBuilder
            modelBuilder.ApplyConfiguration(new UsuarioMap());

            // Chama o método base para completar a configuração do modelo
            base.OnModelCreating(modelBuilder);
        }
    }
}
