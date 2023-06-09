namespace ODataOpenApiExample.Extensions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OData.UriParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Entities = Persistence.Entities;
using Models = ApiVersioning.Examples.Models;

internal static class ODataExtensions
{
    public static IEdmModel GetEdmModelV1()
    {
        ODataConventionModelBuilder edmBuilder = new();
        edmBuilder.EnableLowerCamelCase();


        EntityTypeConfiguration<Models.Order> order = edmBuilder.EntitySet<Models.Order>("Orders").EntityType.HasKey(o => o.Id);
        EntityTypeConfiguration<Models.LineItem> lineItem = edmBuilder.EntityType<Models.LineItem>().HasKey(li => li.Number);

        /*   EntitySetConfiguration<Address> entitesWithEnum = edmBuilder.EntitySet<Address>("Address");
           entitesWithEnum.EntityType.HasKey(o => o.Id);*/
        /***
			var functionEntitesWithEnum = entitesWithEnum.EntityType.Collection.Function("PersonSearchPerPhoneType");
			functionEntitesWithEnum.Parameter<EReponseSecteurTypeDto>("EReponseSecteurTypeDto");
			functionEntitesWithEnum.ReturnsCollectionFromEntitySet<MedtraDocumentDto>("MedtraDocument");
		***/
        return edmBuilder.GetEdmModel();
    }

    public static IEdmModel GetEdmModelV2()
    {
        ODataConventionModelBuilder edmBuilder = new();
        edmBuilder.EnableLowerCamelCase();

        edmBuilder.EntitySet<Entities.Order>("Orders");
        edmBuilder.EntityType<Entities.Order>().HasKey(o => o.Id);
        edmBuilder.EntitySet<Entities.LineItem>("LineItems");
        edmBuilder.EntityType<Entities.LineItem>().HasKey(li => li.Id);


        IEdmModel model = edmBuilder.GetEdmModel();

        // Only Add the ClrPropertyInfoAnnotation for the previous one, not the last one.
        IEdmEntityType orderType = model.SchemaElements.OfType<IEdmEntityType>().FirstOrDefault(e => e.Name == "Order");
        IEdmProperty idProperty = orderType.FindProperty("id");

        PropertyInfo idPropertyInfo = typeof(Entities.Order).GetProperty("Id");
        model.SetAnnotationValue(idProperty, new ClrPropertyInfoAnnotation(idPropertyInfo));


        /*   EntitySetConfiguration<Address> entitesWithEnum = edmBuilder.EntitySet<Address>("Address");
           entitesWithEnum.EntityType.HasKey(o => o.Id);*/
        /***
			var functionEntitesWithEnum = entitesWithEnum.EntityType.Collection.Function("PersonSearchPerPhoneType");
			functionEntitesWithEnum.Parameter<EReponseSecteurTypeDto>("EReponseSecteurTypeDto");
			functionEntitesWithEnum.ReturnsCollectionFromEntitySet<MedtraDocumentDto>("MedtraDocument");
		***/
        return model;
    }

    public static IReadOnlyDictionary<string, object> GetRelatedKeys(this ControllerBase controller, Uri uri)
    {
        // REF: https://github.com/OData/AspNetCoreOData/blob/main/src/Microsoft.AspNetCore.OData/Routing/Parser/DefaultODataPathParser.cs
        Microsoft.AspNetCore.OData.Abstracts.IODataFeature feature = controller.HttpContext.ODataFeature();
        Microsoft.OData.Edm.IEdmModel model = feature.Model;
        Uri serviceRoot = new Uri(new Uri(feature.BaseAddress), feature.RoutePrefix);
        IServiceProvider requestProvider = feature.Services;
        ODataUriParser parser = new ODataUriParser(model, serviceRoot, uri, requestProvider);

        parser.Resolver ??= new UnqualifiedODataUriResolver() { EnableCaseInsensitive = true };
        parser.UrlKeyDelimiter = ODataUrlKeyDelimiter.Slash;

        ODataPath path = parser.ParsePath();
        KeySegment segment = (KeySegment)path.OfType<KeySegment>().FirstOrDefault();

        if (segment is null)
        {
            return new Dictionary<string, object>(capacity: 0);
        }

        return new Dictionary<string, object>(segment.Keys, StringComparer.OrdinalIgnoreCase);
    }

    public static object GetRelatedKey(this ControllerBase controller, Uri uri) => controller.GetRelatedKeys(uri).Values.SingleOrDefault();
}