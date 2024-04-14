using CfpService.Contracts.Dtos.Application;
using CfpService.Contracts.Results;
using MediatR;

namespace CfpService.Application.Commands.Application;

public record AddApplicationCommand(PostApplicationDto Dto) : IRequest<Result<GetApplicationDto>>;