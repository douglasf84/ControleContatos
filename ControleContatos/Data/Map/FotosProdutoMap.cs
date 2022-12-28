using ControleContatos.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ControleContatos.Data.Map
{
    public class FotosProdutoMap : IEntityTypeConfiguration<FotosProdutoModel>
    {
        public void Configure(EntityTypeBuilder<FotosProdutoModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Produto);
        }
    }
}
