﻿namespace ODataOpenApiExample.Controllers.V2;

using ApiVersioning.Examples.Models;
using Asp.Versioning;
using Asp.Versioning.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Validator;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData;
using ODataOpenApiExample.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static Microsoft.AspNetCore.OData.Query.AllowedQueryOptions;

/// <summary>
/// Represents a RESTful people service.
/// </summary>
[ApiVersion(2.0)]
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
    [ProducesResponseType(typeof(ODataValue<IEnumerable<Person>>), Status200OK)]
    public IActionResult Get(ODataQueryOptions<Person> options)
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

        Person[] people = new Person[]
        {
            new()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@somewhere.com",
            },
            new()
            {
                Id = 2,
                FirstName = "Bob",
                LastName = "Smith",
                Email = "bob.smith@somewhere.com",
            },
            new()
            {
                Id = 3,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@somewhere.com",
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
    [ProducesResponseType(typeof(Person), Status200OK)]
    [ProducesResponseType(Status404NotFound)]
    public IActionResult Get(int key, ODataQueryOptions<Person> options)
    {
        Person[] people = new Person[]
        {
            new()
            {
                Id = key,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@somewhere.com",
            }
        };

        object person = options.ApplyTo(people.AsQueryable()).SingleOrDefault();

        if (person == null)
        {
            return NotFound();
        }

        return Ok(person);
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
    [ProducesResponseType(typeof(ODataValue<IEnumerable<Person>>), Status200OK)]
    public IActionResult NewHires(DateTime since, ODataQueryOptions<Person> options) => Get(options);

    /// <summary>
    /// Gets the home address of a person.
    /// </summary>
    /// <param name="key">The person identifier.</param>
    /// <returns>The person's home address.</returns>
    /// <response code="200">The home address was successfully retrieved.</response>
    /// <response code="404">The person does not exist.</response>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Address), Status200OK)]
    [ProducesResponseType(Status404NotFound)]
    public IActionResult GetHomeAddress(int key) =>
        Ok(new Address()
        {
            Id = 42,
            Street = "123 Some Place",
            City = "Seattle",
            State = "WA",
            ZipCode = "98101",
        });
}