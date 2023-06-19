namespace Axess.Controllers.V3;

using Asp.Versioning;
using Asp.Versioning.OData;
using Axess.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.OData.UriParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using static Microsoft.AspNetCore.Http.StatusCodes;

/// <summary>
/// Represents a RESTful service of products.
/// </summary>
[ApiVersion(3.0)]
public class ProductsController : ApiODataControllerBase
{
	private readonly IQueryable<ProductDto> products = new[]
	{
		NewProduct( Guid.NewGuid() ),
		NewProduct( Guid.NewGuid() ),
		NewProduct( Guid.NewGuid() ),
	}.AsQueryable();

	/// <summary>
	/// Retrieves all products.
	/// </summary>
	/// <returns>All available products.</returns>
	/// <response code="200">Products successfully retrieved.</response>
	[HttpGet]
	[EnableQuery]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(typeof(ODataValue<IEnumerable<ProductDto>>), Status200OK)]
	public IQueryable<ProductDto> Get() => products;

	/// <summary>
	/// Gets a single product.
	/// </summary>
	/// <param name="key">The requested product identifier.</param>
	/// <returns>The requested product.</returns>
	/// <response code="200">The product was successfully retrieved.</response>
	/// <response code="404">The product does not exist.</response>
	[HttpGet]
	[EnableQuery]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(typeof(ProductDto), Status200OK)]
	[ProducesResponseType(Status404NotFound)]
	public SingleResult<ProductDto> Get(Guid key) =>
		SingleResult.Create(products.Where(p => p.Code == key));

	/// <summary>
	/// Creates a new product.
	/// </summary>
	/// <param name="product">The product to create.</param>
	/// <returns>The created product.</returns>
	/// <response code="201">The product was successfully created.</response>
	/// <response code="204">The product was successfully created.</response>
	/// <response code="400">The product is invalid.</response>
	[HttpPost]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(typeof(ProductDto), Status201Created)]
	[ProducesResponseType(Status204NoContent)]
	[ProducesResponseType(Status400BadRequest)]
	public IActionResult Post([FromBody] ProductDto product)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		product.Code = Guid.NewGuid();

		return Created(product);
	}

	/// <summary>
	/// Updates an existing product.
	/// </summary>
	/// <param name="key">The requested product identifier.</param>
	/// <param name="delta">The partial product to update.</param>
	/// <returns>The updated product.</returns>
	/// <response code="200">The product was successfully updated.</response>
	/// <response code="204">The product was successfully updated.</response>
	/// <response code="400">The product is invalid.</response>
	/// <response code="404">The product does not exist.</response>
	[HttpPatch]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(typeof(ProductDto), Status200OK)]
	[ProducesResponseType(Status204NoContent)]
	[ProducesResponseType(Status400BadRequest)]
	[ProducesResponseType(Status404NotFound)]
	public IActionResult Patch(Guid key, [FromBody] Delta<ProductDto> delta)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var product = new ProductDto() { Code = key, Name = "Updated Product " + key.ToString() };

		delta.Patch(product);

		return Updated(delta);
	}

	/// <summary>
	/// Updates an existing product.
	/// </summary>
	/// <param name="key">The requested product identifier.</param>
	/// <param name="update">The product to update.</param>
	/// <returns>The updated product.</returns>
	/// <response code="200">The product was successfully updated.</response>
	/// <response code="204">The product was successfully updated.</response>
	/// <response code="400">The product is invalid.</response>
	/// <response code="404">The product does not exist.</response>
	[HttpPut]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(typeof(ProductDto), Status200OK)]
	[ProducesResponseType(Status204NoContent)]
	[ProducesResponseType(Status400BadRequest)]
	[ProducesResponseType(Status404NotFound)]
	public IActionResult Put(int key, [FromBody] ProductDto update)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		return Updated(update);
	}

	/// <summary>
	/// Deletes a product.
	/// </summary>
	/// <param name="key">The product to delete.</param>
	/// <returns>None</returns>
	/// <response code="204">The product was successfully deleted.</response>
	[HttpDelete]
	[ProducesResponseType(Status204NoContent)]
	[ProducesResponseType(Status404NotFound)]
	public IActionResult Delete(int key) => NoContent();

	/// <summary>
	/// Gets the supplier associated with the product.
	/// </summary>
	/// <param name="key">The product identifier.</param>
	/// <returns>The supplier</returns>
	/// <returns>The requested supplier.</returns>
	[HttpGet]
	[EnableQuery]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(typeof(SupplierDto), Status200OK)]
	[ProducesResponseType(Status404NotFound)]
	public SingleResult<SupplierDto> GetSupplier(Guid key) =>
		SingleResult.Create(products.Where(p => p.Code == key).Select(p => p.Supplier));

	/// <summary>
	/// Gets the link to the associated supplier, if any.
	/// </summary>
	/// <param name="key">The product identifier.</param>
	/// <param name="navigationProperty">The name of the related navigation property.</param>
	/// <returns>The supplier link.</returns>
	[HttpGet]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(typeof(ODataId), Status200OK)]
	[ProducesResponseType(Status404NotFound)]
	public IActionResult GetRef(int key, string navigationProperty)
	{
		var segments = Request.ODataFeature().Path.ToArray();
		var entitySet = ((EntitySetSegment)segments[0]).EntitySet;
		var property = entitySet.NavigationPropertyBindings.Single(p => p.Path.Path == navigationProperty).NavigationProperty;

		segments[^1] = new NavigationPropertySegment(property, entitySet);

		var relatedKey = new Uri(Request.CreateODataLink(segments));

		return Ok(relatedKey);
	}

	/// <summary>
	/// Links a supplier to a product.
	/// </summary>
	/// <param name="key">The product identifier.</param>
	/// <param name="navigationProperty">The name of the related navigation property.</param>
	/// <param name="link">The related entity identifier.</param>
	/// <returns>None</returns>
	[HttpPut]
	[ProducesResponseType(Status204NoContent)]
	[ProducesResponseType(Status404NotFound)]
	public IActionResult CreateRef(
		int key,
		string navigationProperty,
		[FromBody] Uri link)
	{
		return NoContent();
	}

	/// <summary>
	/// Unlinks a supplier from a product.
	/// </summary>
	/// <param name="key">The product identifier.</param>
	/// <param name="navigationProperty">The name of the related navigation property.</param>
	/// <param name="relatedKey">The related entity identifier.</param>
	/// <returns>None</returns>
	[HttpDelete]
	[ProducesResponseType(Status204NoContent)]
	[ProducesResponseType(Status404NotFound)]
	public IActionResult DeleteRef(
		int key,
		string navigationProperty,
		int relatedKey) => NoContent();

	private static ProductDto NewProduct(Guid id) =>
		new()
		{

			Category = "Test",
			Name = "Product " + id.ToString(),
			Price = 10,
			Supplier = new SupplierDto() { Code = Guid.NewGuid(), Name = "Supplier " + id.ToString() },
			SupplierId = Guid.NewGuid(),
		};
}