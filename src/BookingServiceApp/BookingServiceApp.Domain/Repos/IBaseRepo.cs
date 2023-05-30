using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingServiceApp.Domain.Repositories
{
	public interface IBaseRepo<T>
		where T : class
	{
		Task<IEnumerable<T>> GetAsync();
		Task<T> GetAsync(object id);
		Task<T> GetSingleAsync(ISpecification<T> specification);
		Task<IEnumerable<T>> GetAsync(ISpecification<T> specification);

		Task<T> CreateAsync(T entity);

		Task UpdateAsync(T entity);

		Task DeleteAsync(object id);
		Task DeleteAsync(T entity);

		Task<IEnumerable<T>> Where(Expression<Func<T, bool>> where);
	}
}
