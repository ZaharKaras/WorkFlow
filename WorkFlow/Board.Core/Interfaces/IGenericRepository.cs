namespace Board.Core.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		Task<T?> GetByIdAsync(Guid id, CancellationToken token = default);
		Task<bool> AddAsync(T entity, CancellationToken token = default);
		Task<bool> UpdateAsync(T entity, CancellationToken token = default);
		Task<bool> DeleteAsync(Guid id, CancellationToken token = default);
	}
}
