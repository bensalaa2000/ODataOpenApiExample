namespace Axess.Application.Configuration;

using Axess.Application.Models;
using Axess.Common.Application.Extensions;
using Axess.Domain.Entities;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Linq;

public static class EdmModelBuilder
{

	public static IEdmModel GetEdmModel()
	{
		ODataConventionModelBuilder edmBuilder = new();
		edmBuilder.EnableLowerCamelCase();

		edmBuilder.EntitySet<OrderDto>("Orders");

		/*Configuration Order Dto*/
		var orderDto = edmBuilder.EntityType<OrderDto>();
		orderDto.HasKey(o => o.Code);
		orderDto.HasMany(x => x.LineItems);
		//orderDto.Select("code", "customer", "description", "lineItems");

		/*Configuration Order Entity*/
		var order = edmBuilder.EntityType<Order>();
		order.HasKey(o => o.Id).Select();
		order.HasMany(x => x.LineItems).Expand();
		///order.Select(SelectExpandType.Allowed/*, "customer", "lineItems"*/);
		////order.Expand();

		///edmBuilder.EntitySet<LineItemDto>("LineItemsOData");
		/*Configuration LineItem Dto*/
		var lineItemDto = edmBuilder.EntityType<LineItemDto>();
		lineItemDto.HasKey(li => li.Code);
		lineItemDto.Select(SelectExpandType.Allowed);

		/*Configuration LineItem Entity*/
		var lineItem = edmBuilder.EntityType<LineItem>();
		lineItem.HasKey(li => li.Id);
		lineItem.Select(SelectExpandType.Allowed);
		lineItem.Ignore(x => x.OrderId);   // On ignore OrderId dans le resultat retourné
		///lineItem.Expand();

		var model = edmBuilder.GetEdmModel();

		// Only Add the ClrPropertyInfoAnnotation for the previous one, not the last one.
		var orderType = model.SchemaElements.OfType<IEdmEntityType>().FirstOrDefault(e => e.Name == "Order");
		var idProperty = orderType.FindProperty("id");
		var idPropertyInfo = typeof(Order).GetProperty("Id");
		model.SetAnnotationValue(idProperty, new ClrPropertyInfoAnnotation(idPropertyInfo));

		var lineItemType = model.SchemaElements.OfType<IEdmEntityType>().FirstOrDefault(e => e.Name == "LineItem");
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