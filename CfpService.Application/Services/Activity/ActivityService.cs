using CfpService.Application.Mappers.ActivityMapper;
using CfpService.Application.Repositories.Activity;
using CfpService.Contracts.Dtos.Activity;

namespace CfpService.Application.Services.Activity;

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