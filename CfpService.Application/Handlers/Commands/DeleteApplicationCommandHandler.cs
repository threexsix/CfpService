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
        var exists = await _repository.ExistByApplicationId(request.Id);
        
        if (!exists) 
            return Result.Fail(ApplicationErrors.ApplicationNotFound(request.Id));
        
        await _repository.Delete(request.Id);
        return Result.Ok();
    }
}