namespace Axess.Controllers.V3;

using ApiVersioning.Examples.Models;
using Asp.Versioning;
using Asp.Versioning.OData;
using Axess.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Validator;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Mime;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static Microsoft.AspNetCore.OData.Query.AllowedQueryOptions;

/// <summary>
/// Represents a RESTful people service.
/// </summary>
[ApiVersion(3.0)]
public class PeopleController : ODataController
{
    /// <summary>
    /// Gets all people.
    /// </summary>
    /// <param name="options">The current OData query options.</param>
    /// <returns>All available people.</returns>
    /// <response code="200">The successfully retrieved people.</response>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ODataValue<IEnumerable<PersonDto>>), Status200OK)]
    public IActionResult Get(ODataQueryOptions<PersonDto> options)
    {
        ODataValidationSettings validationSettings = new ODataValidationSettings()
        {
            AllowedQueryOptions = Select | OrderBy | Top | Skip | Count,
            AllowedOrderByProperties = { "firstName", "lastName" },
            AllowedArithmeticOperators = AllowedArithmeticOperators.None,
            AllowedFunctions = AllowedFunctions.None,
            AllowedLogicalOperators = AllowedLogicalOperators.None,
            MaxOrderByNodeCount = 2,
            MaxTop = 100,
        };

        try
        {
            options.Validate(validationSettings);
        }
        catch (ODataException)
        {
            return BadRequest();
        }

        PersonDto[] people = new PersonDto[]
        {
            new()
            {
                Code = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@somewhere.com",
                Phone = "555-987-1234",
            },
            new()
            {
                Code = Guid.NewGuid(),
                FirstName = "Bob",
                LastName = "Smith",
                Email = "bob.smith@somewhere.com",
                Phone = "555-654-4321",
            },
            new()
            {
                Code = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@somewhere.com",
                Phone = "555-789-3456",
            },
        };

        return Ok(options.ApplyTo(people.AsQueryable()));
    }

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
        PersonDto[] people = new PersonDto[]
        {
            new()
            {
                Code = key,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@somewhere.com",
                Phone = "555-987-1234",
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
    /// Creates a new person.
    /// </summary>
    /// <param name="person">The person to create.</param>
    /// <returns>The created person.</returns>
    /// <response code="201">The person was successfully created.</response>
    /// <response code="400">The person was invalid.</response>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PersonDto), Status201Created)]
    [ProducesResponseType(Status400BadRequest)]
    public IActionResult Post([FromBody] PersonDto person)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        person.Code = Guid.NewGuid();

        return Created(person);
    }

    /// <summary>
    /// Gets the new hires since the specified date.
    /// </summary>
    /// <param name="since">The date and time since people were hired.</param>
    /// <param name="options">The current OData query options.</param>
    /// <returns>The matching new hires.</returns>
    /// <response code="200">The people were successfully retrieved.</response>
    [HttpGet("api/People/NewHires(Since={since})")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ODataValue<IEnumerable<PersonDto>>), Status200OK)]
    public IActionResult NewHires(DateTime since, ODataQueryOptions<PersonDto> options) => Get(options);

    /// <summary>
    /// Promotes a person.
    /// </summary>
    /// <param name="key">The identifier of the person to promote.</param>
    /// <param name="parameters">The action parameters.</param>
    /// <returns>None</returns>
    /// <response code="204">The person was successfully promoted.</response>
    /// <response code="400">The parameters are invalid.</response>
    /// <response code="404">The person does not exist.</response>
    [HttpPost]
    [ProducesResponseType(Status204NoContent)]
    [ProducesResponseType(Status400BadRequest)]
    [ProducesResponseType(Status404NotFound)]
    public IActionResult Promote(int key, [FromBody] ODataActionParameters parameters)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        string title = (string)parameters["title"];
        return NoContent();
    }

    /// <summary>
    /// Gets the home address of a person.
    /// </summary>
    /// <param name="key">The person identifier.</param>
    /// <returns>The person's home address.</returns>
    /// <response code="200">The home address was successfully retrieved.</response>
    /// <response code="404">The person does not exist.</response>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(AddressDto), Status200OK)]
    [ProducesResponseType(Status404NotFound)]
    public IActionResult GetHomeAddress(Guid key) =>
        Ok(new AddressDto()
        {
            Code = Guid.NewGuid(),
            Street = "123 Some Place",
            City = "Seattle",
            State = "WA",
            ZipCode = "98101",
        });

    /// <summary>
    /// Gets the work address of a person.
    /// </summary>
    /// <param name="key">The person identifier.</param>
    /// <returns>The person's work address.</returns>
    /// <response code="200">The work address was successfully retrieved.</response>
    /// <response code="404">The person does not exist.</response>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(AddressDto), Status200OK)]
    [ProducesResponseType(Status404NotFound)]
    public IActionResult GetWorkAddress(Guid key) =>
        Ok(new AddressDto()
        {
            Code = Guid.NewGuid(),
            Street = "1 Microsoft Way",
            City = "Redmond",
            State = "WA",
            ZipCode = "98052",
        });
}