﻿namespace Axess.Application.MediatR.Orders.Commands.V3;

using Axess.Application.Models;
using global::MediatR;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class CreateOrderCommand : IRequest<OrderDto>
{
	public CreateOrderCommand(string customer, string description)
	{
		Customer = customer;
		Description = description;
	}
	/// <summary>
	/// 
	/// </summary>
	public DateTime CreatedDate { get; set; } = DateTime.Now;
	/// <summary>
	/// 
	/// </summary>
	public DateTime EffectiveDate { get; set; } = DateTime.Now;
	/// <summary>
	/// 
	/// </summary>
	public string Customer { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public string Description { get; set; }
	/// <summary>
	/// 
	/// </summary>
	public virtual IList<LineItemDto> LineItems { get; } = new List<LineItemDto>();
}
