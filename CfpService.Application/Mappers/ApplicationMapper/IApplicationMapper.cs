using CfpService.Application.Entities;
using CfpService.Contracts.Dtos.Application;

namespace CfpService.Application.Mappers.ApplicationMapper;

public interface IApplicationMapper
{
    public GetApplicationDto ToDto(ConferenceApplication? entity);
    public ConferenceApplication ToEntity(PostApplicationDto dto);
    public ConferenceApplication ToEntity(PutApplicationDto dto, ConferenceApplication? existingConferenceApplication);

}