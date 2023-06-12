namespace Axess.Architecture.Configuration;

using Axess.Architecture.Models;
using Asp.Versioning;
using Asp.Versioning.OData;
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
        EntityTypeConfiguration<Person> person = builder.EntitySet<Person>("People").EntityType;
        EntityTypeConfiguration<Address> address = builder.EntityType<Address>().HasKey(a => a.Id);

        person.HasKey(p => p.Id);
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
            person.Function("MostExpensive").ReturnsFromEntitySet<Person>("People");
            person.Collection.Function("MostExpensive").ReturnsFromEntitySet<Person>("People");
        }

        if (apiVersion > ApiVersions.V1)
        {
            person.ContainsOptional(p => p.HomeAddress);
            person.Ignore(p => p.WorkAddress);

            FunctionConfiguration function = person.Collection.Function("NewHires");

            function.Parameter<DateTime>("Since");
            function.ReturnsFromEntitySet<Person>("People");
        }

        if (apiVersion > ApiVersions.V2)
        {
            person.ContainsOptional(p => p.WorkAddress);
            person.Action("Promote").Parameter<string>("title");
        }
    }
}