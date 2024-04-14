using CfpService.Application.Queries.Activity;
using CfpService.Contracts.Dtos.Activity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CfpService.Controllers;

[ApiController]
[Route("activities")]
public class ActivitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActivitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    public async Task<ActionResult<List<GetActivityDto>>> GetAllActivities()
    {
        var query = new GetAllActivitiesQuery();
        var result = await _mediator.Send(query);

        return Ok(result);
    }
}