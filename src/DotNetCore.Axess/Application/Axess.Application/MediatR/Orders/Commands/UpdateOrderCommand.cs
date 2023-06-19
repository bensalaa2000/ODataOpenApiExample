﻿namespace Axess.Application.MediatR.Orders.Commands;

using Axess.Application.Models;
using global::MediatR;
using System.Collections.Generic;

public class UpdateOrderCommand : IRequest<OrderDto>
{
	public Guid Id { get; set; }
	public DateTime CreatedDate { get; set; } = DateTime.Now;
	public DateTime EffectiveDate { get; set; } = DateTime.Now;
	public string Customer { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public virtual IList<LineItemDto> LineItems { get; } = new List<LineItemDto>();
}
