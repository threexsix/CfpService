using CfpService.Application.Queries.Application;
using CfpService.Contracts.Dtos.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CfpService.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    [HttpGet("{userId}/currentapplication")]
    public async Task<ActionResult<GetApplicationDto>> GetCurrentApplication(Guid userId)
    {
        var query = new GetUnSubmittedApplicationByUserIdQuery(userId);
        var result = await _mediator.Send(query);

        return Ok(result);
    }
}