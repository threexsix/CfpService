using CfpService.Contracts.Dtos.Application;
using CfpService.Contracts.Results;
using MediatR;

namespace CfpService.Application.Queries.Application;

public record GetAllApplicationsQuery(DateTime? SubmittedAfter, DateTime? UnsubmittedOlder) : IRequest<Result<List<GetApplicationDto>>>;