using CfpService.Application.Services.Application;
using CfpService.Contracts.Dtos.Application;
using Microsoft.AspNetCore.Mvc;

namespace CfpService.Controllers;

[ApiController]
[Route("applications")]
public class ApplicationsController : ControllerBase
{
    private IApplicationService _applicationService;
    public ApplicationsController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }
        
    [HttpGet]
    public ActionResult<IEnumerable<GetApplicationDto>> GetApplications([FromQuery] DateTime? submittedAfter, [FromQuery] DateTime? unsubmittedOlder)
    {
        var applications = _applicationService.GetApplications(submittedAfter, unsubmittedOlder);
        return Ok(applications);
    }
    
    [HttpGet("{applicationId}")]
    public ActionResult<GetApplicationDto> GetById(Guid applicationId)
    {
        var application = _applicationService.GetApplicationById(applicationId);
        return Ok(application);
    }
    
    [HttpPost("{applicationId}/submit")]
    public IActionResult Submit(Guid applicationId)
    {
        _applicationService.SubmitApplication(applicationId);
        return Ok();
    }
    
    [HttpPost]
    public ActionResult<GetApplicationDto> PostApplication([FromBody] PostApplicationDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var createdApplication = _applicationService.AddApplication(dto);
        return Ok(createdApplication);
    }
    
    [HttpPut("{applicationId}")]
    public ActionResult<GetApplicationDto> EditNotSubmittedApplication(Guid applicationId, [FromBody] PutApplicationDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
  
        var alteredApplication = _applicationService.EditApplication(dto with {Id = applicationId});
        return Ok(alteredApplication);
    }

    [HttpDelete("{applicationId}")]
    public IActionResult Delete(Guid applicationId)
    {
        _applicationService.DeleteApplication(applicationId);
        return Ok();
    }
}