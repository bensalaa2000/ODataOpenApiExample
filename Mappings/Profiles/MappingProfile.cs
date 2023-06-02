using AutoMapper;
using System.Reflection;
using Entities = ODataOpenApiExample.Persistence.Entities;
using Models = ApiVersioning.Examples.Models;

namespace ODataOpenApiExample.Mappings.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

        CreateProjection<Entities.Address, Models.Address>();
        CreateProjection<Entities.LineItem, Models.LineItem>();
        CreateProjection<Entities.Order, Models.Order>();
        CreateProjection<Entities.Person, Models.Person>();
        CreateProjection<Entities.Product, Models.Product>();
        CreateProjection<Entities.Supplier, Models.Supplier>();
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
