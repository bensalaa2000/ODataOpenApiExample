﻿using Axess.Common.Infrastructure.Configurations;
using Axess.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Axess.Infrastructure.Configurations.Entities;

/// <summary>
/// Configuration de l'entité 
/// </summary>
public sealed class OrderConfiguration : EntityTypeConfigurationBase<Order>
{
    /// <inheritdoc/>
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Customer).HasMaxLength(20).IsRequired();
        builder.Property(p => p.CreatedDate).HasColumnType("timestamp with time zone").HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        builder.Property(p => p.EffectiveDate).HasColumnType("timestamp with time zone");
        builder.Property(p => p.Description).HasMaxLength(200);

        builder.HasIndex(u => u.Customer).IsUnique();

        builder.HasMany(p => p.LineItems)
            .WithOne(p => p.Order)
            .HasForeignKey(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

