using DotNetCore.Axess.Domain;
using System.Linq.Expressions;

namespace DotNetCore.Axess.Repositories.Interfaces;
/// <summary>
/// Représente l'ensemble des méthodes pour persister une entité.
/// </summary>
/// <typeparam name="TEntity">L'entité à gérer.</typeparam>
public interface ICommandRepository<TEntity, TId> where TEntity : Entity<TId>
{
    /// <summary>
    /// Ajoute une entité.
    /// </summary>
    /// <param name="entity">L'entité à ajouté.</param>
    /// /// <returns></returns>
    void Add(TEntity entity);
    Task AddAsync(TEntity entity);

    /// <summary>
    /// Ajoute plusieurs entités.
    /// </summary>
    /// <param name="entities">Les entités à ajouté.</param>
    /// /// <returns></returns>
    void AddRange(IEnumerable<TEntity> entities);
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// Supprime une entité à partir d'une clef.
    /// </summary>
    /// <param name="key">La clef</param>
    /// <returns></returns>
    void Delete(TId key);
    /// <summary>
    /// Supprime une entité à partir d'une expression.
    /// </summary>
    /// <param name="where">L'expression</param>
    /// <returns></returns>
    void Delete(Expression<Func<TEntity, bool>> where);
    /// <summary>
    /// Supprime une entité à partir de son objet.
    /// </summary>
    /// <param name="entity">L'entité à supprimer.</param>
    /// <returns></returns>
    void Delete(TEntity entity);
    /// <summary>
    /// Supprime une entité à partir d'une clef.
    /// </summary>
    /// <param name="key">La clef</param>
    /// <returns></returns>
    Task DeleteAsync(TId key);
    /// <summary>
    /// Supprime une entité à partir d'une expression.
    /// </summary>
    /// <param name="where">L'expression</param>
    /// <returns></returns>
    Task DeleteAsync(Expression<Func<TEntity, bool>> where);
    /// <summary>
    /// Supprime une entité à partir de son objet.
    /// </summary>
    /// <param name="entity">L'entité à supprimer.</param>
    /// <returns></returns>
    Task DeleteAsync(TEntity entity);

    /// <summary>
    /// Met à jour une entité.
    /// </summary>
    /// <param name="entity">L'entité à mettre à jour.</param>
    /// <returns></returns>
    void Update(TEntity entity);
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Met à jour partielle d'une entité.
    /// </summary>
    /// <param name="item">L'entité à mettre à jour.</param>
    /// <returns></returns>
    void UpdatePartial(TEntity item, object id);
    Task UpdatePartialAsync(TEntity entity, object id);
    /// <summary>
    /// Met à jourplusieurs entités.
    /// </summary>
    /// <param name="entities">L'entité à mettre à jour.</param>
    /// <returns></returns>
    void UpdateRange(IEnumerable<TEntity> entities);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities);
    /// <summary>
    /// Sauvegarde les changements.
    /// </summary>
    /// <returns></returns>
    Task<int> SaveChangesAsync();

    /// <summary>
    /// Sauvegarde les changements(acceptAllChangesOnSuccess).
    /// </summary>
    /// <returns></returns>
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess);

}
