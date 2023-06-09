using Microsoft.AspNetCore.OData.Query;

namespace ODataMappingApi.Services;

public interface IODataService<T> where T : class
{
    Task<IEnumerable<T>> GetQueryAsync(ODataQueryOptions<T> options);
}

