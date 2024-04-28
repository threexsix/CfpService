using CfpService.Contracts.Results;
using MediatR;

namespace CfpService.Application.Commands.Application;

public record DeleteApplicationCommand(Guid Id) : IRequest<Result>;