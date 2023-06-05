﻿namespace ODataOpenApiExample.MediatR.Order.Commands;

using ApiVersioning.Examples.Models;
using global::MediatR;
using System.Collections.Generic;

public class CreateOrder : IRequest<Order>
{
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime EffectiveDate { get; set; } = DateTime.Now;
    public string Customer { get; set; }
    public string Description { get; set; }
    public virtual IList<LineItem> LineItems { get; } = new List<LineItem>();
}
