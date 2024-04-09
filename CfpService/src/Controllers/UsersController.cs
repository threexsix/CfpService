using CfpService.Dtos.Application;
using CfpService.Services.Application;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CfpService.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IApplicationService _applicationService;

    public UsersController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpGet("{userId}/currentapplication")]
    public ActionResult<GetApplicationDto> GetCurrentApplication(Guid userId)
    {
        var application = _applicationService.GetUserUnSubmittedApplication(userId);
        if (application == null) 
            return BadRequest("user doesn't have any submitted applications");
        
        return Ok(application);
    }
}