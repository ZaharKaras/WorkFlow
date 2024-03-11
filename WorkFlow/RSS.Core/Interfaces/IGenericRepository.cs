namespace RSS.Core.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		Task<T?> GetByIdAsync(Guid id);
		Task AddAsync(T entity, CancellationToken token = default);
		Task DeleteAsync(Guid id);
		Task SaveChangesAsync(CancellationToken token = default);
	}
}
