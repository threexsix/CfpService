using CfpService.Dtos.Activity;
using CfpService.Mappers.ActivityMapper;
using CfpService.Repositories.Activity;

namespace CfpService.Services.Activity;

public class ActivityService : IActivityService
{
    private readonly IActivityRepository _activityRepository;
    private readonly IActivityMapper _mapper;

    public ActivityService(IActivityRepository activityRepository, IActivityMapper mapper)
    {
        _activityRepository = activityRepository;
        _mapper = mapper;
    }

    public IEnumerable<GetActivityDto> GetAllActivities()
    {
        var activities = _activityRepository.GetAllActivities();
        
        if (activities == null) 
            throw new  ArgumentException("no activity found");
            
        return activities.Select(x => _mapper.ToDto(x));
    }
}