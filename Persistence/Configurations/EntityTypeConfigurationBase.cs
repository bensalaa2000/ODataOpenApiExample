using DotNetCore.Axess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ODataOpenApiExample.Persistence.Configurations;

/// <inheritdoc/>
public abstract class EntityTypeConfigurationBase<TEntity, TId> : IEntityTypeConfiguration<TEntity> where TEntity : Entity<TId>
{
    /// <inheritdoc/>
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(p => p.Id).IsRequired();
    }
}