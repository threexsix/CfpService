using CfpService.Dtos.Application;
using CfpService.Services.Activity;
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
    
    [HttpGet("{id}")]
    public ActionResult<GetApplicationDto> GetById(Guid id)
    {
        var application = _applicationService.GetApplicationById(id);
        if (application == null) return NotFound();
        return Ok(application);
    }
   
    [HttpPost]
    public ActionResult<GetApplicationDto> Add([FromBody] PostApplicationDto dto)
    {
        if (_applicationService.AnyDraftUserApplications(dto.Author))
            return BadRequest("cannot add, user has not-submitted application");
        
        var createdApplication = _applicationService.AddApplication(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdApplication.Id }, createdApplication);
    }
    
    [HttpPut("{id}")]
    public ActionResult<GetApplicationDto> EditNotSubmittedApplication(Guid id, [FromBody] PutApplicationDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (_applicationService.IsSubmitted(id))
        {
            return BadRequest("cannot edit submitted application");
        }
  
        var alteredApplication = _applicationService.EditApplication(id, dto);
        if (alteredApplication == null) return NotFound();
        
        return CreatedAtAction(nameof(GetById), new { id = alteredApplication.Id }, alteredApplication);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (_applicationService.IsSubmitted(id))
        {
            return BadRequest("cannot delete submitted application");
        }

        _applicationService.DeleteApplication(id);
        
        return Ok();
    }
    
    [HttpPost("{id}/[action]")]
    public IActionResult Submit(Guid id)
    {
        if (!_applicationService.IsApplicationValidToSubmit(id))
        {
            return BadRequest("cannot submit, key fields are not filled in application");
        }

        _applicationService.SubmitApplication(id);
        
        return Ok();
    }
    
    [HttpGet("submittedAfter={timeString}")]
    public ActionResult<IEnumerable<GetApplicationDto>> GetSubmittedAfter(string timeString)
    {
        DateTime time;
        if (!DateTime.TryParse(timeString, out time))
        {
            return BadRequest("invalid date format");
        }
        var applications = _applicationService.GetSubmittedApplications(time);
        
        return Ok(applications);
    }
    
    [HttpGet("unsubmittedOlder={timeString}")]
    public ActionResult<IEnumerable<GetApplicationDto>> GetUnSubmitted(string timeString)
    {
        DateTime time;
        if (!DateTime.TryParse(timeString, out time))
        {
            return BadRequest("invalid date format");
        }
        var applications = _applicationService.GetUnSubmittedApplications(time);
        
        return Ok(applications);
    }
}