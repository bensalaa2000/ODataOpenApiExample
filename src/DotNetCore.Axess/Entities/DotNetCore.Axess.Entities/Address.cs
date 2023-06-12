using DotNetCore.Axess.Domain;

namespace DotNetCore.Axess.Entities;
/// <summary>
/// Represents an address.
/// </summary>
public sealed class Address : Entity<int>
{
    public Address(int id) : base(id) { }
    /// <summary>
    /// Gets or sets the street address.
    /// </summary>
    /// <value>The street address.</value>
    public string Street { get; set; }

    /// <summary>
    /// Gets or sets the address city.
    /// </summary>
    /// <value>The address city.</value>
    public string City { get; set; }

    /// <summary>
    /// Gets or sets the address state.
    /// </summary>
    /// <value>The address state.</value>
    public string State { get; set; }

    /// <summary>
    /// Gets or sets the address zip code.
    /// </summary>
    /// <value>The address zip code.</value>
    public string ZipCode { get; set; }
}