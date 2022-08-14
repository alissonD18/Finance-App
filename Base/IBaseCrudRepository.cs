namespace MVCSandBox.Base
{
    public interface IBaseCrudRepository<TModel> where TModel : BaseModel
    {
        Task<TModel> GetAsync(Guid id, CancellationToken cancellationToken);
        Task InsertAsync(TModel model, CancellationToken cancellationToken);
        Task UpdateAsync(TModel model, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
