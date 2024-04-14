using CfpService.Application.Commands.Application;
using CfpService.Application.Repositories.Application;
using CfpService.Contracts.Errors;
using CfpService.Contracts.Results;
using MediatR;

namespace CfpService.Application.Handlers.Commands;

public class SubmitApplicationCommandHandler : IRequestHandler<SubmitApplicationCommand, Result>
{
    private readonly IApplicationRepository _repository;

    public SubmitApplicationCommandHandler(IApplicationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(SubmitApplicationCommand request, CancellationToken cancellationToken)
    {
        if (await _repository.ExistByApplicationId(request.Id) == false)
            return Result.Fail(ApplicationErrors.ApplicationNotFound(request.Id));

        if (await _repository.IsSubmitted(request.Id))
            return Result.Fail(ApplicationErrors.AlreadySubmittedApplication());
        
        if (await IsApplicationValidToSubmit(request.Id) == false)
            return Result.Fail(ApplicationErrors.CannotSubmitInvalidApplication());
        
        await _repository.Submit(request.Id);
        
        return Result.Ok();
    }
    
    private async Task<bool> IsApplicationValidToSubmit(Guid applicationId)
    {
        var application = await _repository.GetById(applicationId);
        return (!string.IsNullOrWhiteSpace(application.Name) && !string.IsNullOrWhiteSpace(application.Activity) &&
                !string.IsNullOrWhiteSpace(application.Outline));
    }
}