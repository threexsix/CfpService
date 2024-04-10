namespace CfpService.Entities;
public record Application(
    Guid Id,
    Guid Author,
    string? Activity,
    string? Name,
    string? Description,
    string? Outline,
    DateTime CreatedAt,
    DateTime? SubmittedAt
);