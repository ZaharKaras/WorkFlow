﻿namespace Board.Core.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		Task<T?> GetByIdAsync(Guid id, CancellationToken token = default);
		Task AddAsync(T entity, CancellationToken token = default);
		Task UpdateAsync(T entity, CancellationToken token = default);
		Task DeleteAsync(Guid id, CancellationToken token = default);
	}
}
