using MediatR;

namespace CfpService.Application.Commands.Application;

public record SubmitApplicationCommand(Guid Id) : IRequest;