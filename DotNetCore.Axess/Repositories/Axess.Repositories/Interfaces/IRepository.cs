using DotNetCore.Axess.Domain;

namespace Axess.Repositories.Interfaces;

/// <summary>
/// Représente l'ensemble des méthodes pour gérer une entité.
/// </summary>
/// <typeparam name="TEntity">L'entité à gérer.</typeparam>
public interface IRepository<TEntity, TId> : ICommandRepository<TEntity, TId>, IQueryRepository<TEntity, TId> where TEntity : Entity<TId> { }
