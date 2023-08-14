namespace Axess.Application.Configuration;

using Axess.Application.Models;
using Axess.Common.Application.Extensions;
using Axess.Domain.Entities;
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

public static class EdmModelBuilder
{

    public static IEdmModel GetEdmModel()
    {
        ODataConventionModelBuilder edmBuilder = new();
        edmBuilder.EnableLowerCamelCase();

        edmBuilder.EntitySet<OrderDto>("OrderOData");

        /*Configuration Order Dto*/
        EntityTypeConfiguration<OrderDto> orderDto = edmBuilder.EntityType<OrderDto>();
        orderDto.HasKey(o => o.Code);
        orderDto.HasMany(x => x.LineItems);
        //orderDto.Select("code", "customer", "description", "lineItems");

        /*Configuration Order Entity*/
        EntityTypeConfiguration<Order> order = edmBuilder.EntityType<Order>();
        order.HasKey(o => o.Id);

        order.HasMany(x => x.LineItems).Expand();
        order.Select(SelectExpandType.Allowed);
        ////order.Expand();

        ///edmBuilder.EntitySet<LineItemDto>("LineItemsOData");
        /*Configuration LineItem Dto*/
        EntityTypeConfiguration<LineItemDto> lineItemDto = edmBuilder.EntityType<LineItemDto>();
        lineItemDto.HasKey(li => li.Code);
        lineItemDto.Select(SelectExpandType.Allowed);

        /*Configuration LineItem Entity*/
        EntityTypeConfiguration<LineItem> lineItem = edmBuilder.EntityType<LineItem>();
        lineItem.HasKey(li => li.Id);
        lineItem.Select(SelectExpandType.Allowed);
        lineItem.Ignore(x => x.OrderId);   // On ignore OrderId dans le resultat retourné
        ///lineItem.Expand();

        IEdmModel model = edmBuilder.GetEdmModel();

        // Only Add the ClrPropertyInfoAnnotation for the previous one, not the last one.
        IEdmEntityType orderType = model.SchemaElements.OfType<IEdmEntityType>().FirstOrDefault(e => e.Name == "Order");
        IEdmProperty idProperty = orderType.FindProperty("id");
        PropertyInfo idPropertyInfo = typeof(Order).GetProperty("Id");
        model.SetAnnotationValue(idProperty, new ClrPropertyInfoAnnotation(idPropertyInfo));

        IEdmEntityType lineItemType = model.SchemaElements.OfType<IEdmEntityType>().FirstOrDefault(e => e.Name == "LineItem");
        model.SetAnnotationValue(lineItemType.FindProperty("id"), new ClrPropertyInfoAnnotation(typeof(LineItem).GetProperty("Id")));

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
        IEdmModel model = feature.Model;
        Uri serviceRoot = new Uri(new Uri(feature.BaseAddress), feature.RoutePrefix);
        IServiceProvider requestProvider = feature.Services;
        ODataUriParser parser = new ODataUriParser(model, serviceRoot, uri, requestProvider);

        parser.Resolver ??= new UnqualifiedODataUriResolver() { EnableCaseInsensitive = true };
        parser.UrlKeyDelimiter = ODataUrlKeyDelimiter.Slash;

        ODataPath path = parser.ParsePath();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        KeySegment segment = path.OfType<KeySegment>().FirstOrDefault();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        if (segment is null)
        {
            return new Dictionary<string, object>(capacity: 0);
        }

        return new Dictionary<string, object>(segment.Keys, StringComparer.OrdinalIgnoreCase);
    }

    public static object GetRelatedKey(this ControllerBase controller, Uri uri) => controller.GetRelatedKeys(uri).Values.SingleOrDefault();
}