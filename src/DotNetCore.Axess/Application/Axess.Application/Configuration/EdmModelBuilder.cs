namespace Axess.Application.Configuration;

using Axess.Application.Models;
using Axess.Common.Application.Extensions;
using Axess.Domain.Entities;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
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
        order.HasKey(o => o.Id).Select();
        order.HasMany(x => x.LineItems).Expand();
        ///order.Select(SelectExpandType.Allowed/*, "customer", "lineItems"*/);
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
}