using Axess.Domain;
using System.Linq.Expressions;

namespace Axess.Common.Domain.Repositories;
/// <summary>
/// Représente l'ensemble des méthodes pour persister une entité.
/// </summary>
/// <typeparam name="TEntity">L'entité à gérer.</typeparam>
public interface ICommandRepository<TEntity> where TEntity : Entity
{

	Task AddAsync(TEntity entity);

	Task AddRangeAsync(IEnumerable<TEntity> entities);

	Task DeleteAsync(Guid key);

	Task DeleteAsync(Expression<Func<TEntity, bool>> where);

	Task DeleteAsync(TEntity entity);

	Task UpdateAsync(TEntity entity);

	Task UpdatePartialAsync(TEntity entity, object id);

	Task UpdateRangeAsync(IEnumerable<TEntity> entities);

	Task SaveChangesAsync(CancellationToken cancellationToken = default);

}
