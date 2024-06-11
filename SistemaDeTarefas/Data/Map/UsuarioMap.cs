using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Data.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<UsuarioModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioModel> builder)
        {
            builder.HasKey(x => x.ID_Cliente);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Telefone).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Restricao).IsRequired().HasMaxLength(255);
            builder.Property(x => x.CEP).IsRequired().HasMaxLength(255);
            builder.Property(x => x.SENHA).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Tipo).IsRequired().HasMaxLength(255);

        }
    }
}
