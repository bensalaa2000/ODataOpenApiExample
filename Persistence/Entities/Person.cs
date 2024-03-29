﻿using DotNetCore.Axess.Domain;

namespace ODataOpenApiExample.Persistence.Entities;

/// <summary>
/// Represents a person.
/// </summary>
public class Person : Entity<int>
{
    public Person(int id) : base(id) { }
    /// <summary>
    /// Gets or sets the first name of a person.
    /// </summary>
    /// <value>The person's first name.</value>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of a person.
    /// </summary>
    /// <value>The person's last name.</value>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the email address for a person.
    /// </summary>
    /// <value>The person's email address.</value>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the telephone number for a person.
    /// </summary>
    /// <value>The person's telephone number.</value>
    public string Phone { get; set; }

    public int HomeAddressId { get; set; }
    /// <summary>
    /// Gets or sets the person's home address.
    /// </summary>
    /// <value>The person's home address.</value>
    public virtual Address HomeAddress { get; set; }


    public int WorkAddressId { get; set; }

    /// <summary>
    /// Gets or sets the person's work address.
    /// </summary>
    /// <value>The person's work address.</value>
    public virtual Address WorkAddress { get; set; }
}