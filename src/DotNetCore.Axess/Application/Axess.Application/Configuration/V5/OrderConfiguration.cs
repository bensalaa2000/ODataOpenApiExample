namespace Axess.Application.Configuration.V5;

using Asp.Versioning;
using Asp.Versioning.OData;
using Axess.Domain.Entities;
using Microsoft.OData.ModelBuilder;

/// <summary>
/// Represents the model configuration for orders.
/// </summary>
public class OrderConfiguration : IModelConfiguration
{
    /// <inheritdoc />
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
    {
        builder.EntitySet<Order>("EntityOrders");
        EntityTypeConfiguration<Order> orderEntity = builder.EntityType<Order>();
        orderEntity.HasKey(o => o.Id);
        orderEntity.HasMany(x => x.LineItems);

        builder.EntitySet<LineItem>("LineItems");
        EntityTypeConfiguration<LineItem> lineItemEntity = builder.EntityType<LineItem>();
        lineItemEntity.HasKey(li => li.Id);


        //EntityTypeConfiguration<LineItem> lineItem = builder.EntityType<LineItem>().HasKey(li => li.Id);
    }
}