using Axess.Common.Domain.Repositories.Interfaces;
using Axess.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Axess.Common.Infrastructure.Repositories;
public class QueryRepository<TEntity> : IQueryRepository<TEntity> where TEntity : Entity
{
    private readonly DbContext _context;

    public QueryRepository(DbContext context) => _context = context;

    public IQueryable<TEntity> Queryable => _context.Set<TEntity>();


    public async Task<bool> AnyAsync() => await Queryable.AnyAsync();
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where) => await Queryable.AnyAsync(where);


    public async Task<long> CountAsync() => await Queryable.LongCountAsync();
    public async Task<long> CountAsync(Expression<Func<TEntity, bool>> where) => await Queryable.LongCountAsync(where);


    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression) => await Find(expression).ToListAsync(new CancellationToken());


    public async Task<TEntity?> GetByIdAsync(Guid id) => await _context.Set<TEntity>().FindAsync(id);


    public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression) => Queryable.Where(expression);

    public bool Contains(Expression<Func<TEntity, bool>> predicate) => Count(predicate) > 0;

    public async Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> predicate) => await CountAsync(predicate) > 0;

    private long Count(Expression<Func<TEntity, bool>> where) => Queryable.LongCount(where);

}
