using Axess.Domain;
using DotNetCore.Axess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Axess.Common.Infrastructure.Repositories;

/// <summary>
/// Représente l'ensemble des méthodes de base pour gérer une entité.
/// </summary>
/// <typeparam name="TEntity">L'entité à gérer.</typeparam>
public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    #region Membres

    private readonly ICommandRepository<TEntity> _commandRepository;
    private readonly IQueryRepository<TEntity> _queryRepository;

    /// <summary>
    /// Une instance <see cref="ILogger"/> représente le service de log.
    /// </summary>
    protected readonly ILogger<TEntity> _logger;

    /// <inheritdoc/>
    public IQueryable<TEntity> Queryable => _queryRepository.Queryable;

    #endregion

    #region Constructeur

    /// <summary>
    /// Une instance <see cref="M:Repository"/> représente le magasin d'orgainisation.
    /// </summary>
    /// <param name="context">Le contexte de l'application.</param>
    /// <param name="logger">Le logger de l'application.</param>
    public Repository(DbContext context, ILogger<TEntity> logger)
    {
        _commandRepository = new CommandRepository<TEntity>(context);
        _queryRepository = new QueryRepository<TEntity>(context);
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #endregion
    #region Commande Implementations

    /// <inheritdoc/>
    public virtual Task AddAsync(TEntity entity) => _commandRepository.AddAsync(entity);
    /// <inheritdoc/>
    public Task AddRangeAsync(IEnumerable<TEntity> entities) => _commandRepository.AddRangeAsync(entities);

    /// <inheritdoc/>
    public virtual Task DeleteAsync(Guid key) => _commandRepository.DeleteAsync(key);
    /// <inheritdoc/>
    public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> where) => _commandRepository.DeleteAsync(where);
    /// <inheritdoc/>
    public virtual Task DeleteAsync(TEntity entity) => _commandRepository.DeleteAsync(entity);

    public virtual Task UpdateAsync(TEntity entity) => _commandRepository.UpdateAsync(entity);
    /// <inheritdoc/>
    public virtual Task UpdatePartialAsync(TEntity entity, object id) => _commandRepository.UpdatePartialAsync(entity, id);
    /// <inheritdoc/>
    public virtual Task UpdateRangeAsync(IEnumerable<TEntity> entities) => _commandRepository.UpdateRangeAsync(entities);

    /// <inheritdoc/>
    public virtual Task SaveChangesAsync() => _commandRepository.SaveChangesAsync();
    /// <inheritdoc/>
    public virtual Task SaveChangesAsync(bool acceptAllChangesOnSuccess) => _commandRepository.SaveChangesAsync(acceptAllChangesOnSuccess);

    #endregion
    #region Query Implementations

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression) => await _queryRepository.FindAsync(expression);
    /// <inheritdoc/>

    /// <inheritdoc/>
    public virtual Task<TEntity?> GetByIdAsync(Guid id) => _queryRepository.GetByIdAsync(id);


    /// <inheritdoc/>
    public virtual async Task<bool> AnyAsync() => await _queryRepository.AnyAsync();
    /// <inheritdoc/>
    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where) => await _queryRepository.AnyAsync(where);


    /// <inheritdoc/>
    public virtual async Task<long> CountAsync() => await _queryRepository.CountAsync();

    /// <inheritdoc/>
    public virtual async Task<long> CountAsync(Expression<Func<TEntity, bool>> where) => await _queryRepository.CountAsync(where);

    /// <inheritdoc/>
    public virtual async Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> predicate) => await _queryRepository.ContainsAsync(predicate);

    #endregion
}
