namespace Axess.Application.Configuration;

using Asp.Versioning;
using Asp.Versioning.OData;
using Axess.Application.Models;
using Microsoft.OData.ModelBuilder;

/// <summary>
/// Represents the model configuration for orders.
/// </summary>
public class OrderModelConfiguration : IModelConfiguration
{
    /// <inheritdoc />
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
    {

        EntityTypeConfiguration<OrderDto> order = builder.EntitySet<OrderDto>("Orders").EntityType.HasKey(o => o.Code);
        EntityTypeConfiguration<LineItemDto> lineItem = builder.EntityType<LineItemDto>().HasKey(li => li.Code);

        if (apiVersion < ApiVersions.V2)
        {
            order.Ignore(o => o.EffectiveDate);
            lineItem.Ignore(li => li.Fulfilled);
        }

        if (apiVersion < ApiVersions.V3)
        {
            order.Ignore(o => o.Description);
        }


        if (apiVersion == ApiVersions.V1)
        {
            order.Function("MostExpensive").ReturnsFromEntitySet<OrderDto>("Orders");
        }

        if (apiVersion >= ApiVersions.V1)
        {
            order.Collection.Function("MostExpensive").ReturnsFromEntitySet<OrderDto>("Orders");
        }

        /*if (apiVersion >= ApiVersions.V2)
        {
            order.Action("Rate").Parameter("rating");
        }*/
    }
}