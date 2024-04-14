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
        if (!_repository.ExistByApplicationId(request.Id))
            throw new KeyNotFoundException($"application with id {request.Id} not found");
        
        if (!IsApplicationValidToSubmit(request.Id))
            throw new ArgumentException("cannot submit, key fields are not filled in application");
        
        _repository.Submit(request.Id);
    }
    
    private bool IsApplicationValidToSubmit(Guid applicationId)
    {
        var application = _repository.GetById(applicationId);
        return (!string.IsNullOrWhiteSpace(application.Name) && !string.IsNullOrWhiteSpace(application.Activity) &&
                !string.IsNullOrWhiteSpace(application.Outline));
    }
}