using CfpService.Contracts.Dtos.Application;
using CfpService.Contracts.Results;
using MediatR;

namespace CfpService.Application.Queries.Application;

public record GetUnSubmittedApplicationByUserIdQuery(Guid UserId) : IRequest<Result<GetApplicationDto>>;