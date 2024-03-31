using CfpService.Models;
using CfpService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CfpService.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicationController : ControllerBase
{
    private readonly IApplicationService _applicationService;

    public ActivityController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }
    
    [HttpPost]
    public ActionResult<Application> AddApplication([FromBody] Application application)
    {
        var createdApplication = _applicationService.AddApplication(application);
        return CreatedAtAction(nameof(GetApplication), new { id = createdApplication.Id }, createdApplication);
    }

    [HttpPut("{id}")]
    public ActionResult<Application> UpdateApplication(Guid id, [FromBody] Application application)
    {
        if (!_applicationService.Exists(id))
        {
            return NotFound();
        }
        
        var updatedApplication = _applicationService.UpdateApplication(id, application);
        return Ok(updatedApplication);
    }

    [HttpGet("{id}")]
    public ActionResult<Application> GetApplication(Guid id)
    {
        var application = _applicationService.GetApplicationById(id);

        if (application == null)
        {
            return NotFound();
        }

        return Ok(application);
    }

}
