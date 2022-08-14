using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCSandBox.Models;

namespace MVCSandBox.Context.Configurations
{
    public class CartaoConfiguration : IEntityTypeConfiguration<Cartao>
    {
        public void Configure(EntityTypeBuilder<Cartao> builder)
        {
            
        }
    }
}
