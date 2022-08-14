using Microsoft.EntityFrameworkCore;
using MVCSandBox.Base;
using MVCSandBox.Context;
using MVCSandBox.Models;

namespace MVCSandBox.Repositories
{
    public class DetalheLancamentoRepository : BaseCrudRepository<DetalheLancamento>
    {

        public DetalheLancamentoRepository(MvcSandboxContext context) : base(context)
        {
        }

        public async Task<List<DetalheLancamento>> GetAsync(DateTime data, CancellationToken cancellationToken)
        {
            return await _query.Where(w => w.DataMovimento == data).ToListAsync(cancellationToken);
        }

        public async Task<List<DetalheLancamento>> GetByResumoLancamentoAsync(Guid resumoLancamentoId, CancellationToken cancellationToken)
        {
            return await _query.Where(w => w.ResumoLancamentoId == resumoLancamentoId).ToListAsync(cancellationToken);
        }

        public async Task<List<DetalheLancamento>> GetAsync(CancellationToken cancellationToken)
        {
            return await _query.ToListAsync(cancellationToken);
        }
    }
}
