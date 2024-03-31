namespace CfpService.Dtos;

public record PutApplicationDto(
    Guid Author,
    string Activity,
    string Name,
    string Description,
    string Outline
);