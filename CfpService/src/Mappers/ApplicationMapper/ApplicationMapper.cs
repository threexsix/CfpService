using CfpService.Dtos.Application;
using CfpService.Entities;

namespace CfpService.Mappers.ApplicationMapper;

public class ApplicationMapper
{
    public static GetApplicationDto ToDto( Application entity)
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

    public static Application ToEntity(PostApplicationDto dto)
    {
        return new Application
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
    
    public static Application ToEntity(PutApplicationDto dto, Application existingApplication)
    {
        return new Application
        (
            new Guid(),
            existingApplication.Author,
            dto.Activity,
            dto.Name,
            dto.Description,
            dto.Outline,
            existingApplication.CreatedAt,
            existingApplication.SubmittedAt
        );
    }
}
