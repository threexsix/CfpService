using CfpService.Contracts.Dtos.Application;

namespace CfpService.Application.Mappers.ApplicationMapper;

public  class ApplicationMapper : IApplicationMapper
{
    public GetApplicationDto ToDto( Entities.ConferenceApplication entity)
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

    public Entities.ConferenceApplication ToEntity(PostApplicationDto dto)
    {
        return new Entities.ConferenceApplication
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
    
    public Entities.ConferenceApplication ToEntity(PutApplicationDto dto, Entities.ConferenceApplication existingConferenceApplication)
    {
        return new Entities.ConferenceApplication
        (
            new Guid(),
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
