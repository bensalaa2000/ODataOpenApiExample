
using Axess.Repositories.Interfaces;
using DotNetCore.Axess.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Axess.Repositories;
public class CommandRepository<TEntity, TId> : ICommandRepository<TEntity, TId> where TEntity : Entity<TId>
{
    private readonly DbContext _context;

    public CommandRepository(DbContext context) => _context = context;

    private DbSet<TEntity> Set => _context.Set<TEntity>();
    #region Add
    /// <inheritdoc/>
    public Task AddAsync(TEntity entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        Set.Add(entity);
        return Task.CompletedTask;
    }
    /// <inheritdoc/>
    public void Add(TEntity entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        Set.Add(entity);
    }
    /// <inheritdoc/>
    public void AddRange(IEnumerable<TEntity> entities) => Set.AddRange(entities);
    /// <inheritdoc/>
    public Task AddRangeAsync(IEnumerable<TEntity> entities) => Set.AddRangeAsync(entities);
    #endregion
    #region Delete
    /// <inheritdoc/>
    public void Delete(TId key)
    {
        TEntity? item = Set.Find(key);
        if (item is null) return;
        Set.Remove(item);
    }
    /// <inheritdoc/>
    public void Delete(TEntity entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        Set.Remove(entity);
    }
    /// <inheritdoc/>
    public void Delete(Expression<Func<TEntity, bool>> where)
    {
        IQueryable<TEntity> items = Set.Where(where);
        if (!items.Any()) return;
        Set.RemoveRange(items);
    }
    /// <inheritdoc/>
    public Task DeleteAsync(TId key) => Task.Run(() => Delete(key));
    /// <inheritdoc/>
    public Task DeleteAsync(Expression<Func<TEntity, bool>> where) => Task.Run(() => Delete(where));
    /// <inheritdoc/>
    public Task DeleteAsync(TEntity entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        Set.Remove(entity);
        return Task.CompletedTask;
    }
    #endregion
    #region Update
    /// <inheritdoc/>
    public void Update(TEntity entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        Set.Update(entity);
    }
    /// <inheritdoc/>
    public Task UpdateAsync(TEntity entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        Set.Update(entity);
        return Task.CompletedTask;
    }
    /// <inheritdoc/>
    public void UpdatePartial(TEntity item, object id)
    {
        TEntity? entity = Set.Find(id);
        if (entity is null) return;
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> entry = _context.Entry(entity);
        entry.CurrentValues.SetValues(item);
        foreach (Microsoft.EntityFrameworkCore.Metadata.INavigation navigation in entry.Metadata.GetNavigations())
        {
            if (navigation.IsOnDependent || navigation.IsCollection || !navigation.ForeignKey.IsOwnership) continue;
            System.Reflection.PropertyInfo? property = item.GetType().GetProperty(navigation.Name);
            if (property is null) continue;
            object? value = property.GetValue(item, default);
            entry.Reference(navigation.Name).TargetEntry?.CurrentValues.SetValues(value!);
        }
    }
    /// <inheritdoc/>
    public Task UpdatePartialAsync(TEntity entity, object id) => Task.Run(() => UpdatePartial(entity, id));
    /// <inheritdoc/>
    public void UpdateRange(IEnumerable<TEntity> entities) => Set.UpdateRange(entities);
    /// <inheritdoc/>
    public Task UpdateRangeAsync(IEnumerable<TEntity> entities) => Task.Run(() => UpdateRange(entities));
    #endregion
    #region UnitOfWork
    /// <inheritdoc/>
    public Task<int> SaveChangesAsync() => _context.SaveChangesAsync(new CancellationToken());
    /// <inheritdoc/>
    public Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess) => _context.SaveChangesAsync(acceptAllChangesOnSuccess, new CancellationToken());
    #endregion
}
