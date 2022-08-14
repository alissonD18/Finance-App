using Microsoft.EntityFrameworkCore;
using MVCSandBox.Base;
using MVCSandBox.Context;
using MVCSandBox.Models;

namespace MVCSandBox.Repositories
{
    public class CartaoRepository : BaseCrudRepository<Cartao>
    {
        public CartaoRepository(MvcSandboxContext context) : base(context)
        {
        }

        public async Task<List<Cartao>> GetAsync(CancellationToken cancellationToken)
        {
            return await _query.ToListAsync(cancellationToken);
        }
    }
}
