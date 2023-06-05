using MediatR;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ODataOpenApiExample.Controllers;

public abstract class ApiODataControllerBase : ODataController
{
    protected ISender Mediator => HttpContext.RequestServices.GetRequiredService<ISender>();
}
