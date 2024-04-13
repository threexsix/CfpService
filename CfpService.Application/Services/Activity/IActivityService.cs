using CfpService.Contracts.Dtos.Activity;

namespace CfpService.Application.Services.Activity;

public interface IActivityService
{
    public IEnumerable<GetActivityDto> GetAllActivities();
}