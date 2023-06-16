namespace Axess.Application.Configuration;

using Asp.Versioning;
using Asp.Versioning.OData;
using Axess.Application.Models;
using Microsoft.OData.ModelBuilder;

/// <summary>
/// Represents the model configuration for suppliers.
/// </summary>
public class SupplierConfiguration : IModelConfiguration
{
    /// <inheritdoc />
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
    {
        if (apiVersion < ApiVersions.V3)
        {
            return;
        }

        builder.EntitySet<SupplierDto>("Suppliers").EntityType.HasKey(p => p.Code);
        builder.Singleton<SupplierDto>("Acme");
    }
}