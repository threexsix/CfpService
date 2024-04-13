using CfpService.Application.Services.Application;
using CfpService.Contracts.Dtos.Application;
using Microsoft.AspNetCore.Mvc;

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
        return Ok(application);
    }
}