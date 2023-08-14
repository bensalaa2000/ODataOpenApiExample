using AutoMapper;
using Entities = Axess.Domain.Entities;

namespace Axess.Application.Orders.Mappings;
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
        ////ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

        ///cfg.AllowNullCollections = true;

        /**CreateMap<CreateOrderCommand, Entities.Order>()
            .ForMember(src => src.Customer, o => o.MapFrom(dest => dest.CustomerId))
            .ConstructUsing(v => new Entities.Order(Guid.NewGuid()))
            .ForMember(a => a.LineItems, o => o.ExplicitExpansion());

        CreateMap<OrderItemDto, Entities.LineItem>()
            .ForMember(src => src.UnitPrice, o => o.MapFrom(dest => dest.Price))
            .ForMember(src => src.Quantity, o => o.MapFrom(dest => dest.Count));**/

        CreateProjection<Entities.Order, Models.OrderDto>()
           .ForMember(a => a.Code, o => o.MapFrom(x => x.Id))
           .ForMember(a => a.LineItems, o => o.ExplicitExpansion());
        CreateProjection<Entities.LineItem, Models.LineItemDto>()
           .ForMember(a => a.Code, o => o.MapFrom(x => x.Id));


        CreateMap<Entities.Order, Models.OrderDto>()
            .ForMember(a => a.Code, o => o.MapFrom(x => x.Id))
            .ForMember(a => a.LineItems, o => o.ExplicitExpansion())
        //.ForAllMembers(o => o.ExplicitExpansion())
        /*.ReverseMap()*/;

        CreateMap<Entities.LineItem, Models.LineItemDto>()
            .ForMember(a => a.Code, o => o.MapFrom(x => x.Id))
        // .ForAllMembers(o => o.ExplicitExpansion())
        /*.ReverseMap()*/;

        /**CreateProjection<Entities.Address, Models.AddressDto>()
            .ForMember(a => a.Code, o => o.MapFrom(x => x.Id));
        CreateProjection<Entities.Person, Models.PersonDto>()
            .ForMember(a => a.Code, o => o.MapFrom(x => x.Id));
        CreateProjection<Entities.Product, Models.ProductDto>()
            .ForMember(a => a.Code, o => o.MapFrom(x => x.Id));
        CreateProjection<Entities.Supplier, Models.SupplierDto>()
            .ForMember(a => a.Code, o => o.MapFrom(x => x.Id));*/


    }

}
