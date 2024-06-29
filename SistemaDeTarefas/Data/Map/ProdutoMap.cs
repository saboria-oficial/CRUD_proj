using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Data.Map
{
    public class ProdutoMap : IEntityTypeConfiguration<ProdutoModel>
    {
        public void Configure(EntityTypeBuilder<ProdutoModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IdProduto).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Descricao).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Restricao).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Foto).HasColumnType("varbinary(max)"); 
            builder.Property(x => x.Valor).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Restricao).IsRequired().HasMaxLength(255);



        }
    }
}
