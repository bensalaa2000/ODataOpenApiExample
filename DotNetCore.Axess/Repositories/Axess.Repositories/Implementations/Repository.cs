
using Axess.Repositories.Interfaces;
using Axess.Specification.Interfaces;
using DotNetCore.Axess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Axess.Repositories;

/// <summary>
/// Représente l'ensemble des méthodes de base pour gérer une entité.
/// </summary>
/// <typeparam name="TEntity">L'entité à gérer.</typeparam>
public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : Entity<TId>
{
    #region Membres

    private readonly ICommandRepository<TEntity, TId> _commandRepository;
    private readonly IQueryRepository<TEntity, TId> _queryRepository;

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
        _commandRepository = new CommandRepository<TEntity, TId>(context);
        _queryRepository = new QueryRepository<TEntity, TId>(context);
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #endregion
    #region Commande Implementations
    /// <inheritdoc/>
    public virtual void Add(TEntity entity) => _commandRepository.Add(entity);
    /// <inheritdoc/>
    public virtual void AddRange(IEnumerable<TEntity> entities) => _commandRepository.AddRange(entities);
    /// <inheritdoc/>
    public virtual Task AddAsync(TEntity entity) => _commandRepository.AddAsync(entity);
    /// <inheritdoc/>
    public Task AddRangeAsync(IEnumerable<TEntity> entities) => _commandRepository.AddRangeAsync(entities);
    /// <inheritdoc/>
    public virtual void Delete(TId key) => _commandRepository.Delete(key);
    /// <inheritdoc/>
    public virtual void Delete(Expression<Func<TEntity, bool>> where) => _commandRepository.Delete(where);
    /// <inheritdoc/>
    public void Delete(TEntity entity) => _commandRepository.Delete(entity);
    /// <inheritdoc/>
    public virtual Task DeleteAsync(TId key) => _commandRepository.DeleteAsync(key);
    /// <inheritdoc/>
    public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> where) => _commandRepository.DeleteAsync(where);
    /// <inheritdoc/>
    public virtual Task DeleteAsync(TEntity entity) => _commandRepository.DeleteAsync(entity);
    /// <inheritdoc/>
    public virtual void Update(TEntity entity) => _commandRepository.Update(entity);
    /// <inheritdoc/>
    public virtual void UpdatePartial(TEntity item, object id) => _commandRepository.Update(item);
    /// <inheritdoc/>
    public virtual void UpdateRange(IEnumerable<TEntity> entities) => _commandRepository.UpdateRange(entities);
    /// <inheritdoc/>
    public virtual Task UpdateAsync(TEntity entity) => _commandRepository.UpdateAsync(entity);
    /// <inheritdoc/>
    public virtual Task UpdatePartialAsync(TEntity entity, object id) => _commandRepository.UpdatePartialAsync(entity, id);
    /// <inheritdoc/>
    public virtual Task UpdateRangeAsync(IEnumerable<TEntity> entities) => _commandRepository.UpdateRangeAsync(entities);

    /// <inheritdoc/>
    public virtual Task<int> SaveChangesAsync() => _commandRepository.SaveChangesAsync();
    /// <inheritdoc/>
    public virtual Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess) => _commandRepository.SaveChangesAsync(acceptAllChangesOnSuccess);

    #endregion
    #region Query Implementations
    /// <inheritdoc/>
    public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression) => _queryRepository.Find(expression);
    public virtual IEnumerable<TEntity> Find(ISpecification<TEntity> specification) => _queryRepository.Find(specification);
    /// <inheritdoc/>
    public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression) => await _queryRepository.FindAsync(expression);
    /// <inheritdoc/>
    public virtual async Task<IEnumerable<TEntity>> FindAsync(ISpecification<TEntity> specification) => await _queryRepository.FindAsync(specification);

    /// <inheritdoc/>
    public virtual TEntity? GetById(object id) => _queryRepository.GetById(id);
    /// <inheritdoc/>
    public virtual Task<TEntity?> GetByIdAsync(object id) => _queryRepository.GetByIdAsync(id);

    /// <inheritdoc/>
    public virtual bool Any() => _queryRepository.Any();
    /// <inheritdoc/>
    public virtual bool Any(Expression<Func<TEntity, bool>> where) => _queryRepository.Any(where);
    /// <inheritdoc/>
    public virtual async Task<bool> AnyAsync() => await _queryRepository.AnyAsync();
    /// <inheritdoc/>
    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where) => await _queryRepository.AnyAsync(where);


    /// <inheritdoc/>
    public virtual long Count() => _queryRepository.Count();
    /// <inheritdoc/>
    public virtual long Count(ISpecification<TEntity> specification) => _queryRepository.Count(specification);
    /// <inheritdoc/>
    public virtual long Count(Expression<Func<TEntity, bool>> where) => _queryRepository.Count(where);
    /// <inheritdoc/>
    public virtual async Task<long> CountAsync() => await _queryRepository.CountAsync();
    /// <inheritdoc/>
    public virtual async Task<long> CountAsync(ISpecification<TEntity> specification) => await _queryRepository.CountAsync(specification);
    /// <inheritdoc/>
    public virtual async Task<long> CountAsync(Expression<Func<TEntity, bool>> where) => await _queryRepository.CountAsync(where);
    /// <inheritdoc/>
    public virtual bool Contains(ISpecification<TEntity> specification) => _queryRepository.Contains(specification);
    /// <inheritdoc/>
    public virtual bool Contains(Expression<Func<TEntity, bool>> predicate) => _queryRepository.Contains(predicate);
    /// <inheritdoc/>
    public virtual async Task<bool> ContainsAsync(ISpecification<TEntity> specification) => await _queryRepository.ContainsAsync(specification);
    /// <inheritdoc/>
    public virtual async Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> predicate) => await _queryRepository.ContainsAsync(predicate);

    #endregion
}
