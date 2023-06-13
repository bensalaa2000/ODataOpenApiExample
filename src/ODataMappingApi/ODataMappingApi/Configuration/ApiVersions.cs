using Microsoft.AspNetCore.Mvc;

namespace ApiVersioning.Examples.Configuration;

internal static class ApiVersions
{
    internal static readonly ApiVersion V1 = new(1, 0);
    internal static readonly ApiVersion V2 = new(2, 0);
    internal static readonly ApiVersion V3 = new(3, 0);
    internal static readonly ApiVersion V4 = new(4, 0);
    internal static readonly ApiVersion V5 = new(5, 0);
}