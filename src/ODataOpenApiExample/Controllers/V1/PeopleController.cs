namespace Axess.Controllers.V1;

using Asp.Versioning;
using Axess.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Mime;
using System.Threading;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static Microsoft.AspNetCore.OData.Query.AllowedQueryOptions;

/// <summary>
/// Represents a RESTful people service.
/// </summary>
[ApiVersion(1.0)]
[ApiVersion(0.9, Deprecated = true)]
public class PeopleController : ApiODataControllerBase
{
	/// <summary>
	/// Gets a single person.
	/// </summary>
	/// <param name="key">The requested person identifier.</param>
	/// <param name="options">The current OData query options.</param>
	/// <returns>The requested person.</returns>
	/// <response code="200">The person was successfully retrieved.</response>
	/// <response code="404">The person does not exist.</response>
	[HttpGet]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(typeof(PersonDto), Status200OK)]
	[ProducesResponseType(Status404NotFound)]
	public IActionResult Get(Guid key, ODataQueryOptions<PersonDto> options)
	{
		var people = new PersonDto[]
		{
			new()
			{
				Code = key,
				FirstName = "John",
				LastName = "Doe",
			},
		};

		object person = options.ApplyTo(people.AsQueryable()).SingleOrDefault();

		if (person == null)
		{
			return NotFound();
		}

		return Ok(person);
	}

	/// <summary>
	/// Gets the most expensive person.
	/// </summary>
	/// <returns>The most expensive person.</returns>
	/// <response code="200">The person was successfully retrieved.</response>
	/// <response code="404">No people exist.</response>
	[HttpGet]
	[MapToApiVersion(1.0)]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(typeof(PersonDto), Status200OK)]
	[ProducesResponseType(Status404NotFound)]
	[EnableQuery(AllowedQueryOptions = Select)]
	public SingleResult<PersonDto> MostExpensive(ODataQueryOptions<PersonDto> options, CancellationToken ct) =>
			SingleResult.Create(
				new PersonDto[]
				{
				new()
				{
					Code = Guid.NewGuid(),
					FirstName = "Elon",
					LastName = "Musk",
				},
				}.AsQueryable());

	/// <summary>
	/// Gets the most expensive person.
	/// </summary>
	/// <returns>The most expensive person.</returns>
	/// <response code="200">The person was successfully retrieved.</response>
	/// <response code="404">The person does not exist.</response>
	[HttpGet]
	[MapToApiVersion(1.0)]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(typeof(OrderDto), Status200OK)]
	[ProducesResponseType(Status404NotFound)]
	[EnableQuery(AllowedQueryOptions = Select)]
	public SingleResult<PersonDto> MostExpensive(
		int key,
		ODataQueryOptions<PersonDto> options,
		CancellationToken ct) =>
		SingleResult.Create(
			new PersonDto[]
			{
				new()
				{
					Code = Guid.NewGuid(),
					FirstName = "John",
					LastName = "Doe",
				},
			}.AsQueryable());
}