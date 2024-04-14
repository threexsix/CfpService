using CfpService.Contracts.Dtos.Application;
using MediatR;

namespace CfpService.Application.Commands.Application;

public record AddApplicationCommand(PostApplicationDto Dto) : IRequest<GetApplicationDto>;