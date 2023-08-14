using AutoMapper;
using Axess.Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Net.Mime;

namespace Axess.Domain.Evrp.Api.Controllers;

public abstract class ODataControllerBase<TDto> : ODataControllerBase where TDto : class
{
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    public virtual async Task<IActionResult> Get(ODataQueryOptions<TDto> options, CancellationToken ct)
    {
        return Ok(await Mediator.Send(options));
    }
}

public abstract class ODataControllerBase : ODataController
{
    #region Propriétés
    private ISender _mediator = null!;

    private IMapper _mapper = null!;

    private IApplicationDbContext _dbContext = null!;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();

    protected IApplicationDbContext ApplicationDbContext => _dbContext ??= HttpContext.RequestServices.GetRequiredService<IApplicationDbContext>();
    #endregion
    #region Constructeurs
    /** protected ODataControllerBase(ILogger<ODataController> logger)
     {
         _logger = logger;
     }*/
    #endregion
}