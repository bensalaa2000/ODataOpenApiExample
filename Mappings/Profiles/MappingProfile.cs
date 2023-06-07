using AutoMapper;
using System.Reflection;
using Entities = ODataOpenApiExample.Persistence.Entities;
using Models = ApiVersioning.Examples.Models;

namespace ODataOpenApiExample.Mappings.Profiles;
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

        CreateProjection<Entities.Address, Models.Address>();
        CreateProjection<Entities.LineItem, Models.LineItem>()
            .ForMember(a => a.Number, o => o.MapFrom(x => x.Id));
        CreateProjection<Entities.Order, Models.Order>()
            .ForMember(a => a.LineItems, o => o.ExplicitExpansion());
        CreateProjection<Entities.Person, Models.Person>();
        CreateProjection<Entities.Product, Models.Product>();
        CreateProjection<Entities.Supplier, Models.Supplier>();

        CreateMap<Entities.Order, Models.Order>()
             .ForMember(a => a.LineItems, o => o.ExplicitExpansion())
             .ReverseMap();
        CreateMap<Entities.LineItem, Models.LineItem>()
            .ForMember(a => a.Number, o => o.MapFrom(x => x.Id))
            .ReverseMap();
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        Type mapFromType = typeof(IMapFrom<>);

        string mappingMethodName = nameof(IMapFrom<object>.Mapping);

        bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

        List<Type> types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

        Type[] argumentTypes = new Type[] { typeof(Profile) };

        foreach (Type type in types)
        {
            object instance = Activator.CreateInstance(type);

            MethodInfo methodInfo = type.GetMethod(mappingMethodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, new object[] { this });
            }
            else
            {
                List<Type> interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                if (interfaces.Count > 0)
                {
                    foreach (Type @interface in interfaces)
                    {
                        MethodInfo interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                        interfaceMethodInfo?.Invoke(instance, new object[] { this });
                    }
                }
            }
        }
    }
}
