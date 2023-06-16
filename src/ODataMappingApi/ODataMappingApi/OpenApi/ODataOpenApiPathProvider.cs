using Microsoft.OData.Edm;
using Microsoft.OpenApi.OData;
using Microsoft.OpenApi.OData.Edm;

namespace Axess.Api.OpenApi;

internal class ODataOpenApiPathProvider : IODataPathProvider
{
    private IList<ODataPath> _paths = new List<ODataPath>();

    public bool CanFilter(IEdmElement element)
    {
        return true;
    }

    public IEnumerable<ODataPath> GetPaths(IEdmModel model, OpenApiConvertSettings settings)
    {
        return _paths;
    }

    public void Add(ODataPath path)
    {
        _paths.Add(path);
    }
}