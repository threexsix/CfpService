using CfpService.Dtos.Activity;

namespace CfpService.Services.Activity;

public interface IActivityService
{
    public IEnumerable<GetActivityDto> GetAllActivities();
}