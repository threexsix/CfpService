using CfpService.Contracts.Dtos.Application;
using MediatR;

namespace CfpService.Application.Queries.Application;

public record GetAllApplicationsQuery(DateTime? SubmittedAfter, DateTime? UnsubmittedOlder) : IRequest<List<GetApplicationDto>>;