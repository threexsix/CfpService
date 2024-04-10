using CfpService.Dtos.Application;
using CfpService.Entities;

namespace CfpService.Mappers.ApplicationMapper;

public interface IApplicationMapper
{
    public GetApplicationDto ToDto(Application entity);
    public Application ToEntity(PostApplicationDto dto);
    public Application ToEntity(PutApplicationDto dto, Application existingApplication);

}