using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Data.Map
{
    public class RestauranteMap : IEntityTypeConfiguration<RestauranteModel>
    {
        public void Configure(EntityTypeBuilder<RestauranteModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Cnpj).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Intolerancia).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Senha).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Telefone).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Cep).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Imagem).HasColumnType("varbinary(max)"); 
            builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Culinaria).IsRequired().HasMaxLength(255);
        }
    }
}
