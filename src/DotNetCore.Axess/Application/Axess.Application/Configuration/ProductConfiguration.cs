namespace Axess.Application.Configuration;

using Asp.Versioning;
using Asp.Versioning.OData;
using Axess.Application.Models;
using Microsoft.OData.ModelBuilder;

/// <summary>
/// Represents the model configuration for products.
/// </summary>
public class ProductConfiguration : IModelConfiguration
{
    /// <inheritdoc />
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
    {
        if (apiVersion < ApiVersions.V3)
        {
            return;
        }

        EntityTypeConfiguration<ProductDto> product = builder.EntitySet<ProductDto>("Products").EntityType.HasKey(p => p.Code);
    }
}