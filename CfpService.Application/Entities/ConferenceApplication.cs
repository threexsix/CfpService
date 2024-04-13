namespace CfpService.Application.Entities;
public record ConferenceApplication(
    Guid Id,
    Guid Author,
    string? Activity,
    string? Name,
    string? Description,
    string? Outline,
    DateTime CreatedAt,
    DateTime? SubmittedAt
);