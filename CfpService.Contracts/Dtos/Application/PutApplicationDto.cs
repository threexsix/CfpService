namespace CfpService.Contracts.Dtos.Application;

public record PutApplicationDto(
    string? Activity,
    string? Name,
    string? Description,
    string? Outline
);