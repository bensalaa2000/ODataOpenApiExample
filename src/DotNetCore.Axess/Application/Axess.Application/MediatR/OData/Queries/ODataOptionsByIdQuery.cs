namespace Axess.Application.MediatR.OData.Queries;


using Microsoft.AspNetCore.OData.Query;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="Options"></param>
/// <param name="Key"></param>
public sealed record ODataOptionsByIdQuery<T>(ODataQueryOptions<T> Options, Guid Key); //: IRequest<SingleResult<T>>;