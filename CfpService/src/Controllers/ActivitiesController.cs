using CfpService.Dtos.Activity;
using CfpService.Services.Activity;
using Microsoft.AspNetCore.Mvc;

namespace CfpService.Controllers;

[ApiController]
[Route("activities")]
public class ActivitiesController : ControllerBase
{
    private readonly IActivityService _activityService;

    public ActivitiesController(IActivityService activityService)
    {
        _activityService = activityService;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<GetActivityDto>> GetAllActivities()
    {
        var activities = _activityService.GetAllActivities();
        if (activities == null) 
            return BadRequest("no activity found");
        return Ok(activities);
    }
}