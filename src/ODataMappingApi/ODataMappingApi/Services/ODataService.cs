using Microsoft.AspNetCore.OData.Query;

namespace ODataMappingApi.Services;

public class ODataService<T> : IODataService<T> where T : class
{

    public Task<IEnumerable<T>> GetQueryAsync(ODataQueryOptions<T> options)
    {
        throw new NotImplementedException();
    }

}
