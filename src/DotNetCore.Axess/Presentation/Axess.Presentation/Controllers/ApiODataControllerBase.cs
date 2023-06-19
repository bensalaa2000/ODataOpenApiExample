using MediatR;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace Axess.Controllers;
/// <summary>
/// 
/// </summary>
public abstract class ApiODataControllerBase : ODataController
{
	private ISender _mediator = null!;
	/// <summary>
	/// 
	/// </summary>
	protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
