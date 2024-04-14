using CfpService.Application.Entities;
using CfpService.Contracts.Dtos.Activity;
using MediatR;

namespace CfpService.Application.Queries.Activity;

public record GetAllActivitiesQuery() : IRequest<List<GetActivityDto>>;
