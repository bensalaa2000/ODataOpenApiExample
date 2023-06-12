using Axess.Evrp.Dal.Specifications;
using Axess.Repositories.Interfaces;
using Axess.Specification.Interfaces;
using DotNetCore.Axess.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Axess.Repositories;
public class QueryRepository<TEntity, TId> : IQueryRepository<TEntity, TId> where TEntity : Entity<TId>
{
    private readonly DbContext _context;

    public QueryRepository(DbContext context) => _context = context;

    public IQueryable<TEntity> Queryable => _context.Set<TEntity>();

    public bool Any() => Queryable.Any();
    public bool Any(Expression<Func<TEntity, bool>> where) => Queryable.Any(where);

    public async Task<bool> AnyAsync() => await Queryable.AnyAsync();
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where) => await Queryable.AnyAsync(where);

    public long Count() => Queryable.LongCount();
    public long Count(Expression<Func<TEntity, bool>> where) => Queryable.LongCount(where);
    public long Count(ISpecification<TEntity> specification) => ApplySpecification(specification).Count();

    public async Task<long> CountAsync() => await Queryable.LongCountAsync();
    public async Task<long> CountAsync(Expression<Func<TEntity, bool>> where) => await Queryable.LongCountAsync(where);
    public async Task<long> CountAsync(ISpecification<TEntity> specification) => await ApplySpecification(specification).CountAsync();

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression) => await Find(expression).ToListAsync(new CancellationToken());
    public async Task<IEnumerable<TEntity>> FindAsync(ISpecification<TEntity> specification) => await ApplySpecificationAsync(specification);

    public async Task<TEntity?> GetByIdAsync(object id) => await Task.FromResult(GetById(id));
    public TEntity? GetById(object id) => _context.DetectChangesLazyLoading(false).Set<TEntity>().Find(id);

    public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression) => Queryable.Where(expression);
    public IEnumerable<TEntity> Find(ISpecification<TEntity> specification) => ApplySpecification(specification);

    public bool Contains(ISpecification<TEntity> specification) => Count(specification) > 0;
    public bool Contains(Expression<Func<TEntity, bool>> predicate) => Count(predicate) > 0;

    public async Task<bool> ContainsAsync(ISpecification<TEntity> specification) => await CountAsync(specification) > 0;
    public async Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> predicate) => await CountAsync(predicate) > 0;

    #region private method's
    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        return SpecificationEvaluator<TEntity, TId>.GetQuery(_context.Set<TEntity>().AsQueryable(), spec);
    }
    private async Task<IQueryable<TEntity>> ApplySpecificationAsync(ISpecification<TEntity> spec)
    {
        return await SpecificationEvaluator<TEntity, TId>.GetQueryAsync(_context.Set<TEntity>().AsQueryable(), spec);
    }
    #endregion
}
