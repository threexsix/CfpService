using CfpService.Application.Commands.Application;
using CfpService.Application.Repositories.Application;
using CfpService.Contracts.Errors;
using CfpService.Contracts.Results;
using MediatR;

namespace CfpService.Application.Handlers.Commands;

public class DeleteApplicationCommandHandler : IRequestHandler<DeleteApplicationCommand, Result>
{
    private readonly IApplicationRepository _repository;

    public DeleteApplicationCommandHandler(IApplicationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
    {
        var exists = await _repository.ExistByApplicationIdAsync(request.Id);
        
        if (!exists) 
            return Result.Fail(ApplicationErrors.ApplicationNotFound(request.Id));
        
        if (await _repository.IsSubmittedAsync(request.Id))
            return Result.Fail(ApplicationErrors.AlreadySubmittedApplication());
        
        await _repository.DeleteAsync(request.Id);
        return Result.Ok();
    }
}