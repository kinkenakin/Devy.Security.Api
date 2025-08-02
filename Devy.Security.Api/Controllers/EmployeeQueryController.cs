using Devy.Security.Api.Application.Model.Employees;
using Devy.Security.Api.Application.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Devy.Security.Api.Controllers;

/// Controllers for alert executions
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Route("api/employees/contacts")]
[ApiController]
public class EmployeeQueryController : ControllerBase
{
    #region Variables


    private readonly IMediator _mediator = null;

    #endregion Variables

    public EmployeeQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region Get

    #endregion Get


    #region List

    [AllowAnonymous]
    [HttpGet("")]
    [ProducesResponseType(typeof(IReadOnlyCollection<EmployeeContact>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetListContacts()
    {
        var response = await this._mediator.Send(new ListEmployeeContactsQuery());

        return Ok(response);
    }

    #endregion List
}
