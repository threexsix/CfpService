using CfpService.Application.Commands.Application;
using CfpService.Application.Mappers.ApplicationMapper;
using CfpService.Application.Repositories.Application;
using CfpService.Contracts.Dtos.Application;
using CfpService.Contracts.Errors;
using CfpService.Contracts.Results;
using MediatR;

namespace CfpService.Application.Handlers.Commands;

public class EditApplicationCommandHandler : IRequestHandler<EditApplicationCommand, Result<GetApplicationDto>>
{
    private readonly IApplicationRepository _repository;
    private readonly IApplicationMapper _mapper;

    public EditApplicationCommandHandler(IApplicationRepository repository, IApplicationMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Result<GetApplicationDto>> Handle(EditApplicationCommand request, CancellationToken cancellationToken)
    {
        var application = await _repository.GetById(request.ApplicationId);
        
        if (application == null)
            return Result.Fail<GetApplicationDto>(ApplicationErrors.ApplicationNotFound(request.ApplicationId));
            
        if (await _repository.IsSubmitted(request.ApplicationId))
            return Result.Fail<GetApplicationDto>(ApplicationErrors.CannotEditSubmittedApplication());
        
        var alteredApplication = await _repository.Put(_mapper.ToEntity(request.Dto, application));
        
        return Result.Ok(_mapper.ToDto(alteredApplication));
    }
}