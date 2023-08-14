using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.OData.Edm;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.OData;
using Microsoft.OpenApi.OData.Edm;

namespace Axess.Api.OpenApi;

internal static class OpenApiDocumentExtensions
{
    public static OpenApiDocument CreateDocument(HttpContext context, string prefixName)
    {
        IDictionary<string, ODataPath> tempateToPathDict = new Dictionary<string, ODataPath>();
        ODataOpenApiPathProvider provider = new ODataOpenApiPathProvider();
        IEdmModel model = null;
        EndpointDataSource dataSource = context.RequestServices.GetRequiredService<EndpointDataSource>();
        foreach (Endpoint endpoint in dataSource.Endpoints)
        {
            IODataRoutingMetadata metadata = endpoint.Metadata.GetMetadata<IODataRoutingMetadata>();
            if (metadata == null)
            {
                continue;
            }

            if (metadata.Prefix != prefixName)
            {
                continue;
            }
            model = metadata.Model;

            RouteEndpoint routeEndpoint = endpoint as RouteEndpoint;
            if (routeEndpoint == null)
            {
                continue;
            }

            // get rid of the prefix
            int length = prefixName.Length;
            string routePathTemplate = routeEndpoint.RoutePattern.RawText.Substring(length);
            routePathTemplate = routePathTemplate.StartsWith("/") ? routePathTemplate : "/" + routePathTemplate;

            if (tempateToPathDict.TryGetValue(routePathTemplate, out ODataPath pathValue))
            {
                IEnumerable<string> methods = GetHttpMethods(endpoint);
                foreach (string method in methods)
                {
                    pathValue.HttpMethods.Add(method);
                }

                continue;
            }

            var path = metadata.Template.Translate();
            if (path == null)
            {
                continue;
            }

            path.PathTemplate = routePathTemplate;
            provider.Add(path);

            IEnumerable<string> method1 = GetHttpMethods(endpoint);
            foreach (string method in method1)
            {
                path.HttpMethods.Add(method);
            }
            tempateToPathDict[routePathTemplate] = path;
        }

        OpenApiConvertSettings settings = new OpenApiConvertSettings
        {
            PathProvider = provider,
            ServiceRoot = BuildAbsolute(context, prefixName)
        };

        return model.ConvertToOpenApi(settings);
    }

    private static IEnumerable<string> GetHttpMethods(Endpoint endpoint)
    {
        HttpMethodMetadata methodMetadata = endpoint.Metadata.GetMetadata<HttpMethodMetadata>();
        if (methodMetadata != null)
        {
            return methodMetadata.HttpMethods;
        }

        throw new Exception();
    }

    internal static Uri BuildAbsolute(HttpContext context, string prefix)
    {
        HttpRequest request = context.Request;

        return new Uri(UriHelper.BuildAbsolute(request.Scheme, request.Host, request.PathBase) + prefix);
    }
}