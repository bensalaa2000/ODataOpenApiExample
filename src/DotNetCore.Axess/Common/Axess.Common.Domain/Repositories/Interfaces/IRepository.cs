using Axess.Common.Domain.Repositories.Interfaces;
using Axess.Domain;

namespace DotNetCore.Axess.Repositories.Interfaces;

/// <summary>
/// Représente l'ensemble des méthodes pour gérer une entité.
/// </summary>
/// <typeparam name="TEntity">L'entité à gérer.</typeparam>
public interface IRepository<TEntity> : ICommandRepository<TEntity>, IQueryRepository<TEntity> where TEntity : Entity { }
