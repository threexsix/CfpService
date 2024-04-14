using CfpService.Application.Commands.Application;
using CfpService.Application.Repositories.Application;
using MediatR;

namespace CfpService.Application.Handlers.Commands;

public class DeleteApplicationCommandHandler : IRequestHandler<DeleteApplicationCommand>
{
    private readonly IApplicationRepository _repository;

    public DeleteApplicationCommandHandler(IApplicationRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
    {
        _repository.Delete(request.Id);
    }
}