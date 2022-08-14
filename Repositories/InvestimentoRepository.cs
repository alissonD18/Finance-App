using Microsoft.EntityFrameworkCore;
using MVCSandBox.Base;
using MVCSandBox.Context;
using MVCSandBox.Models;

namespace MVCSandBox.Repositories
{
    public class InvestimentoRepository : BaseCrudRepository<Investimento>
    {
        public InvestimentoRepository(MvcSandboxContext context) : base(context)
        {
        }

        public async Task<List<Investimento>> GetAsync(CancellationToken cancellationToken)
        {
            return await _query.ToListAsync(cancellationToken);
        }
    }
}
