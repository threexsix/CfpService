using CfpService.Dtos.Activity;
using CfpService.Entities;

namespace CfpService.Mappers.ActivityMapper;

public interface IActivityMapper
{
    public Activity ToActivity(GetActivityDto dto);
    public GetActivityDto ToDto(Activity activity);
}