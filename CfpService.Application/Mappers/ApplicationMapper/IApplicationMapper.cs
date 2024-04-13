using CfpService.Contracts.Dtos.Application;

namespace CfpService.Application.Mappers.ApplicationMapper;

public interface IApplicationMapper
{
    public GetApplicationDto ToDto(Entities.ConferenceApplication entity);
    public Entities.ConferenceApplication ToEntity(PostApplicationDto dto);
    public Entities.ConferenceApplication ToEntity(PutApplicationDto dto, Entities.ConferenceApplication existingConferenceApplication);

}