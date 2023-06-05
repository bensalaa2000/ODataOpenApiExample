namespace ODataOpenApiExample.Controllers.V4;

using Asp.Versioning;
using global::MediatR;
using Microsoft.AspNetCore.Mvc;


/// <summary>
/// Represents a RESTful service of orders.
/// </summary>
[ApiVersion(4.0)]

public class OrderCommandsController : ControllerBase
{

    private readonly IMediator mediator;

    public OrderCommandsController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    /*
    [HttpPost("create")]
    [MapToApiVersion(4.0)]
    public async Task<ActionResult<Order>> Create(CreateOrder request)
    {
        Order person = await mediator.Send(request);

        return person;
    }
    
     [HttpPost("update")]
     public async Task<ActionResult<Order>> Update(UpdateOrder request)
     {
         Order person = await mediator.Send(request);

         return person;
     }

     [HttpPost("delete")]
     public async Task<IActionResult> Delete(DeleteOrder request)
     {
         await mediator.Send(request);
         return Ok();
     }*/
}
