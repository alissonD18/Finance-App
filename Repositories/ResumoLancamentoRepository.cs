using Microsoft.EntityFrameworkCore;
using MVCSandBox.Base;
using MVCSandBox.Context;
using MVCSandBox.Models;

namespace MVCSandBox.Repositories
{
    public class ResumoLancamentoRepository : BaseCrudRepository<ResumoLancamento>
    {
        public ResumoLancamentoRepository(MvcSandboxContext context) : base(context)
        {
        }

        public async Task<List<ResumoLancamento>> GetByAnoAsync(short ano, CancellationToken cancellationToken)
        {
            return await _query.Where(w => w.Ano == ano).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Pega todos os resumos a partir de uma data de referência.
        /// </summary>
        public async Task<List<ResumoLancamento>> GetRetotalizarAsync(int mes, int ano, CancellationToken cancellationToken)
        {
            return await _query
                .Where(w => w.Mes > mes && w.Ano >= ano)
                .OrderBy(o => o.Ano)
                .ThenBy(o => o.Mes)
                .ToListAsync(cancellationToken);
        }
    }
}
