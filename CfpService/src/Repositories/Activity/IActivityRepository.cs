using CfpService.Dtos.Activity;

namespace CfpService.Repositories;

public interface IActivityRepository
{
    public IEnumerable<GetActivityDto> GetAllActivities();
}