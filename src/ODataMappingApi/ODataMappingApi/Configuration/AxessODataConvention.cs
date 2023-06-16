using Microsoft.AspNetCore.OData.Routing.Conventions;

namespace Axess.Api.Configuration;

/// <summary>
/// Simple convention
/// </summary>
public class AxessODataConvention : IODataControllerActionConvention
{
    /// <summary>
    /// Order value.
    /// </summary>
    public int Order => -100;

    /// <summary>
    /// Apply to action,.
    /// </summary>
    /// <param name="context">Http context.</param>
    /// <returns>true/false</returns>
    public bool AppliesToAction(ODataControllerActionContext context)
    {
        return true; // apply to all controller
    }

    /// <summary>
    /// Apply to controller
    /// </summary>
    /// <param name="context">Http context.</param>
    /// <returns>true/false</returns>
    public bool AppliesToController(ODataControllerActionContext context)
    {
        return false; // continue for all others
    }
}
