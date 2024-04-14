using CfpService.Contracts.Dtos.Application;
using MediatR;

namespace CfpService.Application.Queries.Application;

public record GetUnSubmittedApplicationByUserIdQuery(Guid UserId) : IRequest<GetApplicationDto>;