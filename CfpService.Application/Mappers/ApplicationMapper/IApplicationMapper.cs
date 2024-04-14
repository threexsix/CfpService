using CfpService.Application.Entities;
using CfpService.Contracts.Dtos.Application;

namespace CfpService.Application.Mappers.ApplicationMapper;

public interface IApplicationMapper
{
    public GetApplicationDto ToDto(ConferenceApplication? entity);
    public Entities.ConferenceApplication ToEntity(PostApplicationDto dto);
    public Entities.ConferenceApplication ToEntity(PutApplicationDto dto, ConferenceApplication? existingConferenceApplication);

}