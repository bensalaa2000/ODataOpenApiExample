﻿namespace ApiVersioning.Examples.Configuration;

using ApiVersioning.Examples.Models;
using Asp.Versioning;
using Asp.Versioning.OData;
using Microsoft.OData.ModelBuilder;

/// <summary>
/// Represents the model configuration for products.
/// </summary>
public class ProductConfiguration : IModelConfiguration
{
    /// <inheritdoc />
    public void Apply( ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix )
    {
        if ( apiVersion < ApiVersions.V3 )
        {
            return;
        }

        var product = builder.EntitySet<Product>( "Products" ).EntityType.HasKey( p => p.Id );
    }
}