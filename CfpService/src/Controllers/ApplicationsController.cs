using CfpService.Dtos.Application;
using CfpService.Services.Application;
using Microsoft.AspNetCore.Mvc;

namespace CfpService.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicationsController : ControllerBase
{
    private IApplicationService _applicationService;
    public ApplicationsController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }
    
        
    [HttpGet("submittedAfter={timeString}")]
    public ActionResult<IEnumerable<GetApplicationDto>> GetSubmittedAfter(string timeString)
    {
        if (!DateTime.TryParse(timeString, out var time))
            return BadRequest("invalid date format");
        
        var applications = _applicationService.GetSubmittedApplications(time);
        
        return Ok(applications);
    }
    
    [HttpGet("unsubmittedOlder={timeString}")]
    public ActionResult<IEnumerable<GetApplicationDto>> GetUnsubmittedOlder(string timeString)
    {
        if (!DateTime.TryParse(timeString, out var time))
            return BadRequest("invalid date format");
        
        var applications = _applicationService.GetUnSubmittedApplications(time);
        
        return Ok(applications);
    }
    
    [HttpGet("{applicationId}")]
    public ActionResult<GetApplicationDto> GetById(Guid applicationId)
    {
        if (!_applicationService.ExistByApplicationId(applicationId))
            return NotFound();
        
        var application = _applicationService.GetApplicationById(applicationId);
        
        return Ok(application);
    }
    
    [HttpPost("{applicationId}/[action]")]
    public IActionResult Submit(Guid applicationId)
    {
        if (!_applicationService.ExistByApplicationId(applicationId))
            return NotFound();
        
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
            return NotFound();
            
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
            return NotFound();
        
        if (_applicationService.IsSubmitted(applicationId))
            return BadRequest("cannot delete submitted application");
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _applicationService.DeleteApplication(applicationId);
        
        return Ok();
    }
    
}