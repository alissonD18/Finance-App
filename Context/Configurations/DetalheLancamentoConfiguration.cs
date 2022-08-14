using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCSandBox.Models;

namespace MVCSandBox.Context.Configurations
{
    public class DetalheLancamentoConfiguration : IEntityTypeConfiguration<DetalheLancamento>
    {
        public void Configure(EntityTypeBuilder<DetalheLancamento> builder)
        {
            
        }
    }
}
