using CfpService.Application.Entities;

namespace CfpService.Application.Repositories.Activity;

public interface IActivityRepository
{
    public Task<IEnumerable<ApplicationActivity>> GetAllActivitiesAsync();
}