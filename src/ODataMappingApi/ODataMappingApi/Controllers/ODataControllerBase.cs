using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
//using ODataMappingApi.Services;
using System.Net.Mime;

namespace DotNetCore.Axess.Evrp.Api.Controllers;

public abstract class ODataControllerBase<TDto> : ODataControllerBase where TDto : class
{
    #region Propriétés

    /// <summary>
    /// Une instance <see cref="ILogger"/> représente le service de log.
    /// </summary>
    //protected readonly IODataService<TDto> service;

    #endregion

    #region Constructeurs

    //protected ODataControllerBase(ILogger<ODataController> logger/*, IODataService<TDto> service*/) : base(logger)
    //{
    //this.service = service;
    //}
    #endregion

    /// <summary>
    /// Gets documents to Medtra.
    /// </summary>
    /// <param name="options">The current OData query options.</param>
    /// <param name="ct"></param>
    /// <returns>The requested results.</returns>
    /// <response code="200"></response>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    public virtual async Task<IActionResult> Get(ODataQueryOptions<TDto> options, CancellationToken ct)
    {
        return Ok(Mediator.Send(options));
        //return Ok(await service.GetQueryAsync(options));
    }
}

public abstract class ODataControllerBase : ODataController
{
    #region Propriétés
    private ISender _mediator = null!;
    /// <summary>
    /// 
    /// </summary>
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    /// <summary>
    /// Une instance <see cref="ILogger"/> représente le service de log.
    /// </summary>
    // protected readonly ILogger<ODataController> _logger;
    #endregion

    #region Constructeurs
    /* protected ODataControllerBase(ILogger<ODataController> logger)
     {
         _logger = logger;
     }*/
    #endregion
}