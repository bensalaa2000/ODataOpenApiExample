using MediatR;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ODataOpenApiExample.Controllers;
/// <summary>
/// 
/// </summary>
public abstract class ApiODataControllerBase : ODataController
{
    /// <summary>
    /// 
    /// </summary>
    protected ISender Mediator => HttpContext.RequestServices.GetRequiredService<ISender>();
}
