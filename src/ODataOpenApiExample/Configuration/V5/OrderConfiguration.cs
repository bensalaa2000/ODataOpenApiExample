namespace ODataOpenApiExample.Configuration.V5;

using Asp.Versioning;
using Asp.Versioning.OData;
using Axess.Entities;
using Microsoft.OData.ModelBuilder;

/// <summary>
/// Represents the model configuration for orders.
/// </summary>
public class OrderConfiguration : IModelConfiguration
{
    /// <inheritdoc />
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
    {

        EntityTypeConfiguration<Order> order = builder.EntitySet<Order>("EntityOrders").EntityType.HasKey(o => o.Id);
        EntityTypeConfiguration<LineItem> lineItem = builder.EntityType<LineItem>().HasKey(li => li.Id);
    }
}