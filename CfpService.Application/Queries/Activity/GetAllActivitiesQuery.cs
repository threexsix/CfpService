using CfpService.Contracts.Dtos.Activity;
using CfpService.Contracts.Results;
using MediatR;

namespace CfpService.Application.Queries.Activity;

public record GetAllActivitiesQuery() : IRequest<Result<List<GetActivityDto>>>;
