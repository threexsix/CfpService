using CfpService.Contracts.Dtos.Application;
using MediatR;

namespace CfpService.Application.Queries.Application;

public record GetApplicationByIdQuery(Guid Id) : IRequest<GetApplicationDto>;