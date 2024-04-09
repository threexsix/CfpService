using CfpService.Dtos.Activity;
using CfpService.Repositories.Activity;

namespace CfpService.Services.Activity;

public class ActivityService : IActivityService
{
    private readonly IActivityRepository _activityRepository;

    public ActivityService(IActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }

    public IEnumerable<GetActivityDto> GetAllActivities()
    {
        var activities = _activityRepository.GetAllActivities();
        
        if (activities == null) 
            throw new  ArgumentException("no activity found");
        
        return activities;
    }
}