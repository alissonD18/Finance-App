using Microsoft.EntityFrameworkCore;
using MVCSandBox.Context;

namespace MVCSandBox.Base
{
    public class BaseCrudRepository<TModel> : IBaseCrudRepository<TModel> where TModel : BaseModel
    {

        public BaseCrudRepository(MvcSandboxContext context)
        {
            _context = context;
            _query = _context.Set<TModel>();
        }

        protected readonly MvcSandboxContext _context;
        protected readonly DbSet<TModel> _query;

        public async Task InsertAsync(TModel model, CancellationToken cancellationToken)
        {
            await _context.AddAsync(model, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<TModel> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _query.Where(w => w.Id == id).SingleAsync(cancellationToken);
        }

        public async Task UpdateAsync(TModel model, CancellationToken cancellationToken)
        {
            _context.Update(model);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(List<TModel> models, CancellationToken cancellationToken)
        {
            foreach (var model in models)
            {
                _context.Update(model);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _query.Where(w => w.Id == id).SingleAsync(cancellationToken);
            _context.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
