using AutoMapper;

namespace Axess.Common.Application.Mappings;
/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IMapFrom<T>
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="profile"></param>
	void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}

