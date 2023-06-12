using Axess.Specification.Interfaces;
using DotNetCore.Axess.Domain;
using System.Linq.Expressions;

namespace Axess.Repositories.Interfaces;

/// <summary>
/// Représente l'ensemble des méthodes pour requeter une entité.
/// </summary>
/// <typeparam name="TEntity">L'entité à gérer.</typeparam>
public interface IQueryRepository<TEntity, TId> where TEntity : Entity<TId>
{
    /// <summary>
    /// Renvoie un <see cref="IQueryable"/> de l'entité.
    /// </summary>
    IQueryable<TEntity> Queryable { get; }

    /// <summary>
    /// Verifie l'existence d'un enregistrement sans expression.
    /// </summary>
    /// <returns>Vrai ou faux.</returns>
    bool Any();

    /// <summary>
    /// Verifie l'existence d'un enregistrement avec expression.
    /// </summary>
    /// <returns>Vrai ou faux.</returns>
    bool Any(Expression<Func<TEntity, bool>> where);

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
    long Count();
    /// <summary>
    /// Compte le nombre d'entité sur le pattern specification.
    /// </summary>
    /// <returns>Le nombre d'entité.</returns>
    long Count(ISpecification<TEntity> specification);

    /// <summary>
    /// Compte le nombre d'entité avec une expression.
    /// </summary>
    /// <returns>Le nombre d'entité.</returns>
    long Count(Expression<Func<TEntity, bool>> where);

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
    /// Compte le nombre d'entité avec une expression sur le pattern spécification.
    /// </summary>
    /// <returns>Le nombre d'entité.</returns>
    Task<long> CountAsync(ISpecification<TEntity> specification);

    /// <summary>
    /// Recherche une entité en fonction d'une expression lambda.
    /// </summary>
    /// <param name="expression">L'expression lambda.</param>
    /// <returns>Une liste d'entité.</returns>
    IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
    /// <summary>
    /// Recherche une entité en fonction d'une spécification.
    /// </summary>
    /// <param name="specification">La pecification.</param>
    /// <returns>Une liste d'entité.</returns>
    IEnumerable<TEntity> Find(ISpecification<TEntity> specification);
    /// <summary>
    /// Recherche une entité en fonction d'une expression lambda.
    /// </summary>
    /// <param name="expression">L'expression lambda.</param>
    /// <returns>Une liste d'entité.</returns>
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);
    /// <summary>
    /// Recherche une entité en fonction d'une specification.
    /// </summary>
    /// <param name="specification">La specification.</param>
    /// <returns>Une liste d'entité.</returns>
    Task<IEnumerable<TEntity>> FindAsync(ISpecification<TEntity> specification);

    /// <summary>
    /// Récupère un entité en fonction de son identifiant.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    TEntity? GetById(object id);
    /// <summary>
    /// Récupère un entité en fonction de son identifiant.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity?> GetByIdAsync(object id);
    /// <summary>
    ///Recherche contient à partir d'une specification.
    /// </summary>
    /// <param name="specification"></param>
    /// <returns></returns>
    bool Contains(ISpecification<TEntity> specification);
    /// <summary>
    ///Recherche contient à partir d'une specification.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    bool Contains(Expression<Func<TEntity, bool>> predicate);
    /// <summary>
    ///Recherche contient à partir d'une specification.
    /// </summary>
    /// <param name="specification"></param>
    /// <returns></returns>
    Task<bool> ContainsAsync(ISpecification<TEntity> specification);
    /// <summary>
    ///Recherche contient à partir d'une specification.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> predicate);
}
