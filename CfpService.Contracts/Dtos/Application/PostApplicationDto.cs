namespace CfpService.Contracts.Dtos.Application;

public record PostApplicationDto(
    Guid Author,
    string? Activity,
    string? Name,
    string? Description,
    string? Outline
);