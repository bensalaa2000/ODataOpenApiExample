using AutoMapper;
using AutoMapper.AspNet.OData;
using AutoMapper.QueryableExtensions;
using Axess.Application.Models;
using Axess.Domain.Entities;
using Axess.Domain.Repositories.Interfaces.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Validator;
using Microsoft.OData;
using System.Net.Mime;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Axess.Api.Controllers;

[ApiVersion("1.0")]
public class OrderODataController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly IOrderQueryRepository _orderReadRepository;
    #region Constructeurs
    public OrderODataController(IOrderQueryRepository orderReadRepository, IMapper mapper)
    {
        _orderReadRepository = orderReadRepository;
        _mapper = mapper;
    }

    #endregion

    #region Actions

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<Order>), Status200OK)]
    [MapToApiVersion("1.0")]
    public IActionResult Get(ODataQueryOptions<Order> queryOptions, CancellationToken ct)
    {
        try
        {
            queryOptions.Validator.Validate(queryOptions, new ODataValidationSettings());
        }
        catch (ODataException e)
        {
            return BadRequest(e.Message);
        }
        IQueryable finalQuery = queryOptions.ApplyTo(_orderReadRepository.Queryable);
        return Ok(finalQuery);
    }
    [HttpGet("odata/OrderOData/Automapper/ProjectTo")]
    [EnableQuery]
    //[NonAction]
    public IActionResult GetWithMapping()// Do Not uses because generate an error when expanding. Prefer using 'GetWithAutoMapper' action
    {
        IQueryable<Order> orders = _orderReadRepository.Queryable.AsQueryable();
        IQueryable<OrderDto> result = orders.ProjectTo<OrderDto>(_mapper.ConfigurationProvider);
        return Ok(result);
    }
    [HttpGet("odata/OrderOData/Automapper")]
    public async Task<IActionResult> GetWithAutoMapper(ODataQueryOptions<OrderDto> options)
    {
        try
        {
            options.Validator.Validate(options, new ODataValidationSettings());
        }
        catch (ODataException e)
        {
            return BadRequest(e.Message);
        }
        ICollection<OrderDto> result = await _orderReadRepository.Queryable.GetAsync(_mapper, options, null);
        return Ok(result);
    }

    [HttpGet("odata/OrderOData/Automapper/GetQuery")]
    public async Task<IActionResult> GetQueryWithAutoMapper(ODataQueryOptions<OrderDto> options)
    {
        try
        {
            options.Validator.Validate(options, new ODataValidationSettings());
        }
        catch (ODataException e)
        {
            return BadRequest(e.Message);
        }
        IQueryable<OrderDto> reult = await _orderReadRepository.Queryable.GetQueryAsync(_mapper, options);
        return Ok(reult.ToList());
    }


    /**
         [HttpGet("odata/OrderOData/Automapper/mapping")]
        [EnableQuery]
        public async Task<IActionResult<IQueryable<OrderDto>> GetWithMapping()
        {
             var result = _orderReadRepository.Queryable.ProjectTo<OrderDto>(_mapper.ConfigurationProvider);
            return Ok(result);
    }
     */

    [HttpGet("odata/OrderOData/Automapper/{id}")]
    public async Task<IActionResult> Get(Guid id, ODataQueryOptions<OrderDto> options)
    {
        try
        {
            options.Validator.Validate(options, new ODataValidationSettings());
        }
        catch (ODataException e)
        {
            return BadRequest(e.Message);
        }

        IQueryable<Order> accountQuery = _orderReadRepository.Queryable.Where(c => c.Id == id);
        if (!accountQuery.Any())
        {
            return NotFound();
        }
        ICollection<OrderDto> result = await accountQuery.GetAsync(_mapper, options, null);

        return Ok(result);
    }

    [HttpGet("odata/OrderOData/Orders")]
    [EnableQuery]
    public IQueryable<Order> GetWithOData()
    {
        return _orderReadRepository.Queryable;
    }

    /**[HttpGet("odata/OrderOData/Orders/{id:guid}")]
    [ODataAttributeRouting]
    [EnableQuery]
    public async Task<Order?> GetByIdWithOData(Guid id)
    {
        return await _orderReadRepository.Queryable.Where(x => x.Id.Equals(id)).Get();
    }*/
    #endregion
}