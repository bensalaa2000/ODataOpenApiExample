using AutoMapper;
using Axess.Application.Models;
using Axess.Application.Orders.Commands.CreateOrder;
using Axess.Common.Application.Mappings;
using Axess.Domain.Entities;
using System.Reflection;

namespace Axess.Mappings.Profiles;
/// <summary>
/// 
/// </summary>
public class MappingProfile : Profile
{
	/// <summary>
	/// 
	/// </summary>
	public MappingProfile()
	{
		ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

		CreateMap<CreateOrderCommand, Order>()
		   .ForMember(src => src.Customer, o => o.MapFrom(dest => dest.CustomerId))
		   .ConstructUsing(v => new Order(Guid.NewGuid()))
		   .ForMember(a => a.LineItems, o => o.ExplicitExpansion());

		CreateMap<OrderItemDto, LineItem>()
			.ForMember(src => src.UnitPrice, o => o.MapFrom(dest => dest.Price))
			.ForMember(src => src.Quantity, o => o.MapFrom(dest => dest.Count));

		CreateProjection<Address, AddressDto>()
			.ForMember(a => a.Code, o => o.MapFrom(x => x.Id));

		CreateProjection<LineItem, LineItemDto>()
			.ForMember(a => a.Code, o => o.MapFrom(x => x.Id));
		CreateProjection<Order, OrderDto>()
			.ForMember(a => a.Code, o => o.MapFrom(x => x.Id))
			/*.ForMember(a => a.LineItems, o => o.ExplicitExpansion())*/;

		CreateProjection<Person, PersonDto>()
			.ForMember(a => a.Code, o => o.MapFrom(x => x.Id));
		CreateProjection<Product, ProductDto>()
			.ForMember(a => a.Code, o => o.MapFrom(x => x.Id));
		CreateProjection<Supplier, SupplierDto>()
			.ForMember(a => a.Code, o => o.MapFrom(x => x.Id));

		CreateMap<Order, OrderDto>()
			.ForMember(a => a.Code, o => o.MapFrom(x => x.Id))
			 /*.ForMember(a => a.LineItems, o => o.ExplicitExpansion())*/
			 .ReverseMap();

		CreateMap<LineItem, LineItemDto>()
			.ForMember(a => a.Code, o => o.MapFrom(x => x.Id))
			.ReverseMap();
	}

	private void ApplyMappingsFromAssembly(Assembly assembly)
	{
		var mapFromType = typeof(IMapFrom<>);

		var mappingMethodName = nameof(IMapFrom<object>.Mapping);

		bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

		var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().ToList().Exists(HasInterface)).ToList();

		var argumentTypes = new Type[] { typeof(Profile) };

		foreach (var type in types)
		{
			var instance = Activator.CreateInstance(type);

			var methodInfo = type.GetMethod(mappingMethodName);

			if (methodInfo != null)
			{
				methodInfo.Invoke(instance, new object[] { this });
			}
			else
			{
				var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

				if (interfaces.Count > 0)
				{
					foreach (var @interface in interfaces)
					{
						var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

						interfaceMethodInfo?.Invoke(instance, new object[] { this });
					}
				}
			}
		}
	}
}
