using CfpService.Dtos.Application;
using CfpService.Services.Application;
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
        if (submittedAfter.HasValue && !unsubmittedOlder.HasValue)
        {
            var applications = _applicationService.GetSubmittedApplications(submittedAfter.Value);
            return Ok(applications);
        }
        else if (!submittedAfter.HasValue && unsubmittedOlder.HasValue)
        {
            var applications = _applicationService.GetUnSubmittedApplications(unsubmittedOlder.Value);
            return Ok(applications);
        }
        else
        {
            return BadRequest("Please specify exactly one of the following query parameters: 'submittedAfter' or 'unsubmittedOlder'.");
        }
    }

    
    [HttpGet("{applicationId}")]
    public ActionResult<GetApplicationDto> GetById(Guid applicationId)
    {
        if (!_applicationService.ExistByApplicationId(applicationId))
            return BadRequest("cannot get, application does not exist");
        
        var application = _applicationService.GetApplicationById(applicationId);
        
        return Ok(application);
    }
    
    [HttpPost("{applicationId}/submit")]
    public IActionResult Submit(Guid applicationId)
    {
        if (!_applicationService.ExistByApplicationId(applicationId))
            return BadRequest("cannot submit, application does not exist");
        
        if (!_applicationService.IsApplicationValidToSubmit(applicationId))
        {
            return BadRequest("cannot submit, key fields are not filled in application");
        }

        _applicationService.SubmitApplication(applicationId);
        
        return Ok();
    }
    
    [HttpPost]
    public ActionResult<GetApplicationDto> PostApplication([FromBody] PostApplicationDto dto)
    {
        if (_applicationService.ExistUnsubmittedFromUser(dto.Author))
            return BadRequest("cannot add, user has not-submitted application");
        
        var createdApplication = _applicationService.AddApplication(dto);
        return CreatedAtAction(nameof(GetById), new { applicationId = createdApplication.Id }, createdApplication);
    }
    
    [HttpPut("{applicationId}")]
    public ActionResult<GetApplicationDto> EditNotSubmittedApplication(Guid applicationId, [FromBody] PutApplicationDto dto)
    {
        if (!_applicationService.ExistByApplicationId(applicationId))
            return BadRequest("cannot edit, application does not exist");
            
        if (_applicationService.IsSubmitted(applicationId))
            return BadRequest("cannot edit submitted application");
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
  
        var alteredApplication = _applicationService.EditApplication(applicationId, dto);
        
        return CreatedAtAction(nameof(GetById), new { applicationId = alteredApplication.Id }, alteredApplication);
    }

    [HttpDelete("{applicationId}")]
    public IActionResult Delete(Guid applicationId)
    {
        if (!_applicationService.ExistByApplicationId(applicationId))
            return BadRequest("cannot delete, application does not exist");
        
        if (_applicationService.IsSubmitted(applicationId))
            return BadRequest("cannot delete submitted application");
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _applicationService.DeleteApplication(applicationId);
        
        return Ok();
    }
    
}