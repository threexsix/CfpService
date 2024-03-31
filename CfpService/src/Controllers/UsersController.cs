using CfpService.Dtos.Application;
using CfpService.Services.Application;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CfpService.Controllers;

[ApiController]
[Route("[controller]")]
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
        if (application == null) return NotFound();
        return Ok(application);
    }
}