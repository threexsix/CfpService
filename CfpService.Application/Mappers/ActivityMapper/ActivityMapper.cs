using CfpService.Application.Entities;
using CfpService.Contracts.Dtos.Activity;

namespace CfpService.Application.Mappers.ActivityMapper;

public class ActivityMapper : IActivityMapper
{
    public ApplicationActivity ToActivity(GetActivityDto dto)
    {
        return new ApplicationActivity
        (
            dto.Name,
            dto.Description
        );
    }

    public GetActivityDto ToDto(ApplicationActivity applicationActivity)
    {
        return new GetActivityDto
        (
            applicationActivity.Name,
            applicationActivity.Description
        );
    }
}