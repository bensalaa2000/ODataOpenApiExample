using DotNetCore.Axess.Domain;
using System.Linq.Expressions;

namespace Axess.Domain;
public static class BaseEntityExpression<TEntity, TId> where TEntity : Entity<TId>
{
    public static Expression<Func<TEntity, TId>> TKey => entity => entity.Id;

    /*public static Expression<Func<TEntity, bool>> Id(object id)
    {
        return entity => entity.Id == id;
    }*/

    public static Expression<Func<TEntity, bool>> All()
    {
        return entity => true;
    }
}

