using CfpService.Dtos.Activity;

namespace CfpService.Repositories.Activity;

public interface IActivityRepository
{
    public IEnumerable<GetActivityDto> GetAllActivities();
}