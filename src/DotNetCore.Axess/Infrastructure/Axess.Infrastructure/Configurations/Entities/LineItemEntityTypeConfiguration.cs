using Axess.Common.Infrastructure.Configurations;
using DotNetCore.Axess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetCore.Axess.Infrastructure.Persistence.Configurations.Entities;

/// <summary>
/// Configuration de l'entité 
/// </summary>
public sealed class LineItemEntityTypeConfiguration : EntityTypeConfigurationBase<LineItem>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<LineItem> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Quantity).IsRequired().HasDefaultValue(0).IsRequired();
        builder.Property(p => p.UnitPrice).HasPrecision(10, 2).HasDefaultValueSql("0.00").IsRequired();
        builder.Property(p => p.Fulfilled).HasDefaultValue(true).IsRequired();
        builder.Property(p => p.Description).HasMaxLength(200).IsRequired();

        builder.HasIndex(u => u.OrderId)/*.IsUnique()*/;

        builder.HasOne(p => p.Order).WithMany(p => p.LineItems).HasForeignKey(s => s.OrderId);
    }

}