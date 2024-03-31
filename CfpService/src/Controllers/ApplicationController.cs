using CfpService.Dtos;
using CfpService.Models;
using CfpService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CfpService.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportController : ControllerBase
{
    private IApplicationService _applicationService;

    public ReportController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }
    
    [HttpGet("[action]")]
    public ActionResult<GetApplicationDto> GetById(Guid id)
    {
        var application = _applicationService.GetApplicationById(id);
        if (application == null) return NotFound();
        return Ok(application);
    }
   
    [HttpPost("[action]")]
    public ActionResult<GetApplicationDto> Add([FromBody] PostApplicationDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
  
        var createdApplication = _applicationService.AddApplication(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdApplication.Id }, createdApplication);
    }
    
    [HttpPut("[action]/{id}")]
    public ActionResult<GetApplicationDto> Edit(Guid id, [FromBody] PutApplicationDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
  
        var alteredApplication = _applicationService.EditApplication(id, dto);
        return CreatedAtAction(nameof(GetById), new { id = alteredApplication.Id }, alteredApplication);
    }

    [HttpPut("[action]/{id}")]
    public IActionResult Delete(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _applicationService.DeleteApplication(id);
        
        return Ok();
    }
    
    [HttpPut("{id}/[action]")]
    public IActionResult Submit(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _applicationService.SubmitApplication(id);
        
        return Ok();
    }
    
    [HttpGet("[action]")]
    public ActionResult<IEnumerable<GetApplicationDto>> GetSubmitted([FromQuery] string submittedAfter)
    {
        DateTime time;
        if (!DateTime.TryParse(submittedAfter, out time))
        {
            return BadRequest("Invalid date format.");
        }
        var applications = _applicationService.GetSubmittedApplications(time);
        
        return Ok(applications);
    }
    
    [HttpGet("[action]")]
    public ActionResult<IEnumerable<GetApplicationDto>> GetUnSubmitted([FromQuery] string submittedAfter)
    {
        DateTime time;
        if (!DateTime.TryParse(submittedAfter, out time))
        {
            return BadRequest("Invalid date format.");
        }
        var applications = _applicationService.GetUnSubmittedApplications(time);
        
        return Ok(applications);
    }
    
    
    [HttpGet("[action]")]
    public ActionResult<GetApplicationDto> GetUserUnSubmittedApplication([FromQuery] Guid userId)
    {
        var application = _applicationService.GetUserUnSubmittedApplication(userId);
        if (application == null) return NotFound();
        return Ok(application);
    }
}