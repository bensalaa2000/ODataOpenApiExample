namespace Axess.Application.Models;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents an order.
/// </summary>
/*[Select]
[Select("effectiveDate", SelectType = SelectExpandType.Disabled)]*/
public class OrderDto
{
	/// <summary>
	/// Gets or sets the unique identifier for the order.
	/// </summary>
	/// <value>The order's unique identifier.</value>
	public Guid Code { get; set; }

	/// <summary>
	/// Gets or sets the date and time when the order was created.
	/// </summary>
	/// <value>The order's creation date.</value>
	public DateTime CreatedDate { get; set; } = DateTime.Now;

	/// <summary>
	/// Gets or sets the date and time when the order becomes effective.
	/// </summary>
	/// <value>The order's effective date.</value>
	public DateTime EffectiveDate { get; set; } = DateTime.Now;

	/// <summary>
	/// Gets or sets the name of the ordering customer.
	/// </summary>
	/// <value>The name of the customer that placed the order.</value>
	//[Required]
	public string Customer { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets a description for the order.
	/// </summary>
	/// <value>The description of the order.</value>
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// Gets a list of line items in the order.
	/// </summary>
	/// <value>The <see cref="IList{T}">list</see> of order <see cref="LineItemDto">line items</see>.</value>
	//[Contained]
	public virtual IList<LineItemDto> LineItems { get; } = new List<LineItemDto>();
}