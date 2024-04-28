using CfpService.Application.Entities;
using CfpService.Contracts.Dtos.Activity;

namespace CfpService.Application.Mappers.ActivityMapper;

public interface IActivityMapper
{
    public ApplicationActivity ToActivity(GetActivityDto dto);
    public GetActivityDto ToDto(ApplicationActivity applicationActivity);
}