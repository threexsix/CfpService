using CfpService.Contracts.Dtos.Application;
using CfpService.Contracts.Results;
using MediatR;

namespace CfpService.Application.Queries.Application;

public record GetApplicationByIdQuery(Guid Id) : IRequest<Result<GetApplicationDto>>;