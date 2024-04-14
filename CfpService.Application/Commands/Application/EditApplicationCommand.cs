using CfpService.Contracts.Dtos.Application;
using CfpService.Contracts.Results;
using MediatR;

namespace CfpService.Application.Commands.Application;

public record EditApplicationCommand(PutApplicationDto Dto) : IRequest<Result<GetApplicationDto>>;
