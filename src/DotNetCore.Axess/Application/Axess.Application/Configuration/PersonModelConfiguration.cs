namespace Axess.Application.Configuration;

using Asp.Versioning;
using Asp.Versioning.OData;
using Axess.Application.Models;
using Microsoft.OData.ModelBuilder;
using System;

/// <summary>
/// Represents the model configuration for people.
/// </summary>
public class PersonModelConfiguration : IModelConfiguration
{
    /// <inheritdoc />
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
    {
        EntityTypeConfiguration<PersonDto> person = builder.EntitySet<PersonDto>("People").EntityType;
        EntityTypeConfiguration<AddressDto> address = builder.EntityType<AddressDto>().HasKey(a => a.Code);

        person.HasKey(p => p.Code);
        person.Select().OrderBy("firstName", "lastName");

        if (apiVersion < ApiVersions.V3)
        {
            person.Ignore(p => p.Phone);
        }

        if (apiVersion <= ApiVersions.V1)
        {
            person.Ignore(p => p.HomeAddress);
            person.Ignore(p => p.WorkAddress);
            person.Ignore(p => p.Email);
        }

        if (apiVersion == ApiVersions.V1)
        {
            person.Function("MostExpensive").ReturnsFromEntitySet<PersonDto>("People");
            person.Collection.Function("MostExpensive").ReturnsFromEntitySet<PersonDto>("People");
        }

        if (apiVersion > ApiVersions.V1)
        {
            person.ContainsOptional(p => p.HomeAddress);
            person.Ignore(p => p.WorkAddress);

            FunctionConfiguration function = person.Collection.Function("NewHires");

            function.Parameter<DateTime>("Since");
            function.ReturnsFromEntitySet<PersonDto>("People");
        }

        if (apiVersion > ApiVersions.V2)
        {
            person.ContainsOptional(p => p.WorkAddress);
            person.Action("Promote").Parameter<string>("title");
        }
    }
}