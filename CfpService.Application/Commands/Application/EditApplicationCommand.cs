using CfpService.Contracts.Dtos.Application;
using MediatR;

namespace CfpService.Application.Commands.Application;

public record EditApplicationCommand(PutApplicationDto Dto) : IRequest<GetApplicationDto>;
