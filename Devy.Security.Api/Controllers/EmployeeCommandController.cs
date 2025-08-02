using Devy.Security.Api.Application.Model.Employees;
using Devy.Security.Api.Application.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Numerics;

namespace Devy.Security.Api.Controllers;

/// <summary>
/// Controllers for alert executions
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
[Route("api/employees/contacts")]
[ApiController]
public class EmployeeCommandController : ControllerBase
{
    #region Variables

    private readonly IMediator _mediator = null;

    #endregion Variables

    public EmployeeCommandController(IMediator mediator)
    {
        _mediator = mediator;
    }


    #region Post

    [AllowAnonymous]
    [HttpPost("")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> PostCreateEmployeeContact(CreateEmployeeContactsCommand command)
    {
        var response = await this._mediator.Send(command);

        return Ok(response);
    }

    #endregion Post

    #region Put

    #endregion Put

    #region Delete

    #endregion Delete
}
