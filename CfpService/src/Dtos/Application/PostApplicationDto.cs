namespace CfpService.Dtos;

public record PostApplicationDto(
    Guid Author,
    string Activity,
    string Name,
    string Description,
    string Outline
);