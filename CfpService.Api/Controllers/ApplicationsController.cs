using CfpService.Application.Commands.Application;
using CfpService.Application.Queries.Application;
using CfpService.Contracts.Dtos.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CfpService.Controllers;

[ApiController]
[Route("applications")]
public class ApplicationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApplicationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<GetApplicationDto>>> GetApplications([FromQuery] DateTime? submittedAfter, [FromQuery] DateTime? unsubmittedOlder)
    {
        var query = new GetAllApplicationsQuery(submittedAfter, unsubmittedOlder);
        var result = await _mediator.Send(query);

        if (result.Failure)
            return StatusCode(result.Error.ErrorCode, new { Code = result.Error.ErrorCode, Error = result.Error.ErrorMessage });

        return Ok(result.Value);
    }
    
    [HttpGet("{applicationId}")]
    public async Task<ActionResult<GetApplicationDto>> GetById(Guid applicationId)
    {
        var query = new GetApplicationByIdQuery(applicationId);
        var result = await _mediator.Send(query);
        
        if (result.Failure)
            return StatusCode(result.Error.ErrorCode, new { Code = result.Error.ErrorCode, Error = result.Error.ErrorMessage });

        return Ok(result.Value);
    }
    
    [HttpPost("{applicationId}/submit")]
    public async Task<IActionResult> Submit(Guid applicationId)
    {
        var command = new SubmitApplicationCommand(applicationId);
        var result = await _mediator.Send(command);

        if (result.Failure)
            return StatusCode(result.Error.ErrorCode, new { Code = result.Error.ErrorCode, Error = result.Error.ErrorMessage });
        
        return Ok();
    }
    
    [HttpPost]
    public async Task<ActionResult<GetApplicationDto>> PostApplication([FromBody] PostApplicationDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var command = new AddApplicationCommand(dto);
        var result = await _mediator.Send(command);

        if (result.Failure)
            return StatusCode(result.Error.ErrorCode, new { Code = result.Error.ErrorCode, Error = result.Error.ErrorMessage });
        
        return Ok(result.Value);
    }
    
    [HttpPut("{applicationId}")]
    public async Task<ActionResult<GetApplicationDto>> EditNotSubmittedApplication(Guid applicationId, [FromBody] PutApplicationDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var command = new EditApplicationCommand(applicationId, dto);
        var result = await _mediator.Send(command);
        
        if (result.Failure)
            return StatusCode(result.Error.ErrorCode, new { Code = result.Error.ErrorCode, Error = result.Error.ErrorMessage });

        return Ok(result.Value);
    }

    [HttpDelete("{applicationId}")]
    public async Task<IActionResult> Delete(Guid applicationId)
    {
        var command = new DeleteApplicationCommand(applicationId);
        var result = await _mediator.Send(command);

        if (result.Failure)
            return StatusCode(result.Error.ErrorCode, new { Code = result.Error.ErrorCode, Error = result.Error.ErrorMessage });
        
        return Ok();
    }
}