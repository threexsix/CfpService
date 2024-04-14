using CfpService.Application.Entities;
using CfpService.Contracts.Dtos.Application;

namespace CfpService.Application.Mappers.ApplicationMapper;

public  class ApplicationMapper : IApplicationMapper
{
    public GetApplicationDto ToDto( ConferenceApplication? entity)
    {
        return new GetApplicationDto
        (
            entity.Id,
            entity.Author,
            entity.Activity,
            entity.Name,
            entity.Description,
            entity.Outline
        );
    }

    public ConferenceApplication ToEntity(PostApplicationDto dto)
    {
        return new ConferenceApplication
        (
            new Guid(),
            dto.Author,
            dto.Activity,
            dto.Name,
            dto.Description,
            dto.Outline,
            DateTime.Now,
            null
        );
    }
    
    public ConferenceApplication ToEntity(PutApplicationDto dto, ConferenceApplication? existingConferenceApplication)
    {
        return new ConferenceApplication
        (
            existingConferenceApplication.Id,
            existingConferenceApplication.Author,
            dto.Activity,
            dto.Name,
            dto.Description,
            dto.Outline,
            existingConferenceApplication.CreatedAt,
            existingConferenceApplication.SubmittedAt
        );
    }
}
