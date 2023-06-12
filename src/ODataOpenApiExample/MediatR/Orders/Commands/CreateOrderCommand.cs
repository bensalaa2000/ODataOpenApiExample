namespace Axess.MediatR.Order.Commands;

using Axess.Architecture.Models;
using global::MediatR;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class CreateOrderCommand : IRequest<Order>
{
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
    public virtual IList<LineItem> LineItems { get; } = new List<LineItem>();
}
