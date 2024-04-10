using CfpService.Dtos.Activity;
using CfpService.Entities;

namespace CfpService.Mappers.ActivityMapper;

public class ActivityMapper : IActivityMapper
{
    public Activity ToActivity(GetActivityDto dto)
    {
        return new Activity
        (
            dto.Name,
            dto.Description
        );
    }

    public GetActivityDto ToDto(Activity activity)
    {
        return new GetActivityDto
        (
            activity.Name,
            activity.Description
        );
    }
}