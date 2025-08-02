using Devy.Security.Api.Application.Model.Employees;
using Devy.Security.Api.Application.Model.Locations;
using Devy.Security.Api.Application.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Devy.Security.Api.Controllers;

/// Controllers for alert executions
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Route("api/locations")]
[ApiController]
public class LocationQueryController : ControllerBase
{
    #region Variables


    private readonly IMediator _mediator = null;

    #endregion Variables

    public LocationQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region Get

    #endregion Get

    #region List

    [AllowAnonymous]
    [HttpGet("")]
    [ProducesResponseType(typeof(IReadOnlyCollection<Location>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetListLocations()
    {
        var response = await this._mediator.Send(new ListLocationsQuery());

        return Ok(response);
    }

    #endregion List
}
