namespace CfpService.Dtos;

public record PutApplicationDto(
    string Activity,
    string Name,
    string Description,
    string Outline
);