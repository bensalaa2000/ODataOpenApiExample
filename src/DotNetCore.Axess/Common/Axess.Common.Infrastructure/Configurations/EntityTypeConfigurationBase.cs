using Axess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Axess.Common.Infrastructure.Configurations;

/// <inheritdoc/>
public abstract class EntityTypeConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
{
	/// <inheritdoc/>
	public virtual void Configure(EntityTypeBuilder<TEntity> builder)
	{
		builder.HasKey(v => v.Id);
		builder.Property(p => p.Id).IsRequired()/*.ValueGeneratedOnAdd()*/;
	}
}