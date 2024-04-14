using CfpService.Application.Commands.Application;
using CfpService.Application.Repositories.Application;
using MediatR;

namespace CfpService.Application.Handlers.Commands;

public class SubmitApplicationCommandHandler : IRequestHandler<SubmitApplicationCommand>
{
    private readonly IApplicationRepository _repository;

    public SubmitApplicationCommandHandler(IApplicationRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(SubmitApplicationCommand request, CancellationToken cancellationToken)
    {
        if (await _repository.ExistByApplicationId(request.Id) == false)
            throw new KeyNotFoundException($"application with id {request.Id} not found");
        
        if (await IsApplicationValidToSubmit(request.Id) == false)
            throw new ArgumentException("cannot submit, key fields are not filled in application");
        
        await _repository.Submit(request.Id);
    }
    
    private async Task<bool> IsApplicationValidToSubmit(Guid applicationId)
    {
        var application = await _repository.GetById(applicationId);
        return (!string.IsNullOrWhiteSpace(application.Name) && !string.IsNullOrWhiteSpace(application.Activity) &&
                !string.IsNullOrWhiteSpace(application.Outline));
    }
}