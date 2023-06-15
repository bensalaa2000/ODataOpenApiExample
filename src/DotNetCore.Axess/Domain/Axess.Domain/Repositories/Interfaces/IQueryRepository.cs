using Axess.Domain;
using System.Linq.Expressions;

namespace Axess.Domain.Repositories.Interfaces;

/// <summary>
/// Représente l'ensemble des méthodes pour requeter une entité.
/// </summary>
/// <typeparam name="TEntity">L'entité à gérer.</typeparam>
public interface IQueryRepository<TEntity> where TEntity : Entity
{
    /// <summary>
    /// Renvoie un <see cref="IQueryable"/> de l'entité.
    /// </summary>
    IQueryable<TEntity> Queryable { get; }

    /// <summary>
    /// Verifie l'existence d'un enregistrement sans expression.
    /// </summary>
    /// <returns>Vrai ou faux.</returns>
    Task<bool> AnyAsync();


    /// <summary>
    /// Verifie l'existence d'un enregistrement avec expression.
    /// </summary>
    /// <returns>Vrai ou faux.</returns>
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where);


    /// <summary>
    /// Compte le nombre d'entité.
    /// </summary>
    /// <returns>Le nombre d'entité.</returns>
    Task<long> CountAsync();

    /// <summary>
    /// Compte le nombre d'entité avec une expression.
    /// </summary>
    /// <returns>Le nombre d'entité.</returns>
    Task<long> CountAsync(Expression<Func<TEntity, bool>> where);

    /// <summary>
    /// Recherche une entité en fonction d'une expression lambda.
    /// </summary>
    /// <param name="expression">L'expression lambda.</param>
    /// <returns>Une liste d'entité.</returns>
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);


    /// <summary>
    /// Récupère un entité en fonction de son identifiant.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity?> GetByIdAsync(Guid id);


    /// <summary>
    ///Recherche contient à partir d'une specification.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> predicate);
}
