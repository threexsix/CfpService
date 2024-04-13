namespace CfpService.Contracts.Dtos.Application;

public record GetApplicationDto(
    Guid Id,
    Guid Author,
    string? Activity,
    string? Name,
    string? Description,
    string? Outline
);