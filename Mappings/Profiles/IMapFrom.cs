using AutoMapper;

namespace ODataOpenApiExample.Mappings.Profiles;

public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}

