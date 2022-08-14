using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCSandBox.Models;

namespace MVCSandBox.Context.Configurations
{
    public class ResumoLancamentoConfiguration : IEntityTypeConfiguration<ResumoLancamento>
    {
        public void Configure(EntityTypeBuilder<ResumoLancamento> builder)
        {
            
        }
    }
}
